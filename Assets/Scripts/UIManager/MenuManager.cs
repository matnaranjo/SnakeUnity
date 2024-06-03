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
    TextMeshProUGUI leaderBoardTxt;

    public void StartBtn(){
        startMenu.SetActive(false);

        controls.SetActive(true);
    }

    public void Defeat(){
        controls.SetActive(false);

        defeat.SetActive(true);
    }

    public void MainMenu(){
        configuration.SetActive(false);
        controls.SetActive(false);
        defeat.SetActive(false);
        leaderBoard.SetActive(false);

        startMenu.SetActive(true);
    }

    public void LeaderBoard(){
        startMenu.SetActive(false);

        leaderBoard.SetActive(true);
    }

    public void LeaderboardUpdate(string leaderBoardResult){
        leaderBoardTxt.text = leaderBoardResult;
    }
}
