using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{

    public void OnClickExit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
