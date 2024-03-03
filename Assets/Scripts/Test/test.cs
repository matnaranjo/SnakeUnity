using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class test : MonoBehaviour
{

    float time=0;
    bool oneTime = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            Debug.Log("aca");
        }

        else{Debug.Log(PlayFabSettings.staticSettings.TitleId);}

        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 10 && oneTime==true){
            ScreenCapture.CaptureScreenshot("SomeLevel4.png");
            oneTime = false;
        }
    }

    void OnLoginSuccess(LoginResult result){
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    void OnLoginFailure(PlayFabError error){
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }





}
