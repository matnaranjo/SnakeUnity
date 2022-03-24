using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeEats : MonoBehaviour
{


// Variables for snake eating, changing score text and creating new food
    [SerializeField]
    private GameObject FoodToSpawn = null;

    private GameObject FoodParticle = null; //receives the object 'food' in the screen
    private GameObject[] SnakeSegments = new GameObject[] {}; // contains all the objects that form the snake (head and body segments)

    private List <int> AvailableSpotsX = new List<int>(); // contains the available x positions in the grid to place food
    private List <int> AvailableSpotsY = new List<int>(); // contains the available y positions in the grid to place food
    
    private int RandomSelection=0; // selects a random index based on the size of AvailableSpot X and Y, to place the new food
    private float CoordinatesPosX=0; // transformed  value of int grid to transform.position.x value
    private float CoordinatesPosY=0;// transformed  value of int grid to transform.position.y value
    private Vector3 RandomPos; // Stores the x and y coordinate to place the new food particle
    // Position of the food in the map, once it is placed again
    private float FoodPosX;
    private float FoodPosY;
    //Position of the snake's head used to be compared with the foodPos
    private float HeadPosX;
    private float HeadPosY;

    // Score Text and score value.
    [SerializeField]
    private Text ScoreText;
    private int Score=0;



    // Variables for body instatement
    [SerializeField]
    private GameObject BodyBlock=null; // Body block to instate 

    private List<GameObject> SnakeBody = new List<GameObject>(); //List of body blocks


    // Variables for Snake movement
    private int Move = 0; 
    public static int Refresh;

    private float MovementX=0f;
    private float MovementY=0f;
    private float PosChange = 0.4f;

    private float CurrentPosX=10f;

    private float CurrentPosY=10f;
    private Vector3 PastPos;
    private Vector3 CurrentPos;
    private bool Alive = true;

    // Variables for Pause
    private bool Pause=false;

    [SerializeField]
    private GameObject PauseMenuCanvas;


    // Variables for sound effect

    [SerializeField]
    private AudioSource ASource;

    [SerializeField]
    private AudioClip EatSoundEffect;
    [SerializeField]
    private AudioClip DeadSoundEffect;

    // Start is called before the first frame update
    void Start()
    {   
        Refresh = 6;
        FindFood();
    }

    void Update(){

        PauseGame();
        if (!Pause){
            InputGetter();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Pause){
            if (!(Refresh == Move)){
                Move+=1;
            
            }
            else {
                SnakeFindsFood();
                Move=0;
            }
        }
        
    }

    // Compares the position of the snake's head with the food's position and if they are in the same grid position, firts, the food particle is Destroyed, then, the grid matrix is updated, a point is added to the score, and a new position for a new food particle is decided.
    private void SnakeFindsFood(){

        SnakeBodyMovement();
        FindFood();

        if ((HeadPosX>FoodPosX-0.1f && HeadPosX<FoodPosX+0.1f ) && (HeadPosY>FoodPosY-0.1f && HeadPosY<FoodPosY+0.1f)){

            ASource.PlayOneShot(EatSoundEffect, 0.5f);

            Destroy(FoodParticle);
            GridUpdate();

            // New boddy block added to Snake Body List
            int BodyLength=SnakeBody.Count;
            GameObject NewBlock = Instantiate(BodyBlock,transform.position,transform.rotation);
            SnakeBody.Add(NewBlock);
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



    //Movement

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

    private void SnakeBodyMovement(){
        if (Alive){
            PastPos = transform.position;
            transform.position+=new Vector3(MovementX, MovementY, 0f) * PosChange;
            for (int Segment = 0 ; Segment<SnakeBody.Count; Segment++) {
                CurrentPos = SnakeBody[Segment].transform.position;
                SnakeBody[Segment].transform.position = PastPos;
                PastPos = CurrentPos;
            }
        }
        CollisionsWithLimitsAndBody();
    }

    private void CollisionsWithLimitsAndBody(){
        HeadPosX = transform.position.x;
        HeadPosY = transform.position.y;

        if (Alive){
            if (HeadPosX>4 || HeadPosX<-4 || HeadPosY>2.8 || HeadPosY<-5.2){
                ASource.PlayOneShot(DeadSoundEffect, 0.5f);
                Alive=false;
            }
            foreach(GameObject Segment in SnakeBody){
                float SegmentX = Segment.transform.position.x;
                float SegmentY = Segment.transform.position.y;
                if ((HeadPosX>SegmentX-0.1 && HeadPosX<SegmentX+0.1)&& (HeadPosY>SegmentY-0.1 && HeadPosY<SegmentY+0.1)){
                    ASource.PlayOneShot(DeadSoundEffect, 0.5f);
                    Alive=false;
                }
            }
        }


    }

    private void PauseGame(){
        if (Input.GetKeyDown("p")){
            Pause = !Pause;
            PauseMenuCanvas.SetActive(Pause);
            Move = 0;
        }
    }
}
