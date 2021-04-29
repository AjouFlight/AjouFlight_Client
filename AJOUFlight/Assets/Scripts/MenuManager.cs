using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{



    public void OnClickPlay()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickRoom()
    {
        SceneManager.LoadScene("RoomScene");
    }

    public void OnClickRanking()
    {
        SceneManager.LoadScene("RankingScene");
    }

    public void OnClickStore()
    {
        SceneManager.LoadScene("StoreScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
