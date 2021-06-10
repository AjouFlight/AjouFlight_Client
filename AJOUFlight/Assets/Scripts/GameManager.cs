using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;
using Models;
using UnityEditor;

public class GameManager : MonoBehaviour 
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private int score = 0;
    private double money = 0;
    private int stage = 0;
    private int type = 0;
    private bool isGameOver = false;
    private bool bossTime = false;

    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private GameObject[] bosses;
    [SerializeField]
    private Sprite[] skins;
    [SerializeField]
    private MovementJoystick movementJoystick;
    [SerializeField]
    private ShootingJoystick shootingJoystick;
    [SerializeField]
    private Text finalScoreText;
    [SerializeField]
    private Text finalMoneyText;
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private Canvas bossHpCanvas;
    [SerializeField]
    private Text endPanelText;
    [SerializeField]
    private GameObject nextStageButton;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text hpText;
    
    private GameObject player;
    private List<GameObject> currentEnemies = new List<GameObject>();

    // Server variables
    private RequestHelper currentRequest;
    private const string basePath = "http://ec2-3-36-132-39.ap-northeast-2.compute.amazonaws.com";


    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public double Money
    {
        get { return money; }
        set { if (value > 0) money = value; else money = 0; }
    }
    public int Stage
    {
        get { return stage; }
        set { if (stage > 0 && stage < 4) stage = value; else stage = 1; }
    }
    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }
    public bool IsBossTime
    {
        get { return bossTime; }
        set { bossTime = value; }
    }
    public int Type
    {
        get { return type; }
        set { if (value >= 0 && value <= 3) type = value; else type = 0; }
    }


    void Awake()
    {
        InitPlayer();
    }


    void Start()
    {
        Type = PlayerInformation.currentStage - 1;
        IsBossTime = false;
        IsGameOver = false;

        StartCoroutine(PlayBossFlow());
        StartCoroutine(PlayGameFlow());
    }
    

    void Update()
    {
        if (IsGameOver) return;

        scoreText.text = "Score: " + score.ToString();
    }


    /********************************************
    * Function : InitPlayer()
    * descrition : Instantiate the player and attach the movement, shooting joysticks.
    ********************************************/
    private void InitPlayer()
    {
        int flightIndex = PlayerInformation.selectedFlight;
        int skinIndex = PlayerInformation.selectedSkin;
        player = Instantiate(players[flightIndex].gameObject, Vector3.zero, transform.rotation);
        if(skinIndex != -1) {
            player.GetComponent<SpriteRenderer>().sprite = skins[skinIndex];
        }

        switch (flightIndex)
        {
            case 0:
                player.GetComponent<FlightA>().MoveJoystick = movementJoystick;
                player.GetComponent<FlightA>().Shootjoystick = shootingJoystick;
                break;
            case 1:
                player.GetComponent<FlightB>().MoveJoystick = movementJoystick;
                player.GetComponent<FlightB>().Shootjoystick = shootingJoystick;
                break;
            case 2:
                player.GetComponent<FlightC>().MoveJoystick = movementJoystick;
                player.GetComponent<FlightC>().Shootjoystick = shootingJoystick;
                break;
            case 3:
                player.GetComponent<FlightD>().MoveJoystick = movementJoystick;
                player.GetComponent<FlightD>().Shootjoystick = shootingJoystick;
                break;
            default:
                Debug.Log("Invalid selected flight");
                break;
        }
    }


    /********************************************
     * Function : AddScore(int score)
     * descrition : Add the score
     ********************************************/
    public void AddScore(int score)
    {
        Score += score;
    }


    /********************************************
     * Function : AddGameMoney(double money)
     * descrition : Add the money
     ********************************************/
    public void AddGameMoney(double money)
    {
        Money += money;
    }


    /********************************************
     * Function : GameOver()
     * descrition : Stop Game and call the ShowEndPanel() method for showing the game result.
     ********************************************/
    public void GameOver()
    {
        AudioManager.Instance.PlayPlayerDeathClip();
        IsGameOver = true;

        Destroy(player.gameObject);
        foreach (GameObject enemy in currentEnemies) {
            Destroy(enemy);
        }

        ShowEndPanel();
    }


    /********************************************
     * Function : BossDead()
     * descrition : When the boss is died, call the ShowEndPanel() method and set it for the next stage or ending.
     ********************************************/
    public void BossDead()
    {
        int clearedStage = PlayerInformation.clearedStage;
        int currentStage = PlayerInformation.currentStage;
        if (clearedStage < currentStage) {
            PlayerInformation.clearedStage = PlayerInformation.currentStage;
            clearedStage = currentStage;
        }

        AudioManager.Instance.PlayWinClip();

        Destroy(player.gameObject);
        foreach (GameObject enemy in currentEnemies) {
            Destroy(enemy);
        }
        
        ShowEndPanel();

        if (clearedStage == 3 && currentStage == 3) {
            // Ending Text
            endPanelText.text = "All Stage Clear !";
        }
        else {
            // Can go to the next stage
            endPanelText.text = "Clear Stage " + currentStage + " !";
        }
        
        if(currentStage < 3){
            nextStageButton.SetActive(true);
        }
    }


    /********************************************
     * Function : ShowEndPanel()
     * descrition : 
     *  - Call the NoticeToServer() method for sending play information. 
     *  - Result panel set active.
     *  - Update player information.
     ********************************************/
    private void ShowEndPanel()
    {
        NoticeToServer();

        gameOverPanel.SetActive(true);
        movementJoystick.gameObject.SetActive(false);
        shootingJoystick.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        finalScoreText.text = "Score: " + Score.ToString();
        finalMoneyText.text = "Money: " + Money.ToString();

        PlayerInformation.money += Money;
        PlayerInformation.score += Score;
    }


    /********************************************
     * Function : RemoveEnemy(GameObject o)
     * descrition : Remove the enemy object as a parameter.
     ********************************************/
    public void RemoveEnemy(GameObject o)
    {
        currentEnemies.Remove(o);
    }


    /********************************************
    * Function : NoticeToServer()
    * descrition : The score, money, and stage information of the stage is sent to the server by the PUT method.
    ********************************************/
    private void NoticeToServer()
    {
        int updatedStage = PlayerInformation.clearedStage;
        if (!IsGameOver) {
            int cs = PlayerInformation.currentStage;
            int cleardMaxStage = PlayerInformation.clearedStage;

            switch (cs) {
                case 3:
                    updatedStage = 3;
                    break;
                case 2:
                    if (cleardMaxStage < 2) updatedStage = 2;
                    break;
                case 1:
                    if (cleardMaxStage < 1) updatedStage = 1;
                    break;
                default:
                    break;
            }
        }
        
        StageUser updatedUser = new StageUser {
            score = Score,
            money = Money,
            stage = updatedStage,
            skin = PlayerInformation.selectedSkin
        };

        string jwt_token = PlayerInformation.token;
        currentRequest = new RequestHelper
        {
            Uri = basePath + "/user",
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + jwt_token }
            },
            Body = updatedUser,
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Put<ServerResponse>(currentRequest)
        .Then(res => {
            Debug.Log("Success Put!");
            //EditorUtility.DisplayDialog("Success", JsonUtility.ToJson(res, true), "Ok");
        })
        .Catch(err => {
            Debug.Log(err.ToString());
            // EditorUtility.DisplayDialog("error", err.Message, "Ok");
        });
    }


    /********************************************
    * Function : PlayBossFlow()
    * descrition : After some time, instantiate the boss.
    ********************************************/
    IEnumerator PlayBossFlow()
    {
        yield return new WaitForSeconds(15.0f);
        IsBossTime = true;
        
        bossHpCanvas.gameObject.SetActive(true);
        Instantiate(bosses[Type], new Vector3(0, 2.85f, 0), new Quaternion(0, 0, 180, 0));
    }


    /********************************************
    * Function : PlayGameFlow()
    * descrition : Instantiate the enemy with time flow.
    ********************************************/
    IEnumerator PlayGameFlow()
    {
        while (true)
        {
            if(IsGameOver || IsBossTime) break;
            int num = 5;
            for(int i=0; i< num; i++)
            {
                GameObject currentEnemy = Instantiate(enemies[Type], 
                    gameObject.transform.position, new Quaternion(0, 0, 180, 0));
                currentEnemies.Add(currentEnemy);
                
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(5.0f);
        }
    }


    /********************************************
    * Function : NextGame()
    * descrition : If the player defeats the boss, go to the next stage.
    ********************************************/
    public void NextGame()
    {
        PlayerInformation.currentStage += 1;
        SceneManager.LoadScene("PlayScene");
    }


    /********************************************
    * Function : ReGame()
    * descrition : Re start the play scene.
    ********************************************/
    public void ReGame()
    {
        SceneManager.LoadScene("PlayScene");
    }


    /********************************************
    * Function : OnClickBack()
    * descrition : Go to the MenuScene
    ********************************************/
    public void OnClickBack()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
