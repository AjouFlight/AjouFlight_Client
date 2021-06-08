using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Models;
using Proyecto26;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject stagePanel;
    [SerializeField]
    private Button[] stageButtons;
    [SerializeField]
    private GameObject outsideButton;
    [SerializeField]
    private Text welcomeText;


    private const string basePath = "http://ec2-3-36-132-39.ap-northeast-2.compute.amazonaws.com";

    void Start()
    {
        welcomeText.text = "Welcome " + PlayerInformation.playerID + "!" ;
        
    }


    private void InitPlayer()
    {
        string token = PlayerInformation.token;

        RestClient.Get<MenuResponse>(basePath + "/user/" + token).Then(user =>
        {
            PlayerInformation.score = user.data.score;
            PlayerInformation.canSelectFlight = user.data.flights;
            PlayerInformation.money = user.data.money;

            if (user.data.stage == 3) PlayerInformation.clearedStage = 3;
            else if (user.data.stage == 2) PlayerInformation.clearedStage = 2;
            else if (user.data.stage == 1) PlayerInformation.clearedStage = 1;
            else PlayerInformation.clearedStage = 0;

        }).Catch(error => {
            Debug.Log("fail init Player!");
            EditorUtility.DisplayDialog("Error", error.Message, "Ok");
        });

    }


    public void ShowStagePanel()
    {
        SetAbleStageButtons();
        stagePanel.SetActive(true);
        outsideButton.SetActive(true);
    }

    public void HideStagePanel()
    {
        stagePanel.SetActive(false);
        outsideButton.SetActive(false);
    }

    public void SetAbleStageButtons()
    {
        int clearedStage = PlayerInformation.clearedStage;
        for(int i=0; i<clearedStage+1; i++)
        {
            if (i == 3) break;
            stageButtons[i].interactable = true;
        }
    }

    public void SelectStage(int stageIndex)
    {
        PlayerInformation.currentStage = stageIndex;
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickPlay()
    {
        ShowStagePanel();
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
