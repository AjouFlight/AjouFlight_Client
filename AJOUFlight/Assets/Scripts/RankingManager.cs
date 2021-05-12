using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private Text[] userIDText;
    [SerializeField]
    private Text[] scoreText;


    public void GetDataFromServer()
    {
        // ...
    }


    public void ShowRanking()
    {
        // ...
    }


    public void OnClickExit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
