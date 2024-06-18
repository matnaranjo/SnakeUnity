using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTxt;
    [SerializeField]
    TextMeshProUGUI maxScoreTxt;
    public void ScoreTxt( int scoreInt){
        scoreTxt.text = scoreInt.ToString();
        Debug.Log(gameObject.name);
    }
}
