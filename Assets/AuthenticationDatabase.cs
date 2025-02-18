using Photon.Pun;
using PlayFab;

using PlayFab.ClientModels;
using PlayFab.EconomyModels;
using PlayFab.GroupsModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class AuthenticationDatabase : MonoBehaviour
{
    #region Variables
    [Header("Sign Up things")]
  
    public TMP_InputField emailFieldSign;
    public TMP_InputField UsernameSign;
    public TMP_InputField passwordFieldSign;
    public TMP_InputField passwordFieldConfirmSign;
    public TMP_Text messageTextSign; // Display messages to the user
    [Header("=====================================================================================")]

    [Header("Login In things")]
    public TMP_InputField emailFieldLoginLogin;
    public TMP_InputField passwordFieldLogin;
    public TMP_Text messageTextLogin; // Display messages to the user
    public GameObject RequestPanel;
    public GameObject LoginPage;
    public TMP_Text RequestJoinMessage;
    public GameObject JoinRooms;
    [Header("=====================================================================================")]


    [Header("Login In Admin")]
    public TMP_InputField emailFieldAdmin;
    public TMP_InputField passwordFieldAdmin;
    public TMP_Text messageTextAdmin; // Display messages to the user
    public string AdminEmail;
    public GameObject CreateRooms;
    public GameObject AdminLogin;

    public string playfabID;


    #endregion



    #region Start

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            Debug.LogError("PlayFab Title ID is not set. Please configure it in the PlayFab settings.");
        }
        else
        {
            Debug.Log("PlayFab Title ID: " + PlayFabSettings.TitleId);
        }


    }

    #endregion

    #region Register
    // Sign up a new user with PlayFab
    public void RegisterUser()
    {
        if(passwordFieldSign.text== passwordFieldConfirmSign.text)
        {
            var registerRequest = new RegisterPlayFabUserRequest
            {
                DisplayName = UsernameSign.text,
                Username = UsernameSign.text,
                Email = emailFieldSign.text,
                Password = passwordFieldSign.text,
                RequireBothUsernameAndEmail = true
                
            };

            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
        }
        else
        {
            messageTextSign.text = "Password doesn't match";
        }
    
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
       // SaveSharedGroupData("CustomGroupGlobal","PlayfabID",result.PlayFabId);
        Debug.Log("Register Success!");
        messageTextSign.text = "Registration Successful!";
        
     
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError("Register Failed: " + error.GenerateErrorReport());
        messageTextSign.text = "Registration Failed: " + error.ErrorMessage;
    }
  
    #endregion



    #region Login

    // Log in with PlayFab
    public string MinePlayfabUserID;
    public void LoginUser()
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            Username = emailFieldLoginLogin.text,
            Password = passwordFieldLogin.text
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        PhotonNetwork.NickName = emailFieldLoginLogin.text;
        RequestPanel.SetActive(true);
        LoginPage.SetActive(true);
        Debug.Log("Login Success!");
        messageTextLogin.text = "Login Successful!";
        playfabID = result.PlayFabId;
        MinePlayfabUserID = result.PlayFabId;
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Login Failed: " + error.GenerateErrorReport());
        messageTextLogin.text = "Login Failed: " + error.ErrorMessage;
    }
    #endregion


    #region LoginAdmin
    // Log in with PlayFab
    public void LoginUserAdmin()
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            
            Username = emailFieldAdmin.text,
            Password = passwordFieldAdmin.text
        };
       
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, OnLoginSuccessAdmin, OnLoginFailureAdmin);
    }

    private void OnLoginSuccessAdmin(LoginResult result)
    {
        Debug.Log("User ID correct");

        if (emailFieldAdmin.text==AdminEmail)
        {
            AdminLogin.SetActive(false);
            CreateRooms.SetActive(true);
            PhotonNetwork.NickName = emailFieldAdmin.text;
            Debug.Log("Move forward");
            playfabID = result.PlayFabId;

            GetComponent<AdminAPIDatabase>().GetPlayFabUserIDsFromLeaderboard();

        }
        else
        {

            messageTextAdmin.text = "Admin Login Incorrect";
        }
       
    }

    private void OnLoginFailureAdmin(PlayFabError error)
    {
        Debug.LogError("Login Failed: " + error.GenerateErrorReport());
        messageTextAdmin.text = "Login Failed: " + error.ErrorMessage;
    }

    #endregion


    #region UpdateAndGetData

 


    //this function call from button through UI
   public void GetPlayerData()
    {
        GetPlayFabUserIDsFromLeaderboard();
     
    }

    public void GetPlayFabUserIDsFromLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerIDLeaderboard",
            StartPosition = 0,
            MaxResultsCount = 100 // Fetch top 100 players
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        bool checkUserExist=false;
        foreach (var entry in result.Leaderboard)
        {
            if(MinePlayfabUserID== entry.PlayFabId)
            {
                checkUserExist = true;
                if (entry.StatValue == 20)
                {
                    
                    RequestPanel.SetActive(false);
                    JoinRooms.SetActive(true);
                    Debug.Log("Hello 1");
                    break;
                }
                else if (entry.StatValue == 10)
                {
                    RequestJoinMessage.text = "Request is pending";
                }
                else
                {
                    RequestJoinMessage.text = "Request send to Admin";
                    UpdateLeaderboardWithPlayFabId(10);
                }
              
            }
    
       
        }
        if(!checkUserExist)
        {
            RequestJoinMessage.text = "Request send to Admin";
            UpdateLeaderboardWithPlayFabId(10);
        }
    }

    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError("Failed to retrieve leaderboard: " + error.GenerateErrorReport());
    }

    public void UpdateLeaderboardWithPlayFabId(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
        {
            new StatisticUpdate { StatisticName = "PlayerIDLeaderboard", Value = score }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnLeaderboardUpdateFailure);
    }

    private void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully updated leaderboard.");
    }

    private void OnLeaderboardUpdateFailure(PlayFabError error)
    {
        Debug.LogError("Failed to update leaderboard: " + error.GenerateErrorReport());
    }

  

    #endregion


}
