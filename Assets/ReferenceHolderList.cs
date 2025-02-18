using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceHolderList : MonoBehaviour
{

    public TMP_Text Name;
    public string PlayfabUserID;
    public Button RemoveBTn;
    public Button ConfirmBTn;
    // Start is called before the first frame update

    private void Start()
    {
        RemoveBTn.onClick.AddListener(Remove);
        ConfirmBTn.onClick.AddListener(Confirm);
    }
    public void Remove()
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "removeLeaderboardEntry",
            FunctionParameter = new
            {
                playerId = PlayfabUserID,
                leaderboardId = "PlayerIDLeaderboard"
            },
            GeneratePlayStreamEvent = true // Optional
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnRemoveSuccess, OnRemoveError);
    }
    void OnRemoveSuccess(ExecuteCloudScriptResult result)
    {
        Destroy(gameObject);
        Debug.Log("Entry removed: " + result.FunctionResult.ToString());
    }

    void OnRemoveError(PlayFabError error)
    {
        Debug.LogError("Error removing entry: " + error.ErrorMessage);
    }





    public void Confirm()
    {
        UpdateOtherUserLeaderboardScore(PlayfabUserID, 20);
    }

    public void UpdateOtherUserLeaderboardScore(string targetPlayFabId, int newScore)
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "updateLeaderboardScore",
            FunctionParameter = new { playFabId = targetPlayFabId, newScore = newScore },
            GeneratePlayStreamEvent = true // Optional: to generate a PlayStream event
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnCloudScriptSuccess, OnCloudScriptError);
    }

    private void OnCloudScriptSuccess(ExecuteCloudScriptResult result)
    {
        Destroy(gameObject);
        Debug.Log("Leaderboard score updated successfully for another user.");
    }

    private void OnCloudScriptError(PlayFabError error)
    {
        Debug.LogError("Error updating leaderboard score: " + error.GenerateErrorReport());
    }



   
   
}
