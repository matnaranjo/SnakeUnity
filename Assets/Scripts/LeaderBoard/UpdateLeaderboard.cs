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

    string playerStatistic="";


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
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderBoardPlayerSuccess, OnError);
    }

    void OnLeaderBoardPlayerSuccess(GetLeaderboardAroundPlayerResult result){
        List<PlayerLeaderboardEntry> playerEntry = result.Leaderboard;
        playerStatistic="";
        
        if(playerEntry.Count>0){
            if (playerEntry[0].Position > 9){
                playerStatistic += $"{playerEntry[0].Position+1}  {playerEntry[0].DisplayName}  {playerEntry[0].StatValue}";
                Get10Scores(9, playerStatistic);
            }
            else{
                Get10Scores(10, playerStatistic);
            }
        }
        else{
            playerStatistic = "No highscore found";
            menuManager.LeaderboardUpdate(playerStatistic);
        }
    }


    public void Get10Scores(int resultCount, string playerData){
        GetLeaderboardRequest request = new GetLeaderboardRequest(){
            StatisticName = "HighScore",
            MaxResultsCount = resultCount
        };

        PlayFabClientAPI.GetLeaderboard(request,result=> OnGet10Scores(result, playerData), OnError);
    }

    void OnGet10Scores( GetLeaderboardResult result, string playerData){
        List<PlayerLeaderboardEntry> entries = result.Leaderboard;
        foreach (PlayerLeaderboardEntry entry in entries){
            playerStatistic += $"{entry.Position+1}  {entry.DisplayName}  {entry.StatValue}\n";
        }
        if (playerData!=""){
            playerStatistic += playerData;
        }
        menuManager.LeaderboardUpdate(playerStatistic);
    }

}
