using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEats : MonoBehaviour
{
    private GameObject FoodParticle = null;
    private GameObject[] SnakeSegments = new GameObject[] {};

    [SerializeField]
    private GameObject FoodToSpawn = null;

    private List <int> AvailableSpotsX = new List<int>();
    private List <int> AvailableSpotsY = new List<int>();
    private Vector3 RandomPos;
    private int RandomSelection=0;
    private float CoordinatesPosX=0;
    private float CoordinatesPosY=0;

    private float FoodPosX;
    private float FoodPosY;
    private float HeadPosX;
    private float HeadPosY;

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

            AvailableSpotsX.Clear();
            AvailableSpotsY.Clear();
            for (int j=0; j<20; j++){
                for (int i=0; i<20; i++){
                    if (GridStatus.GridArray[j,i]=='e'){
                        AvailableSpotsY.Add(j);
                        AvailableSpotsX.Add(i);
                    }
                }
            }
            RandomSelection = Random.Range(0, AvailableSpotsX.Count);
            CoordinatesPosX = (0.4f*AvailableSpotsX[RandomSelection])-3.8f;
            CoordinatesPosY = (-0.4f*AvailableSpotsY[RandomSelection])+2.6f;
            RandomPos = new Vector3(CoordinatesPosX, CoordinatesPosY, 0f);
            GameObject NewFood = Instantiate(FoodToSpawn,RandomPos,transform.rotation);


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
