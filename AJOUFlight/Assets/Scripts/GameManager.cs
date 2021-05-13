using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private MovementJoystick movementJoystick;
    [SerializeField]
    private ShootingJoystick shootingJoystick;
    [SerializeField]
    private Text finalScoreText;
    [SerializeField]
    private Text finalMoneyText;
    [SerializeField]
    private GameObject gameOverPanel;

    // For Test. there will be removed.
    public int type = 0;
    public Text scoreText;
    public Text hpText;

    private GameObject player;
    private List<Enemy> currentEnemies;


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
        IsGameOver = false;
        StartCoroutine(playGameFlow());
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
        if (flightIndex == 0)
        {
            player.GetComponent<FlightA>().MoveJoystick = movementJoystick;
            player.GetComponent<FlightA>().Shootjoystick = shootingJoystick;
        }
        else if (flightIndex == 1)
        {
            player.GetComponent<FlightB>().MoveJoystick = movementJoystick;
            player.GetComponent<FlightB>().Shootjoystick = shootingJoystick;
        }
        else if (flightIndex == 2)
        {
            player.GetComponent<FlightC>().MoveJoystick = movementJoystick;
            player.GetComponent<FlightC>().Shootjoystick = shootingJoystick;
        }
        else if (flightIndex == 3)
        {
            player.GetComponent<FlightD>().MoveJoystick = movementJoystick;
            player.GetComponent<FlightD>().Shootjoystick = shootingJoystick;
        }
        else
        {
            Debug.Log("Invalid selected flight");
        }
        
    }


    public void AddScore(int score)
    {
        Score += score;
    }


    public void GameOver()
    {
        movementJoystick.gameObject.SetActive(false);
        shootingJoystick.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        IsGameOver = true;
        gameOverPanel.SetActive(true);
        finalMoneyText.text = "Score: " + Money.ToString();
        finalScoreText.text = "Money: " + Score.ToString();



    }


    



    public void GoNextStage()
    {
        // Go Next Stage.
    }


    IEnumerator playGameFlow()
    {
        while (true)
        {
            if(IsGameOver) break;

            Instantiate(enemies[type], gameObject.transform.position, new Quaternion(0,0,180, 0));

            yield return new WaitForSeconds(5.0f);
        }
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
