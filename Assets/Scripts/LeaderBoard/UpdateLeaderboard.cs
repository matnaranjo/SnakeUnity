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

    string posStatistic="";
    string nameStatistic="";
    string scoreStatistic="";


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

    public void GetPersonalScore( bool isLeaderboardCall){
        GetLeaderboardAroundPlayerRequest request = new GetLeaderboardAroundPlayerRequest(){
            StatisticName = "HighScore",
            PlayFabId = PlayerPrefs.GetString("userid"),
            MaxResultsCount = 1
        };

        if (isLeaderboardCall){
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderCallBoardPlayerSuccess, OnError);
        }
        else{
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGameCallPlayerSuccess, OnError);
        }
    }

    void OnGameCallPlayerSuccess(GetLeaderboardAroundPlayerResult result){
        List<PlayerLeaderboardEntry> playerEntry = result.Leaderboard;
        menuManager.SetHighScoreUI($"{playerEntry[0].StatValue}");
    }

    void OnLeaderCallBoardPlayerSuccess(GetLeaderboardAroundPlayerResult result){
        List<PlayerLeaderboardEntry> playerEntry = result.Leaderboard;
        posStatistic="";
        nameStatistic ="";
        scoreStatistic="";
        
        if(playerEntry.Count>0){
            if (playerEntry[0].Position > 4){
                Get10Scores(4, $"{playerEntry[0].Position+1}", $"{playerEntry[0].DisplayName}", $"{playerEntry[0].StatValue}");
            }
            else{
                Get10Scores(5, "", "", "");
            }
        }
        else{
            posStatistic = "No ";
            nameStatistic = "Highscore";
            scoreStatistic = "Found";
            menuManager.LeaderboardUpdate(posStatistic, nameStatistic, scoreStatistic);
        }
    }


    public void Get10Scores(int resultCount, string posData, string nameData, string scoreData){
        GetLeaderboardRequest request = new GetLeaderboardRequest(){
            StatisticName = "HighScore",
            MaxResultsCount = resultCount
        };

        PlayFabClientAPI.GetLeaderboard(request,result=> OnGet10Scores(result, posData, nameData, scoreData), OnError);
    }

    void OnGet10Scores( GetLeaderboardResult result, string posData, string nameData, string scoreData){
        List<PlayerLeaderboardEntry> entries = result.Leaderboard;
        foreach (PlayerLeaderboardEntry entry in entries){
            posStatistic += $"{entry.Position+1}\n";
            nameStatistic += $"{entry.DisplayName}\n";
            scoreStatistic += $"{entry.StatValue}\n";
        }
        if (posData!=""){
            posStatistic += posData;
            nameStatistic += nameData;
            scoreStatistic += scoreData;
        }
        menuManager.LeaderboardUpdate(posStatistic, nameStatistic, scoreStatistic);
    }

}
