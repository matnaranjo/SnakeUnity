using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

    /// <summary>
    /// If snake eats, transfer one of the objects from the storage to the body
    /// </summary>
    public static void ChangeFathers(){

        //get storage and body objects
        Transform body = GameObject.FindGameObjectWithTag("snakebody").transform;
        Transform storage = GameObject.FindGameObjectWithTag("storage").transform;

        //transfer first child of storage to body
        storage.GetChild(0).SetParent(body);
    }
}
