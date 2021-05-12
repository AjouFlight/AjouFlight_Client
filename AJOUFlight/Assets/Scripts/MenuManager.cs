using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    void Start()
    {
        welcomeText.text = "Welcome " + PlayerInformation.playerID + "!" ;
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
        for(int i=0; i<clearedStage; i++)
        {
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
