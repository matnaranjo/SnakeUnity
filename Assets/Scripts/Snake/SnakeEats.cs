using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeEats : MonoBehaviour
{

    [SerializeField]
    private GameObject FoodToSpawn = null;
    [SerializeField]
    private Text ScoreText;
    private GameObject FoodParticle = null;
    private GameObject[] SnakeSegments = new GameObject[] {};

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

    private int Score=0;

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

    // Compares the position of the snake's head with the food's position and if they are in the same grid position, firts, the food particle is Destroyed, then, the grid matrix is updated, a point is added to the score, and a new position for a new food particle is decided.
    private void SnakeFindsFood(){

        HeadPosX = transform.position.x;
        HeadPosY = transform.position.y;
        if ((HeadPosX>FoodPosX-0.1f && HeadPosX<FoodPosX+0.1f ) && (HeadPosY>FoodPosY-0.1f && HeadPosY<FoodPosY+0.1f)){
            Destroy(FoodParticle);
            GridUpdate();

            Score++;
            ScoreText.text = "Score: "+ Score;
            

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
    
    // Finds any object with tag food, before comparing the positions of the snake head with it.
    private void FindFood(){
        if (FoodParticle == null){
            FoodParticle = GameObject.FindWithTag("Food");
            FoodPosX = FoodParticle.transform.position.x;
            FoodPosY = FoodParticle.transform.position.y;
        }
    }



    // Every Time the snake eats, updates the game grid, leaving empty spaces with an 'e' and the spaces where the snake is WaitWhile an'f'
    private void GridUpdate(){
        int GridPosX=0;
        int GridPosY=0;
        float SegmentPosX = 0f;
        float SegmentPosY =0f;
        FillArray();
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



    // Transforms the position of every snake segment into grid positions
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

    private void FillArray(){
        GridStatus.GridArray = new char [20,20]{{'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
                                                {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'}};
    }
}
