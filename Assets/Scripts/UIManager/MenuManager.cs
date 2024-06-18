using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject configuration;
    [SerializeField]
    GameObject controls;
    [SerializeField]
    GameObject defeat;
    [SerializeField]
    GameObject leaderBoard;
    [SerializeField]
    GameObject startMenu;
    [SerializeField]
    TextMeshProUGUI posTxt;
    [SerializeField]
    TextMeshProUGUI nameTxt;
    [SerializeField]
    TextMeshProUGUI scoreTxt;
    [SerializeField]
    TextMeshProUGUI highScore;
    [SerializeField]
    GameObject congrats;
    [SerializeField]
    GameObject youDied;
    [SerializeField]
    TextMeshProUGUI finalScore;
    [SerializeField]
    TextMeshProUGUI newHighScore;
    [SerializeField]
    GameManager gm;

    ///<summary>
    ///
    ///</summary>
    public void StartBtn(){
        startMenu.SetActive(false);
        controls.SetActive(true);
    }

    public void Defeat(bool isHighScore){
        controls.SetActive(false);

        defeat.SetActive(true);

        if(isHighScore){
            congrats.SetActive(true);
            youDied.SetActive(false);
            newHighScore.text = gm.score.ToString();
        }
        else{
            congrats.SetActive(false);
            youDied.SetActive(true);
            finalScore.text = gm.score.ToString();
        }
    }

    public void MainMenu(){
        configuration.SetActive(false);
        controls.SetActive(false);
        defeat.SetActive(false);
        congrats.SetActive(false);
        youDied.SetActive(false);
        leaderBoard.SetActive(false);

        startMenu.SetActive(true);
    }

    public void LeaderBoard(){
        startMenu.SetActive(false);

        leaderBoard.SetActive(true);
    }

    public void LeaderboardUpdate(string position, string name, string score){
        posTxt.text = position;
        nameTxt.text = name;
        scoreTxt.text = score;
    }

    public void SetHighScoreUI(string highScorestr){
        highScore.text = highScorestr;
    }
}
