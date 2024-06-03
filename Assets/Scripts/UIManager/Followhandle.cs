using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followhandle : MonoBehaviour
{
    [SerializeField]
    Transform handle;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Makes position of this object (Text), equal to handle position
    /// </summary>
    private void Move(){
        gameObject.transform.position = handle.position;
    }
}
