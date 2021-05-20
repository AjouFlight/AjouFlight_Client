using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;
using Models;


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

    private const string basePath = "http://ec2-13-209-72-98.ap-northeast-2.compute.amazonaws.com";


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
        player = Instantiate(players[flightIndex].gameObject, Vector3.zero, transform.rotation);

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


    public void GameOver()
    {
        IsGameOver = true;

        Destroy(player.gameObject);
        foreach (GameObject enemy in currentEnemies) {
            Destroy(enemy);
        }

        ShowEndPanel();
    }


    public void BossDead()
    {
        PlayerInformation.clearedStage = PlayerInformation.currentStage;

        Destroy(player.gameObject);
        foreach (GameObject enemy in currentEnemies) {
            Destroy(enemy);
        }
        
        ShowEndPanel();
        endPanelText.text = "Clear Stage " + PlayerInformation.currentStage + " !";
        nextStageButton.SetActive(true);
    }


    public void ShowEndPanel()
    {
        NoticeToServer();
        gameOverPanel.SetActive(true);
        movementJoystick.gameObject.SetActive(false);
        shootingJoystick.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        finalMoneyText.text = "Score: " + Money.ToString();
        finalScoreText.text = "Money: " + Score.ToString();
    }


    public void RemoveEnemy(GameObject o)
    {
        currentEnemies.Remove(o);
    }


    public void NoticeToServer()
    {
        StageUser updatedUser = new StageUser {
            score = Score,
            money = Money
        };
        
        if (!IsGameOver) {
            int cs = PlayerInformation.currentStage;
            switch (cs)
            {
                case 3:
                    updatedUser.stage3 = true;
                    updatedUser.stage2 = true;
                    updatedUser.stage1 = true;
                    break;
                case 2:
                    updatedUser.stage2 = true;
                    updatedUser.stage1 = true;
                    break;
                case 1:
                    updatedUser.stage1 = true;
                    break;
                default:
                    break;
            }
        }


        RestClient.Put<ServerResponse>(basePath + "/user", updatedUser).Then(customResponse => {
            //UnityEditor.EditorUtility.DisplayDialog("JSON", JsonUtility.ToJson(customResponse, true), "Ok");
        }).Catch(err =>
        {
            //UnityEditor.EditorUtility.DisplayDialog("error", err.Message, "Ok");
        });
    }


    public void GoNextStage()
    {
        // Go Next Stage.
    }


    IEnumerator PlayBossFlow()
    {
        yield return new WaitForSeconds(10.0f);
        bossTime = true;

        bossHpCanvas.gameObject.SetActive(true);
        Instantiate(bosses[type], new Vector3(0, 2.85f, 0), new Quaternion(0, 0, 180, 0));

    }

    IEnumerator PlayGameFlow()
    {
        while (true)
        {
            if(IsGameOver || bossTime) break;

            for(int i=0; i< 5; i++)
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
        // next scene...
        SceneManager.LoadScene("PlayScene");
    }


    public void ReGame()
    {
        SceneManager.LoadScene("PlayScene");
    }


    // For prototype. It will be removed.
    public void OnClickBack()
    {
        SceneManager.LoadScene("MenuScene");
    }

    

}
