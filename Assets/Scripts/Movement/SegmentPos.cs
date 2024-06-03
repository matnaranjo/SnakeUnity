using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class SegmentPos : MonoBehaviour
{

    int [] pos = {30,30};

    public int posx;
    public int posy;

    public int[] Pos{
        get{return pos;}
        set{
            pos = value;
            posx = pos[0];
            posy = pos[1];
            //if game is to the left
            float xPos = (0.4f*pos[0])-8.1f;
            float ypos = (0.4f*pos[1])-4.6f;

            gameObject.transform.position = new Vector3(xPos, ypos);
            }
    }
}
