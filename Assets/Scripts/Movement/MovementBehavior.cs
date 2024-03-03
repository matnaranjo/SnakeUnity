using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    Inputs snakeInputs;

    Vector2 snakeDir = Vector2.right;

    public Vector2 SnakeDir{
        get{return snakeDir;}
    }

    #region Enable and Disable
    void OnEnable(){
        snakeInputs.Enable();
    }

    void OnDisable(){
        snakeInputs.Disable();
    }
    #endregion
    
    void Awake(){
        snakeInputs = new Inputs();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DirectionCheck(Direction());
    }

    /// <summary>
    /// Takes the information of input, and determines what vector to return.
    /// </summary>
    /// <returns>dir = Vector2 with the direction information of the snake</returns>
    /// <paramref name="dir"/>
    private Vector2 Direction(){
        
        Vector2 dir = Vector2.zero;
        Vector2 joystickInput = snakeInputs.Snake.Movement.ReadValue<Vector2>();
        float angle = Vector2.SignedAngle(Vector2.right, joystickInput);
        
        if (snakeInputs.Snake.Movement.triggered){

            //signed angle correction
            if (angle<0){
                angle= 180+(180+angle);
            }

            //selection of direction
            if (angle>=0 && angle<45){
                dir = Vector2.right;
            }
            else if (angle>45 && angle < 135){
                dir = Vector2.up;
            }
            else if (angle>135 && angle < 225){
                dir = Vector2.left;
            }
            else if (angle>225 && angle < 315){
                dir = Vector2.down;
            }
            else if (angle>315 && angle <=360){
                dir = Vector2.right;
            }
        }
        return dir;
    }


    /// <summary>
    /// Depending on the current direction of the snake and the input, changes the direction vector
    /// </summary>
    /// <param name="inputDir"></param>
    private void DirectionCheck(Vector2 inputDir){
        if (snakeDir.x!=0 && inputDir.y!=0){
            snakeDir = inputDir;
        }
        else if (snakeDir.y!=0 && inputDir.x!=0){
            snakeDir = inputDir;
        }
    }

}
