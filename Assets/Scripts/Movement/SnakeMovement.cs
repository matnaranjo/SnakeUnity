using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    GameObject snakeSegments;
    MovementBehavior movementInfo;
    FoodBehavior food;
    GridStatus grid;
    Vector2 snakeDir;
    int [] snakeDirInt = {0,0};
    int [] snakePos = {11,0};
    int [] prevPos = {0,0};
    int [] limits = {0, 23};
    float step = 0.4f;
    int speed = 4;
    int counter = 0;

    public int posX;
    public int posY;

    // 1). Gets the movement information (directions)
    // 2). SnakeSegments stores the segments the snake has.
    void Start()
    {
        movementInfo = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<MovementBehavior>();

        snakeSegments = GameObject.FindGameObjectWithTag("snakebody");
        
        grid = GameObject.FindGameObjectWithTag("grid").GetComponent<GridStatus>();

        food = GameObject.FindGameObjectWithTag("food").GetComponent<FoodBehavior>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        IsFeed();
    }

    /// <summary>
    /// Based on the direction information given by MovementBehavior, moves the snake both in the screen and in the matrix.
    /// </summary>
    void Movement(){
        //calls snakeDir from the movement behavior script
        snakeDir = movementInfo.SnakeDir;

        if (counter>=speed){
            //analizes a prestep.
            Vector3 snakeStep = snakeDir * step;

            //moves the "snake" in the matrix indexes, also sets the new position to CheckForLimits.
            ToMatrixNotation();

            //if the snake goes further than the limits, make timescale 0 and step 0
            //so the snake just dies and does not move outside of the limits.
            if (CheckForLimits() || CheckForBody()){
                snakeStep *= 0;
                Time.timeScale = 0;
            }

            // snake's head moves and counter goes to 0, to start a new count.
            gameObject.transform.position += snakeStep;
            // snake body follows the head movement
            SegmentMovement(prevPos);

            counter=0;
        }
        counter ++;
    }

    /// <summary>
    /// Checks if the snake head goes off limits
    /// </summary>
    /// <returns>True if goes off limits, false if not</returns>
    bool CheckForLimits(){
        bool check = (snakePos[0]<limits[0] || snakePos[0]>limits[1] || snakePos[1]<limits[0] || snakePos[1]>limits[1]);
        if(check){
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks in premove if the head is gonna jump over a segment of the body
    /// </summary>
    /// <returns>True if it touches<br/>False if it doesn't touch</returns>
    bool CheckForBody(){
        SegmentPos [] segments = snakeSegments.GetComponentsInChildren<SegmentPos>();
        
        foreach (SegmentPos segment in segments)
        {
            if (snakePos[0] == segment.Pos[0] && snakePos[1] == segment.Pos[1]){
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// converts Direction to int and moves the snake in the matrix before moving it in the screen with Movement().
    /// </summary>
    void ToMatrixNotation(){
        prevPos[0] = snakePos[0];
        prevPos[1] = snakePos[1];
        snakeDirInt[0] = Convert.ToInt32(snakeDir.x);
        snakeDirInt[1] = Convert.ToInt32(snakeDir.y);
        snakePos[0] += snakeDirInt[0];
        snakePos[1] += snakeDirInt[1];

        posX = snakePos[0];
        posY = snakePos[1];

    }


    /// <summary>
    /// Gets all the components of Type SegmentPos of the segments stored in the object with tag "snakebody" declared in Start()<br/>
    /// Then, changes the position moving the the segment X to the previous position of the segment X-1<br/><br/>
    /// Input: <paramref name="initialPos"></paramref>
    /// </summary>
    /// <param name="initialPos"></param>
    void SegmentMovement(int [] initialPos){
        SegmentPos [] segments = snakeSegments.GetComponentsInChildren<SegmentPos>();
        // Current pos was taking the value asigned to initialPos, in this case prevPos, taking the reference of prevPos to everything that was asigned with currentPos.
        int[] currentPos ={0,0};
        currentPos[0] = initialPos[0];
        currentPos[1] = initialPos[1];

        int[] lastPos = {0,0};

        foreach (SegmentPos segment in segments ){
            lastPos = segment.Pos;
            segment.Pos = currentPos;
            currentPos = lastPos;
        }

    }


    /// <summary>
    /// Gets all the components in the object with thag "snakebody"<br/>
    /// Changes the value of the grid where the head is<br/>
    /// Changes the values of the grid where all the other segments are
    /// </summary>
    void UpdateGrid(){
        SegmentPos [] segments = snakeSegments.GetComponentsInChildren<SegmentPos>();

        //Clear the grid before updating.
        grid.ClearArrayValues();

        // upgrade position of the head
        grid.GridUpdate(snakePos);

        // upgrade positions of the other segments
        foreach (SegmentPos segment in segments)
        {
            grid.GridUpdate(segment.Pos);
        }

        // Fills the list of empty spaces in the grid.
        grid.EmptySpaces();
    }

    /// <summary>
    /// if the postion in the gameGrid of the snake head is the same as the position of the food, update the information of the grid and repositions the food in the empty cells.
    /// </summary>
    void IsFeed(){

        if (food.FoodPos[0] == snakePos[0] && food.FoodPos[1] == snakePos[1]){
            UpdateGrid();
            food.RepositionFood(grid.emptyIndexes);
            Body.ChangeFathers();
        }
    }





}
