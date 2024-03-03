using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridStatus : MonoBehaviour
{
    
    
    public char [,] gameArray = new char [24,24];

    public List<int[]> fullIndexes = new List<int[]>();

    public List<int[]> emptyIndexes = new List<int[]>();
    
    // Start is called before the first frame update
    void Start()
    {
        ClearArrayValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// clears both the list of empty spaces to 0 items and the gameArray to everything = 'e'
    /// </summary>
    public void ClearArrayValues(){
        char[,] clearArray = new char [24,24]{
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
        {'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'}};
        gameArray = clearArray;
        emptyIndexes.Clear();
        fullIndexes.Clear();
    }

    /// <summary>
    /// Recieves an int[] to change the spaces where there are segments of the snake to 'f'<br/>
    /// Input: <paramref name="change"/>
    /// </summary>
    /// <param name="change"></param>
    public void GridUpdate(int[] change){
            gameArray[change[1],change[0]] = 'f';
    }

    /// <summary>
    /// Goes through the matrix to get the index of the cells with 'e' on it, or empty cells
    /// Stores the information in an array.
    /// </summary>
    public void EmptySpaces(){
        for (int i = 0; i < 23; i++)
        {
            for (int j = 0; j < 23; j++)
            {
                if (gameArray[i,j]=='e'){
                    int []index = {j,i};
                    emptyIndexes.Add(index);
                }
            }
        }
    }

    
}
