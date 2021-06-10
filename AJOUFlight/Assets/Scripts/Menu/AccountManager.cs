using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using UnityEditor;
using Models;

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

    // YoungWoo's server
    // private const string basePath = "http://ec2-13-209-72-98.ap-northeast-2.compute.amazonaws.com";

    // JinSeok's server
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

        
        User user = new User
        {
            userId = idText,
            password = passwordText
        };

        currentRequest = new RequestHelper
        {
            Uri = basePath + "/login",
            Body = user,
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Post<MenuResponse>(currentRequest)
        .Then(res =>
        {
            Debug.Log(res.ToString());

            loginText.text = "Welcome\n" + user.userId;
            
            PlayerInformation.playerID = user.userId;
            PlayerInformation.token = res.data.token;
            PlayerInformation.clearedStage = res.data.stage - 1;
            PlayerInformation.score = res.data.score;
            PlayerInformation.money = res.data.money;
            PlayerInformation.selectedSkin = res.data.skin;

            for(int i=0; i<res.data.flights.Length; i++)
            {
                int index = res.data.flights[i];
                PlayerInformation.canSelectFlight[index] = 1;
            }

            int skinIndex = PlayerInformation.selectedSkin;
            if(skinIndex >= 0) {
                PlayerInformation.selectedFlight = skinIndex / 5;
            }

            // EditorUtility.DisplayDialog("Success", JsonUtility.ToJson(res, true), "Ok");
            SceneManager.LoadScene("MenuScene");
        })
        .Catch(err =>
        {
            loginText.text = "Invalid Account.";
            Debug.Log(err.ToString());
            // EditorUtility.DisplayDialog("error", err.Message, "Ok");
        });
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
