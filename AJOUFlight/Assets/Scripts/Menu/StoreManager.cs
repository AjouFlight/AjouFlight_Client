using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Models;
using Proyecto26;
using UnityEditor;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private Button[] flightButtons;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private Text storeText;

    private double gameMoney;
    private int[] canSelectFlights = {0, 0, 0, 0};

    private RequestHelper currentRequest;
    private const string basePath = "http://ec2-3-36-132-39.ap-northeast-2.compute.amazonaws.com";

    void Start()
    {
        InitStore();
    }


    public void InitStore()
    {
        canSelectFlights = PlayerInformation.canSelectFlight;
        gameMoney = PlayerInformation.money;

        storeText.text = "Store";
        moneyText.text = gameMoney.ToString();

        for (int i=1; i<canSelectFlights.Length; i++) {
            if(canSelectFlights[i] == 1) {
                flightButtons[i-1].interactable = false;
            }
        }
    }


    public void Purchase(int flightIndex) // 1,2,3
    {
        double price = (flightIndex+1)*100;
        
        if (price <= gameMoney) {
            gameMoney -= price;
            canSelectFlights[flightIndex] = 1;
            flightButtons[flightIndex-1].interactable = false;
            storeText.text = "Purchase the Flight" + (flightIndex+1).ToString() + " !";
            moneyText.text = gameMoney.ToString();

            PostFlightsToServer(flightIndex, price);
            UpdatePlayerInformation();
        }
        else {
            storeText.text = "Not enough money";
        }
    }


    private void PostFlightsToServer(int id, double m)
    {
        string jwt_token = PlayerInformation.token;

        StoreFlight newFlight = new StoreFlight
        {
            flightId = id,
            money = m
        };

        currentRequest = new RequestHelper
        {
            Uri = basePath + "/user/flight",
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + jwt_token }
            },
            Body = newFlight,
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Post<ServerResponse>(currentRequest)
        .Then(res => {
            Debug.Log("Success Post!");
            EditorUtility.DisplayDialog("Success", JsonUtility.ToJson(res, true), "Ok");
        })
        .Catch(err =>
        {
            Debug.Log(err.ToString());
            EditorUtility.DisplayDialog("error", err.Message, "Ok");
        });
    }


    private void UpdatePlayerInformation()
    {
        PlayerInformation.money = gameMoney;
        PlayerInformation.canSelectFlight = canSelectFlights;
    }


    public void OnClickExit()
    {
        UpdatePlayerInformation();
        SceneManager.LoadScene("MenuScene");
    }
}
