using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
public class AdminAPIDatabase : MonoBehaviour
{
    public GameObject Content;
    public GameObject PrefabScrollView;

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

        if (Content.transform.childCount > 0)
        {
            foreach (Transform child in Content.transform)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var entry in result.Leaderboard)
        {
            if (entry.StatValue == 10)
            {
                Debug.Log("Player PlayFabId: " + entry.PlayFabId);
                GameObject ListObj = Instantiate(PrefabScrollView, Content.transform);
                ListObj.GetComponent<ReferenceHolderList>().Name.text = entry.DisplayName;
                ListObj.GetComponent<ReferenceHolderList>().PlayfabUserID = entry.PlayFabId;
            }
        }
    }

    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError("Failed to retrieve leaderboard: " + error.GenerateErrorReport());
    }

}
