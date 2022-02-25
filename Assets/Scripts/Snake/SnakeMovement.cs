using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    private int Move = 0; 
    private int Refresh = 10;

    private float MovementX=0f;
    private float MovementY=0f;
    private float PosChange = 0.4f;

    private float CurrentPosX=10f;

    private float CurrentPosY=10f;
    private Vector3 PastPos;
    private Vector3 CurrentPos;
    private bool Alive = true;

    void Start()
    {
        
    }
    void Update(){
        InputGetter();
    }

    void FixedUpdate()
    {
        if (!(Refresh == Move)){
            Move+=1;
            
        }
        else {
            SnakeBodyMovement();
            Move=0;
        }

    }

    private void SnakeBodyMovement(){
        if (Alive){
            PastPos = transform.position;
            transform.position+=new Vector3(MovementX, MovementY, 0f) * PosChange;
            for (int Segment = 0 ; Segment<Body.SnakeBody.Count; Segment++) {
                CurrentPos = Body.SnakeBody[Segment].transform.position;
                Body.SnakeBody[Segment].transform.position = PastPos;
                PastPos = CurrentPos;
            }
        }
        CollisionsWithLimitsAndBody();
    }

    private void CollisionsWithLimitsAndBody(){
        float HeadX = transform.position.x;
        float HeadY = transform.position.y;
        if (HeadX>4 || HeadX<-4 || HeadY>2.8 || HeadY<-5.2){
            Alive=false;
        }
        foreach(GameObject Segment in Body.SnakeBody){
            float SegmentX = Segment.transform.position.x;
            float SegmentY = Segment.transform.position.y;
            if ((HeadX>SegmentX-0.1 && HeadX<SegmentX+0.1)&& (HeadY>SegmentY-0.1 && HeadY<SegmentY+0.1)){
                Alive=false;
            }
        }
    }



    private void InputGetter(){
        if ( MovementX==0f && (Input.GetKey("left") || Input.GetKey("right")) && (CurrentPosY!=transform.position.y)){
            MovementX = Input.GetAxisRaw("Horizontal");
            MovementY=0f;
            CurrentPosY = transform.position.y;


        }
        if (MovementY==0f && (Input.GetKey("down") || Input.GetKey("up")) && (CurrentPosX!=transform.position.x)){
            MovementY = Input.GetAxisRaw("Vertical");
            MovementX=0f;
            CurrentPosX = transform.position.x;
        }
    }


}
