using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    // Button behaviour
    [SerializeField]
    private Scrollbar Speed;
    [SerializeField]
    private Text SpeedText;

    private int SpeedInt=5;

    void Update(){
        SpeedValue();
    }



    public void ReStartGame(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }






    private void SpeedValue(){
        SpeedInt = (int) Mathf.RoundToInt(Speed.value*10) + 1;
        SpeedText.text = "SPEED: " + SpeedInt;
    }
    

    public void SaveSpeedChanges(){
        SnakeEats.Refresh = 13 - SpeedInt;
    }


}
