using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int score;
    int speed;
    public int Speed{
        get{return speed;}
        set{speed = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeScore(){
        score += (10-speed);
    }

    public void ResetScore(){
        score=0;
    }

    public void SetSpeed(){
    }



}
