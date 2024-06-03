using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Grid information
    GridStatus grid;

    //Leaderboard Controller
    [SerializeField]
    UpdateLeaderboard upLeaderBoard;

    //Score ui control
    [SerializeField]
    Score scoreUI;
    //Storage and snake body
    Transform storage;
    Transform snakeBody;
    //SnakeHead
    GameObject snakeHead;
    //SnakeStatus
    bool isAlive = false;
    public bool IsAlive{
        get{return isAlive;}
        set{isAlive = value;}
    }
    //Score and speed
    int score = 0;
    int speed;
    public int Speed{
        get{return speed;}
        set{speed = value;}
    }

    void Start(){
        // Snake head, to restart
        snakeHead = GameObject.FindGameObjectWithTag("head");

        // storage of body segments and snake body
        storage = GameObject.FindGameObjectWithTag("storage").transform;
        snakeBody = GameObject.FindGameObjectWithTag("snakebody").transform;

        //grid info
        grid = GameObject.FindGameObjectWithTag("grid").GetComponent<GridStatus>();

    }
    
    //
    public void ChangeScore(){
        score += (12 - Speed);
        scoreUI.ScoreTxt(score);
    }

    public void ResetScore(){
        score=0;
        scoreUI.ScoreTxt(score);
    }

    public void Death(){
        upLeaderBoard.SaveMaxScore(score);
        isAlive = false;
    }
    
    /// <summary>
    /// gets the value in the slider and sets the speed of the level to that refresh rate
    /// </summary>
    public void SetSpeed(){
        //gets the object slider
        Slider levelSlider = GameObject.FindGameObjectWithTag("speed").GetComponent<Slider>();
        //get the value in the slider and transform it to int
        Speed = 11 - Convert.ToInt32(levelSlider.value);
    }

    /// <summary>
    /// Organices the segments in the storage, restarts the score and sets the head in the initial position of the game
    /// </summary>
    public void RestartLevel(){
        int segmentsInBody = snakeBody.childCount;
        List<Transform> segments = new List<Transform>();

        //Iterate between the segments in the body to move them all to the storage
        for (int i = 0; i < segmentsInBody; i++)
        {
            segments.Add(snakeBody.GetChild(i));
        }
        // foreach segment in the list of segments move them to the position of the storage
        foreach (Transform segment in segments)
        {
            segment.SetParent(storage);
            segment.position = storage.position;
        }

        //reset score to 0
        ResetScore();

        //return isAlive to true to restart
        isAlive=true;
        //Clear all the values in the grid
        grid.ClearArrayValues();
        //Gets the speed in the slider and sets the speed of the game to that one
        SetSpeed();
        //Sets snakepos to initial pos (grid position, not real position)
        snakeHead.GetComponent<SnakeMovement>().SnakePos= new int[] {11,0};
        //sets the snake head to the initial real position
        snakeHead.transform.position = new Vector2(-3.7f, -4.6f);
        //resets the direction to (0,0)
        gameObject.GetComponent<MovementBehavior>().ResetDirection();
    }



}
