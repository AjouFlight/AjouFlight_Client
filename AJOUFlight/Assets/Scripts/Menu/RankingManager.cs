using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Models;
using Proyecto26;
using UnityEditor;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private Text[] userIDText;
    [SerializeField]
    private Text[] scoreText;

    private List<Dictionary<string, int>> tenUserlist;


    void Start()
    {
        tenUserlist = PlayerInformation.tenUserlist;
        ShowRanking();
    }


    public void ShowRanking()
    {
        for (int i = 0; i < 10; i++){
            foreach(KeyValuePair<string, int> kv in tenUserlist[i]){
                userIDText[i].text = "User ID: " + kv.Key;
                scoreText[i].text = "Score: " + kv.Value.ToString();

                if (kv.Key.CompareTo(PlayerInformation.playerID) == 0) {
                    userIDText[i].color = Color.blue;
                    scoreText[i].color = Color.blue;
                }
            }
        }
    }


    public void OnClickExit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
