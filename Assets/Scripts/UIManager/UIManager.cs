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

    ///<summary>
    ///if user is authenticated, and with name, shows welcome message and start text
    ///</summary>
    public void UserIsAuth(string userName){
        string msg = "Welcome " + userName + "\n\n Click to\nstart ... " ;
        logIn.SetActive(false);
        start.SetActive(true);
        btnStart.text = msg;
    }

    ///<summary>
    ///if user is NOT authenticated, shows button for manual authentication
    ///</summary>
    public void UserIsNotAuth(){
        logIn.SetActive(true);
        start.SetActive(false);
    }

    ///<summary>
    ///Authenticated user with no name in the system, must go through name selection
    ///</summary>
    public void UserNoName(){
        logIn.SetActive(false);
        nameSelection.SetActive(true);
    }

    ///<summary>
    ///Authenticated user who went through selection of name and was submitted successfully
    ///</summary>
    public void UserWithName(string userName){
        string msg = "Welcome " + userName + "\n\n Click to\nstart ... " ;

        nameSelection.SetActive(false);
        start.SetActive(true);
        btnStart.text = msg;
    }
}
