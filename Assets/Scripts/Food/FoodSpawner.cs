using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject SnakeHead=null;

    [SerializeField]
    private GameObject FoodToSpawn = null;
    private GameObject FoodParticle = null;
    private List <int> AvailableSpotsX = new List<int>();
    private List <int> AvailableSpotsY = new List<int>();
    private Vector3 RandomPos;
    private int RandomSelection=0;
    private float CoordinatesPosX=0;
    private float CoordinatesPosY=0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FindFood();
        FoodSpawn();
    }


    public void FoodSpawn(){
        if (FoodParticle==null){
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
            FoodParticle = GameObject.FindWithTag("Food");
    }
}
