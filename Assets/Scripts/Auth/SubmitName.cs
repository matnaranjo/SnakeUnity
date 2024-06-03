using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;


public class SubmitName : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;
    TMP_InputField inputName;
    TextMeshProUGUI txtMessage;

    public void CheckName(){
        inputName = GameObject.FindGameObjectWithTag("inputname").GetComponent<TMP_InputField>();
        txtMessage = GameObject.FindGameObjectWithTag("namemsg").GetComponent<TextMeshProUGUI>();
        string strName = inputName.text;

        if(strName.Length<4 || strName.Length>12 || Regex.IsMatch(strName, @"\W|_")){
            txtMessage.text = "- Name has to be between 8 and 12 characters and contain only letters or numbers - \n";
        }
        else{
            Debug.Log(strName);
            txtMessage.text = "";
            SubmitToCloud(strName);
        }

    }

    private void SubmitToCloud(string name){
        UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest{
            DisplayName = name
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSuccess, OnError);
    }

    private void OnSuccess(UpdateUserTitleDisplayNameResult result){
        uiManager.UserWithName(result.DisplayName);
    }
    private void OnError(PlayFabError error){

        if(error.Error == PlayFabErrorCode.NameNotAvailable){
            txtMessage.text = "- Name Not Available -";
        }
        else{
            txtMessage.text = "- An error ocurred while saving the name, please try again - ";
        }
    }
}
