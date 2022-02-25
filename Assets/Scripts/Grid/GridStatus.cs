using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStatus : MonoBehaviour
{
    public static char[,] GridArray = new char [20,20]{{'e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e','e'},
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

    private float [] Xpos = new float [20] {-3.8f, -3.4f, -3f, -2.6f, -2.2f, -1.8f, -1.4f, -1f, -0.6f, -0.2f, 0.2f, 0.6f, 1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f, 3.4f, 3.8f};
    private float [] Ypos = new float [20] {2.6f, 2.2f, 1.8f, 1.4f, 1f, 0.6f, 0.2f, -0.2f, -0.6f, -1f, -1.4f, -1.8f, -2.2f, -2.6f, -3f, -3.4f, -3.8f, -4.2f, -4.6f, -5f};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
