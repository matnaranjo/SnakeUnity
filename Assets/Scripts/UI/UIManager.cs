using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject logIn;
    [SerializeField]
    GameObject start;


    public void UserIsAuth(){
        logIn.SetActive(false);
        start.SetActive(true);
    }

    public void UserIsNotAuth(){
        logIn.SetActive(true);
        start.SetActive(false);
    }
}
