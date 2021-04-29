using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomManager : MonoBehaviour
{
    
    public void GetDataFromServer()
    {
        // ...
    }


    public void SelectFlight(int flightIndex)
    {
        // ...
    }


    public void SelectSkin(int skinIndex)
    {
        // ...
    }


    public void OnClickExit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
