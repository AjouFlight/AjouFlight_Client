using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Models;
using Proyecto26;


public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private Image[] flightBasicImages;
    [SerializeField]
    private Image[] flightSkinImages;
    [SerializeField]
    private Button[] flightBasicButton;
    [SerializeField]
    private Button[] flightSkinButton;

    [SerializeField]
    private Image viewCharacterImage;

    [SerializeField]
    private Text rankingText;

    private int selectedFlight=0;
    private int selectedSkin=0;
    private int skinNum=0;

    private RequestHelper currentRequest;
    private const string basePath = "http://ec2-3-36-132-39.ap-northeast-2.compute.amazonaws.com";


    void Start()
    {
        skinNum = 5;
        selectedFlight = PlayerInformation.selectedFlight;
        selectedSkin = PlayerInformation.selectedSkin;

        InitStore();
        UpdateView();
    }


    private void PutInformationToServer()
    {
        StageUser updatedUser = new StageUser
        {
            score = 0,
            money = 0,
            stage = PlayerInformation.clearedStage,
            skin = selectedSkin
        };

        string jwt_token = PlayerInformation.token;
        currentRequest = new RequestHelper
        {
            Uri = basePath + "/user",
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + jwt_token }
            },
            Body = updatedUser,
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Put<ServerResponse>(currentRequest)
        .Then(res => {
            Debug.Log("Success Put!");
            //EditorUtility.DisplayDialog("Success", JsonUtility.ToJson(res, true), "Ok");
        })
        .Catch(err =>
        {
            Debug.Log(err.ToString());
            // EditorUtility.DisplayDialog("error", err.Message, "Ok");
        });

    }


    private void InitStore()
    {
        int[] ableFlight = PlayerInformation.canSelectFlight;
        int ranking = PlayerInformation.ranking;
        selectedFlight = PlayerInformation.selectedFlight;
        selectedSkin = PlayerInformation.selectedSkin;

        int numFlights = ableFlight.Length;

        // Show the player's ranking percentage with text.
        rankingText.text = ranking.ToString() + " %";

        // Check if the player has flights.
        // Check the player's ranking so that can select skins.
        for (int i=0; i<ableFlight.Length; i++) {
            if(ableFlight[i] == 0) {
                flightBasicImages[i].color = Color.black;
                flightBasicButton[i].interactable = false;
                for(int j=0; j<skinNum; j++) {
                    flightSkinImages[skinNum*i+j].color = Color.black;
                    flightSkinButton[skinNum*i+j].interactable = false;
                }
            }
            else {
                int maxSkin = ranking / (skinNum * numFlights);
                for (int j=0; j< maxSkin; j++) {
                    flightSkinImages[skinNum*i+(skinNum-1-j)].color = Color.black;
                    flightSkinButton[skinNum*i+(skinNum-1-j)].interactable = false;
                }
            }
        }
    }


    public void UpdateView()
    {
        if (selectedSkin < 0)
            viewCharacterImage.sprite = flightBasicImages[selectedFlight].sprite;
        else
            viewCharacterImage.sprite = flightSkinImages[selectedSkin].sprite;
    }


    public void SelectFlight(int flightIndex)
    {
        selectedFlight = flightIndex;
        selectedSkin = -1;
        UpdateView();
    }


    public void SelectSkin(int skinIndex)
    {
        selectedSkin = skinIndex;
        selectedFlight = skinIndex / skinNum;
        PutInformationToServer();
        UpdateView();
    }


    private void UpdatePlayerInformation()
    {
        PlayerInformation.selectedFlight = selectedFlight;
        PlayerInformation.selectedSkin = selectedSkin;
    }


    public void OnClickExit()
    {
        UpdatePlayerInformation();
        SceneManager.LoadScene("MenuScene");
    }
}
