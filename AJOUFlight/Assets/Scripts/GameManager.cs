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

    
    public int score = 0;
    public int stage = 1;
    public bool isGameOver;

    public GameObject[] enemies;
    public GameObject[] players;


    // For prototype. there will be removed.
    public int type = 0;
    public Text scoreText;
    public Text hpText;
    public Player player;


    void Start()
    {
        StartCoroutine(playGameFlow());
    }
    

    void Update()
    {
        scoreText.text = score.ToString();
        hpText.text = player.Hp.ToString();
    }


    public void AddScore(int score)
    {
        this.score += score;
    }


    public void GameOver()
    {
        // GameOver.
    }


    public void GoNextStage()
    {
        // Go Next Stage.
    }


    IEnumerator playGameFlow()
    {
        while (true)
        {
            // For prototype. It will be changed.
            Instantiate(enemies[type], gameObject.transform.position, gameObject.transform.rotation);

            yield return new WaitForSeconds(5.0f);

            if (type < 2) type++;
            else type = 0;
        }
    }






    // For prototype. It will be removed.
    public void OnClickBack()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
