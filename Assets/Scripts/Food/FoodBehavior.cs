using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class FoodBehavior : MonoBehaviour
{

    int [] foodPos = {13,11};

    public int[] FoodPos {
        get {return foodPos;}
        set {foodPos = value;
            //if game is to the left
            float xPos = (0.4f*foodPos[0])-8.1f;
            float ypos = (0.4f*foodPos[1])-4.6f;

            gameObject.transform.position = new Vector3(xPos, ypos);}
    } 
    
    // Start is called before the first frame update
    void Start()
    {
        FoodPos = foodPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Chooses randomly between the elements in the array of empty positions to get the new position of the food.<br/>
    /// Input: <paramref name="indexes"/>
    /// </summary>
    /// <param name="indexes"></param>
    public void RepositionFood(List<int[]> indexes){
        int [] newFoodPos;
        int numberOfEmptySpaces = indexes.Count;

        newFoodPos = indexes[Random.Range(0, numberOfEmptySpaces-1)];

        FoodPos = newFoodPos;
    }
}
