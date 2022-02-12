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
            SnakeMovementTest();
            Move=0;
        }

    }

    private void SnakeMovementTest(){
            transform.position+=new Vector3(MovementX, MovementY, 0f) * PosChange;
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
