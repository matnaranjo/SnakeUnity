using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEats : MonoBehaviour
{
    private GameObject FoodParticle = null;
    private GameObject[] SnakeSegments = new GameObject[] {};

    private float FoodPosX;
    private float FoodPosY;
    private float HeadPosX;
    private float HeadPosY;
    bool cat = true;

    // Start is called before the first frame update
    void Start()
    {
        FindFood();
    }

    void Update(){
        FindFood();
        SnakeFindsFood();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void SnakeFindsFood(){

        HeadPosX = transform.position.x;
        HeadPosY = transform.position.y;
        if ((HeadPosX>FoodPosX-0.1f && HeadPosX<FoodPosX+0.1f ) && (HeadPosY>FoodPosY-0.1f && HeadPosY<FoodPosY+0.1f)){
            Destroy(FoodParticle);
            GridUpdate();
        }
    }

    private void FindFood(){
        if (FoodParticle == null){
            FoodParticle = GameObject.FindWithTag("Food");
            FoodPosX = FoodParticle.transform.position.x;
            FoodPosY = FoodParticle.transform.position.y;
        }
    }

    private void GridUpdate(){
        int GridPosX=0;
        int GridPosY=0;
        float SegmentPosX = 0f;
        float SegmentPosY =0f;
        SnakeSegments = new GameObject[] {};
        SnakeSegments = GameObject.FindGameObjectsWithTag("Snake");  
        foreach (GameObject Segment in SnakeSegments){
            SegmentPosX = Segment.transform.position.x;
            SegmentPosY = Segment.transform.position.y;
            GridPosX = XFunction(SegmentPosX);
            GridPosY = YFunction(SegmentPosY);
             
            GridStatus.GridArray[GridPosY, GridPosX] = 'f'; 
        }
    }

    private int XFunction(float XPos){
        int GridX = 0;
        GridX = (int)Mathf.Round((2.5f*XPos)+9.5f);
        return GridX;
    }
    private int YFunction(float YPos){
        int GridY = 0;
        GridY = (int)Mathf.Round((-2.5f*YPos)+6.5f);
        return GridY;
    }
}
