using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class UpdateLeaderboard : MonoBehaviour
{
    [SerializeField]
    MenuManager menuManager;

    [SerializeField]
    TextMeshProUGUI test;


    public void SaveMaxScore(int score){
        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "HighScore",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }

    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result){
        
    }

    void OnError(PlayFabError error){

    }

    public void GetPersonalScore(){
        GetLeaderboardAroundPlayerRequest request = new GetLeaderboardAroundPlayerRequest(){
            StatisticName = "HighScore",
            PlayFabId = PlayerPrefs.GetString("userid"),
            MaxResultsCount = 1
        };
        test.text = "getting personal scores";
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderBoardPlayerSuccess, OnError);
    }

    void OnLeaderBoardPlayerSuccess(GetLeaderboardAroundPlayerResult result){
        List<PlayerLeaderboardEntry> playerEntry = result.Leaderboard;
        string playerStatistic="";
        test.text = "result melo";
        if(playerEntry.Count>0){
            playerStatistic = $"{playerEntry[0].Position+1}.  {playerEntry[0].DisplayName}  {playerEntry[0].StatValue}";
        test.text = playerStatistic;
        }
        else{
            playerStatistic = "No highscore found";
        test.text = playerStatistic;
        }

        menuManager.LeaderboardUpdate(playerStatistic);
    }

}
