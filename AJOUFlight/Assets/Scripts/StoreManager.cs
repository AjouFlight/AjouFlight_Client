using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private Button[] flightButtons;

    [SerializeField]
    private Text moneyText;

    [SerializeField]
    private Text storeText;

    private double money;
    private int[] canSelectFlights;


    void Start()
    {
        initStore();
    }


    public void initStore()
    {
        canSelectFlights = PlayerInformation.canSelectFlight;
        money = PlayerInformation.money;

        storeText.text = "Store";
        moneyText.text = money.ToString();

        for (int i=1; i<canSelectFlights.Length; i++) {
            if (canSelectFlights[i] == 1)
                flightButtons[i-1].interactable = false;
        }
    }


    public void Purchase(int flightIndex) // 1,2,3
    {
        if((flightIndex+1)*100 <= money) {
            money -= (flightIndex + 1) * 100;
            canSelectFlights[flightIndex] = 1;
            flightButtons[flightIndex-1].interactable = false;
            storeText.text = "Purchase the Flight" + (flightIndex+1).ToString() + " !";
            moneyText.text = money.ToString();
            UpdatePlayerInformation();
        }
        else {
            storeText.text = "Not enough money";
        }
    }


    private void UpdatePlayerInformation()
    {
        PlayerInformation.money = money;
        PlayerInformation.canSelectFlight = canSelectFlights;
    }


    public void OnClickExit()
    {
        UpdatePlayerInformation();
        SceneManager.LoadScene("MenuScene");
    }
}
