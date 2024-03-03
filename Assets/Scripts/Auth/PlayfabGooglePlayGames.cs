using GooglePlayGames;
using GooglePlayGames.BasicApi;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;

public class PlayfabGooglePlayGames : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    UIManager uiManager;

    public bool isAuth;
    
    void Start()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated()){
            text.text = "Cat1 auth";
            uiManager.UserIsAuth();
        }
        else{
            text.text = "Cat1 NO auth";
            AutoAuthentication();
        }
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            text.text = "Cat3 auth succ";
            PlayGamesPlatform.Instance.RequestServerSideAccess(false, ProcessServerAuthCode);
        }
        else{
            text.text = "here4 auth NO succ" + status;
        }
    }

    private void ProcessServerAuthCode(string serverAuthCode)
    {
        text.text = "Server Auth Code: " + serverAuthCode;

        var request = new LoginWithGooglePlayGamesServicesRequest
        {
            ServerAuthCode = serverAuthCode,
            CreateAccount = true,
            TitleId = PlayFabSettings.TitleId
        };

        PlayFabClientAPI.LoginWithGooglePlayGamesServices(request, OnLoginWithGooglePlayGamesServicesSuccess, OnLoginWithGooglePlayGamesServicesFailure);
    }

    private void OnLoginWithGooglePlayGamesServicesSuccess(LoginResult result)
    {
        isAuth =true;
        uiManager.UserIsAuth();
        text.text = "PF Login Success LoginWithGooglePlayGamesServices";
    }

    private void OnLoginWithGooglePlayGamesServicesFailure(PlayFabError error)
    {
        isAuth =false;
        uiManager.UserIsNotAuth();
        text.text = "PF Login Failure LoginWithGooglePlayGamesServices: " + error.GenerateErrorReport();
    }

    public void TryAuthentication(){
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    private void AutoAuthentication(){
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }


}
