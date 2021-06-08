using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using UnityEditor;
using Models;
using System.Net;
using System.IO;


public class AccountManager : MonoBehaviour 
{
    // Log In
    [SerializeField]
    private Text loginText;
    [SerializeField]
    private InputField idInputField;
    [SerializeField]
    private InputField passwordInputField;

    // Sign Up
    [SerializeField]
    private Text signUpText;
    [SerializeField]
    private InputField signUpIdInputField;
    [SerializeField]
    private InputField signUpPasswordInputField;
    [SerializeField]
    private InputField verifyPasswordInputField;

    [SerializeField]
    private Canvas signInCanvas;
    [SerializeField]
    private Canvas signUpCanvas;

    private RequestHelper currentRequest;

    // private const string basePath = "http://ec2-13-209-72-98.ap-northeast-2.compute.amazonaws.com";
    private const string basePath = "http://ec2-3-36-132-39.ap-northeast-2.compute.amazonaws.com";


    public void OnClickEnter()
    {
        string idText = idInputField.text;
        string passwordText = passwordInputField.text;

        if (idText.CompareTo("") == 0) {
            loginText.text = "Please check \nyour ID";
            return;
        }

        if (passwordText.CompareTo("") == 0)
        {
            loginText.text = "Please check \nyour Password";
            return;
        }


        var httpWebRequest = (HttpWebRequest)WebRequest.Create(basePath + "/login");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = 
                "{\"userId\":\"" + idText + "\"," +
                 "\"password\":\"" + passwordText + "\"}";
            
            streamWriter.Write(json);
        }

        try
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Debug.Log(result);

                string tmp = "";
                int index = result.IndexOf("token");
                for (int i = 8; i < 200; i++)
                {
                    if (result[index + i + 1] == ',') {
                        break;
                    }
                    tmp += result[index + i];
                }
                PlayerInformation.token = tmp;
                tmp = "";

                index = result.IndexOf("stage");
                int stageNum = result[index + 7] - '0';
                PlayerInformation.clearedStage = stageNum - 1;

                index = result.IndexOf("score");
                for (int i = 7; i < 20; i++)
                {
                    if (result[index + i] == ',') break;
                    tmp += result[index + i];
                }
                PlayerInformation.score = int.Parse(tmp);
                tmp = "";

                index = result.IndexOf("money");
                for (int i = 7; i < 20; i++)
                {
                    if (result[index + i] == ',') break;
                    tmp += result[index + i];
                }
                PlayerInformation.money = int.Parse(tmp);
                Debug.Log(tmp);
                tmp = "";

                index = result.IndexOf("flights");
                for (int i = 10; i < 200; i++)
                {
                    if (result[index + i] == ']') break;
                    tmp += result[index + i];
                }

                if (tmp.Length > 0)
                {
                    string[] words = tmp.Split(',');
                    foreach (string word in words)
                    {
                        int i = word.IndexOf(":");
                        int flightID = word[i+1] - '0';
                        PlayerInformation.canSelectFlight[flightID] = 1;
                    }
                }
                else
                {
                    PlayerInformation.canSelectFlight[0] = 1;
                }

                PlayerInformation.playerID = idText;
                SceneManager.LoadScene("MenuScene");
            }
        } catch (WebException e)
        {
            Debug.Log(e.ToString());
            loginText.text = "Invalid account.";
        }
        
        
        /*
        User newUser = new User
        {
            userId = idText,
            password = passwordText
        };


        // case 1. Using the generic request method.
        currentRequest = new RequestHelper
        {
            Uri = basePath + "/login",
            Body = newUser,
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Post<ServerResponse>(currentRequest)
        .Then(res =>
        {
            loginText.text = "Welcome\n" + newUser.userId;
            PlayerInformation.playerID = newUser.userId;
            //PlayerInformation.token = res.token;

            Debug.Log(res.ToString());

            EditorUtility.DisplayDialog("Success", JsonUtility.ToJson(res, true), "Ok");
            SceneManager.LoadScene("MenuScene");
        })
        .Catch(err =>
        {
            loginText.text = "No Valid Account.";
            EditorUtility.DisplayDialog("error", err.Message, "Ok");
        });
        */

        // case 2. Using the short request method (ToDoList: select the case 1 or 2)
        /*
        RestClient.Post<ServerResponse>(basePath + "/login", newUser).Then(response =>
        {
            EditorUtility.DisplayDialog("token: ", response.token, "Ok");
            EditorUtility.DisplayDialog("Message: ", response.message, "Ok");
        }).Catch(err => EditorUtility.DisplayDialog("error", err.Message, "Ok"));
        */

    }



    public void OnClickSignUpOK()
    {
        string idText = signUpIdInputField.text;
        string passwordText = signUpPasswordInputField.text;
        string verifyingText = verifyPasswordInputField.text;

        if(idText.CompareTo("") == 0) {
            signUpText.text = "Please check \nyour ID";
            return ;
        }

        if (passwordText.CompareTo("") == 0)
        {
            signUpText.text = "Please check \nyour Password";
            return;
        }

        // If the password and verifying password not same.
        if (VerifyPassword(passwordText, verifyingText) == false) {
            signUpText.text = "Please check \nyour verify password.";
            return ;
        }


        User newUser = new User
        {
            userId = idText,
            password = passwordText
        };

        currentRequest = new RequestHelper
        {
            Uri = basePath + "/user/register",
            Body = newUser,
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Post<SignUpResponse>(currentRequest)
        .Then(res => {
            signUpText.text = "Success Sign Up!";
            // EditorUtility.DisplayDialog("Success", JsonUtility.ToJson(res, true), "Ok");
            
            signInCanvas.gameObject.SetActive(true);
            signUpCanvas.gameObject.SetActive(false);
            loginText.text = "Success Sign Up!";
        })
        .Catch(err =>
        {
            signUpText.text = "No Valid Account.";
            // EditorUtility.DisplayDialog("error", err.Message, "Ok");
        });
    }


    public void OnClickSignUp()
    {
        signInCanvas.gameObject.SetActive(false);
        signUpCanvas.gameObject.SetActive(true);

    }

    public void OnClickSignUp2SignIn()
    {
        signInCanvas.gameObject.SetActive(true);
        signUpCanvas.gameObject.SetActive(false);
    }


    public bool VerifyPassword(string sourcePassword, string verifyingPassword)
    {
        if(sourcePassword.CompareTo(verifyingPassword) == 0) 
            return true;
        return false;
    }


    public void OnClickTest()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
