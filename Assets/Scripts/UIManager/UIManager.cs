using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject logIn;
    [SerializeField]
    GameObject start;
    [SerializeField]
    GameObject nameSelection;
    [SerializeField]
    TextMeshProUGUI btnStart;


    public void UserIsAuth(string userName){
        string msg = "Welcome " + userName + "\n\n Click to\nstart ... " ;
        logIn.SetActive(false);
        start.SetActive(true);
        btnStart.text = msg;
    }

    public void UserIsNotAuth(){
        logIn.SetActive(true);
        start.SetActive(false);
    }

    public void UserNoName(){
        logIn.SetActive(false);
        nameSelection.SetActive(true);
    }

    public void UserWithName(string userName){
        string msg = "Welcome " + userName + "\n\n Click to\nstart ... " ;

        nameSelection.SetActive(false);
        start.SetActive(true);
        btnStart.text = msg;

    }
}
