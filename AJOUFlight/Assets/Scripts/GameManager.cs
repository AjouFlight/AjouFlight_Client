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

    private int score;
    private double money;
    private int stage;
    private bool isGameOver;

    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private GameObject[] bosses;
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
    private Sprite[] skins;

    [SerializeField]
    private Canvas bossHpCanvas;
    [SerializeField]
    private Text endPanelText;
    [SerializeField]
    private GameObject nextStageButton;


    // For Test. there will be removed.
    public int type = 0;
    public Text scoreText;
    public Text hpText;
    public bool bossTime;

    private GameObject player;
    private List<GameObject> currentEnemies = new List<GameObject>();

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


    void Awake()
    {
        InitPlayer();
    }


    void Start()
    {
        type = PlayerInformation.currentStage - 1;
        bossTime = false;
        IsGameOver = false;

        StartCoroutine(PlayBossFlow());
        StartCoroutine(PlayGameFlow());
    }
    

    void Update()
    {
        if (IsGameOver) return;

        scoreText.text = "Score: " + score.ToString();
    }


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


    public void AddScore(int score)
    {
        Score += score;
    }

    public void AddGameMoney(double money)
    {
        Money += money;
    }


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
            endPanelText.text = "All Stage Clear !";
        }
        else {
            endPanelText.text = "Clear Stage " + currentStage + " !";
        }
        
        if(currentStage < 3){
            nextStageButton.SetActive(true);
        }
    }


    public void ShowEndPanel()
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


    public void RemoveEnemy(GameObject o)
    {
        currentEnemies.Remove(o);
    }


    public void NoticeToServer()
    {
        int updatedStage = 0;
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

        StageUser updatedUser = new StageUser{
            score = Score,
            money = PlayerInformation.money,
            stage = updatedStage
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
            EditorUtility.DisplayDialog("Success", JsonUtility.ToJson(res, true), "Ok");
        })
        .Catch(err =>
        {
            Debug.Log(err.ToString());
            EditorUtility.DisplayDialog("error", err.Message, "Ok");
        });
    }


    IEnumerator PlayBossFlow()
    {
        yield return new WaitForSeconds(10.0f);
        bossTime = true;
        
        bossHpCanvas.gameObject.SetActive(true);
        GameObject boss = Instantiate(bosses[type], new Vector3(0, 2.85f, 0), new Quaternion(0, 0, 180, 0));
        
    }

    IEnumerator PlayGameFlow()
    {
        while (true)
        {
            if(IsGameOver || bossTime) break;
            int num = 5;
            for(int i=0; i< num; i++)
            {
                GameObject currentEnemy = Instantiate(enemies[type], 
                    gameObject.transform.position, new Quaternion(0, 0, 180, 0));
                currentEnemies.Add(currentEnemy);
                
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(5.0f);
        }
    }


    public void NextGame()
    {
        PlayerInformation.currentStage += 1;
        SceneManager.LoadScene("PlayScene");
    }


    public void ReGame()
    {
        SceneManager.LoadScene("PlayScene");
    }


    public void OnClickBack()
    {
        SceneManager.LoadScene("MenuScene");
    }

    

}
