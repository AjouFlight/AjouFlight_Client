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

    private bool bCompleted = false;

    private const string basePath = "http://ec2-3-36-132-39.ap-northeast-2.compute.amazonaws.com";


    void Start()
    {
        tenUserlist = new List<Dictionary<string, int>>();
        bCompleted = false;
        GetDataFromServer();
    }

    void Update()
    {

    }

    public void GetDataFromServer()
    {
        RestClient.Get<RankingResponse>(basePath + "/user/ranking").Then(tenUsers =>
        {
            Debug.Log("Get: Get top 10 player information");

            bCompleted = true;
            
            foreach(RankingUser ru in tenUsers.data)
            {
                Dictionary<string, int> d = new Dictionary<string, int>();
                string uId = ru.userId;
                int uScore = ru.score;
                d[uId] = uScore;
                tenUserlist.Add(d);
            }
            
            ShowRanking();

        }).Catch(error => {
            bCompleted = false;
            EditorUtility.DisplayDialog("Error", error.Message, "Ok");
        });
    }


    public void ShowRanking()
    {
        if (!bCompleted) return;

        for (int i = 0; i < tenUserlist.Count; i++){
            foreach(KeyValuePair<string, int> kv in tenUserlist[i]){
                userIDText[i].text = "User ID: " + kv.Key;
                scoreText[i].text = "Score: " + kv.Value.ToString();

                if (kv.Key.CompareTo(PlayerInformation.playerID) == 0) {
                    userIDText[i].color = Color.blue;
                    scoreText[i].color = Color.blue;

                    PlayerInformation.ranking = i;
                }
            }
        }
    }


    public void OnClickExit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
