using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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


    void Start()
    {
        skinNum = 5;
        initStore();
        UpdateView();
    }


    private void initStore()
    {
        int[] ableFlight = PlayerInformation.canSelectFlight;
        int ranking = PlayerInformation.ranking;
        selectedFlight = PlayerInformation.selectedFlight;
        selectedSkin = PlayerInformation.selectedSkin;


        // Show the player's ranking with text.
        rankingText.text = ranking.ToString();

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
                for (int j=0; j<skinNum; j++) {
                    if(skinNum-ranking < j) {
                        flightSkinImages[skinNum*i+j].color = Color.black;
                        flightSkinButton[skinNum*i+j].interactable = false;
                    }
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
