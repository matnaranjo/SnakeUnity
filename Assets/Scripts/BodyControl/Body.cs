using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// If snake eats, transfer one of the objects from the storage to the body
    /// </summary>
    public static void ChangeFathers(){
        Transform body = GameObject.FindGameObjectWithTag("snakebody").transform;
        Transform storage = GameObject.FindGameObjectWithTag("storage").transform;

        storage.GetChild(0).SetParent(body);
    }
}
