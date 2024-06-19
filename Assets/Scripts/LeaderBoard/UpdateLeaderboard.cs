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
    GameManager gm;

    [SerializeField]
    TextMeshProUGUI test;

    string posStatistic="";
    string nameStatistic="";
    string scoreStatistic="";

    ///<summary>
    ///On death, receives the score the player gotand sends a request to update the statistic<br/>
    ///if the score is higher than the previously saved, it changes, if it doesn't, keeps the previous one
    ///</summary>
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

    ///<summary>
    ///No real reason to have something here, but the function asks for this 2 functions
    ///</summary>
    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result){}
    ///<summary>
    ///No real reason to have something here, but the function asks for this 2 functions
    ///</summary>
    void OnError(PlayFabError error){}


    ///<summary>
    ///Gets the statistic "HighScore", from the player currently playing<br/>
    ///and activates a success response based on what called that request, the leaderboard button or the game screen
    ///</summary>
    public void GetPersonalScore( bool isLeaderboardCall){
        GetLeaderboardAroundPlayerRequest request = new GetLeaderboardAroundPlayerRequest(){
            StatisticName = "HighScore",
            PlayFabId = PlayerPrefs.GetString("userid"),
            MaxResultsCount = 1
        };

        //if the leaderboard called thar statistic
        if (isLeaderboardCall){
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderCallBoardPlayerSuccess, OnError);
        }
        //if it was the game screen
        else{
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGameCallPlayerSuccess, OnError);
        }
    }

    ///<summary>
    ///If it was the game screen calling the score, get the result and use the result for the player to populate the highscore text UI 
    ///</summary>
    void OnGameCallPlayerSuccess(GetLeaderboardAroundPlayerResult result){
        List<PlayerLeaderboardEntry> playerEntry = result.Leaderboard;
        test.text  = playerEntry.Count.ToString() + $"{playerEntry[0].StatValue}";
        menuManager.SetHighScoreUI($"{playerEntry[0].StatValue}");
        gm.UpdateMaxScore(playerEntry[0].StatValue);
    }

    ///<summary>
    ///If it was the leaderboard, there are 3 possibilities
    ///1). The player doesn't have a highscore yet, so we show No Highscore Found
    ///2). The player has one and is top 10, so we want to get the 10 Highscores and show them, showing the player own Highscore
    ///3). The player has one and is NOT top 10, so we still want to show 10 positions<br/>
    ///but 9 will be the top 9 and the last one will be the players score, with the position.
    ///</summary>
    void OnLeaderCallBoardPlayerSuccess(GetLeaderboardAroundPlayerResult result){
        List<PlayerLeaderboardEntry> playerEntry = result.Leaderboard;
        posStatistic="";
        nameStatistic ="";
        scoreStatistic="";
        
        if(playerEntry.Count>0){
            //The player has one and is NOT top 10, so we still want to show 10 positions but 9 will be the top 9 and the last one will be the players score, with the position.
            if (playerEntry[0].Position > 9){
                Get10Scores(9, $"{playerEntry[0].Position+1}", $"{playerEntry[0].DisplayName}", $"{playerEntry[0].StatValue}");
            }
            //The player has one and is top 10, so we want to get the 10 Highscores and show them, showing the player own Highscore
            else{
                Get10Scores(10, "", "", "");
            }
        }
        //The player doesn't have a highscore yet, so we show No Highscore Found
        else{
            posStatistic = "No ";
            nameStatistic = "Highscore";
            scoreStatistic = "Found";
            menuManager.LeaderboardUpdate(posStatistic, nameStatistic, scoreStatistic);
        }
    }

    ///<summary>
    ///Request for the first 10 highscores or the 9 highscores + the one of the player outside of the top 10
    ///</summary>
    public void Get10Scores(int resultCount, string posData, string nameData, string scoreData){
        GetLeaderboardRequest request = new GetLeaderboardRequest(){
            StatisticName = "HighScore",
            MaxResultsCount = resultCount
        };

        PlayFabClientAPI.GetLeaderboard(request,result=> OnGet10Scores(result, posData, nameData, scoreData), OnError);
    }

    ///<summary>
    ///Orginices the result array in the UI and adds the player result at the end of the the list if it is NOT top 10
    ///</summary>
    void OnGet10Scores( GetLeaderboardResult result, string posData, string nameData, string scoreData){
        List<PlayerLeaderboardEntry> entries = result.Leaderboard;
        foreach (PlayerLeaderboardEntry entry in entries){
            posStatistic += $"{entry.Position+1}\n";
            nameStatistic += $"{entry.DisplayName}\n";
            scoreStatistic += $"{entry.StatValue}\n";
        }
        //if the information of the player is not empty, means is not a top 10, so the score is added at the end
        if (posData!=""){
            posStatistic += posData;
            nameStatistic += nameData;
            scoreStatistic += scoreData;
        }
        //Shows the leaderboard with the leaderboard information
        menuManager.LeaderboardUpdate(posStatistic, nameStatistic, scoreStatistic);
    }

}
