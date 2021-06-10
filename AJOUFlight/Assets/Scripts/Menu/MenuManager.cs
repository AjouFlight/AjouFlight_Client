using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using Proyecto26;
using Models;
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
    [SerializeField]
    private GameObject audioPanel;

    [SerializeField]
    private AudioMixer myMixer;
    [SerializeField]
    private Slider BGMSlider;
    [SerializeField]
    private Slider SFXSlider;

    private int totalUserNum;
    private int myRanking;

    private RequestHelper currentRequest;
    private const string basePath = "http://ec2-3-36-132-39.ap-northeast-2.compute.amazonaws.com";


    void Start()
    {
        welcomeText.text = "Welcome " + PlayerInformation.playerID + "!" ;
        GetRankingDataFromServer();
    }

    void Update()
    {
        AudioControl();
    }


    private void GetRankingDataFromServer()
    {
        string jwt_token = PlayerInformation.token;

        currentRequest = new RequestHelper
        {
            Uri = basePath + "/user/ranking",
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + jwt_token }
            },
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Get<RankingResponse>(currentRequest).Then(tenUsers =>
        {
            List<Dictionary<string, int>> tmpTen = new List<Dictionary<string, int>>();

            foreach (RankingUser ru in tenUsers.data.top10)
            {
                Dictionary<string, int> d = new Dictionary<string, int>();
                string uId = ru.userId;
                int uScore = ru.score;
                d[uId] = uScore;
                tmpTen.Add(d);
            }

            PlayerInformation.tenUserlist = tmpTen;

            totalUserNum = tenUsers.data.totalNum;
            myRanking = tenUsers.data.myRanking;
            if (PlayerInformation.score == 0) {
                PlayerInformation.ranking = 100;
            }
            else {
                PlayerInformation.ranking = (int)(((float)myRanking / totalUserNum) * 100);
            }
            
        }).Catch(error => {
            Debug.Log(error.ToString());
            // EditorUtility.DisplayDialog("Error", error.Message, "Ok");
        });
    }


    private void AudioControl()
    {
        float BGMVolume = BGMSlider.value;
        float SFXVolume = SFXSlider.value;

        myMixer.SetFloat("BGM", BGMVolume);
        myMixer.SetFloat("SFX", SFXVolume);
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
        Debug.Log(clearedStage);
        for(int i=0; i<clearedStage+1; i++)
        {
            if (i == 3) break;
            stageButtons[i].interactable = true;
        }
    }

    public void HidePanels()
    {
        stagePanel.SetActive(false);
        audioPanel.SetActive(false);
        outsideButton.SetActive(false);
    }


    public void HideAudioPanel()
    {
        audioPanel.SetActive(false);
        outsideButton.SetActive(false);
    }

    public void ShowAudioPanel()
    {
        audioPanel.SetActive(true);
        outsideButton.SetActive(true);
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
