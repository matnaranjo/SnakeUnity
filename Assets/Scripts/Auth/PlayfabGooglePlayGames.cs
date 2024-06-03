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
    TextMeshProUGUI nameText;
    [SerializeField]
    UIManager uiManager;


    public bool isAuth;
    

    //Check if the user is authenticated or not when entering the scene
    void Start()
    {
        // if (PlayGamesPlatform.Instance.IsAuthenticated()){
        //     //if authenticated, change UI to UserIsAuth
        //     text.text = "Cat1 auth";
        //     uiManager.UserIsAuth();
        // }
        // else{
        //     //if not, call for the autoAuthentication
        //     text.text = "Cat1 NO auth";
        //     AutoAuthentication();
        // }

        AutoAuthentication();

    }

    /// <summary>
    /// When the user is not authenticated as soon as the game starts, it will call for authoauthentication of a previously used google play games account and call for ProcessAuthentication to see if it was completed successfully or not
    /// </summary>
    private void AutoAuthentication(){
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    /// <summary>
    /// Starts a call to request access to the server side. 
    /// </summary>
    /// <param name="status"></param>
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
            TitleId = PlayFabSettings.TitleId,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithGooglePlayGamesServices(request, OnLoginWithGooglePlayGamesServicesSuccess, OnLoginWithGooglePlayGamesServicesFailure);
    }

    private void OnLoginWithGooglePlayGamesServicesSuccess(LoginResult result)
    {
        isAuth =true;
        if (result.InfoResultPayload.PlayerProfile.DisplayName== null){
            uiManager.UserNoName();
            nameText.text = result.InfoResultPayload.PlayerProfile.DisplayName + "profile exists";
        }
        else{
            string userName = result.InfoResultPayload.PlayerProfile.DisplayName;

            //Save name in playerprefs
            PlayerPrefs.SetString("userid", result.PlayFabId);
            PlayerPrefs.SetString("username", userName);

            uiManager.UserIsAuth(userName);
            nameText.text = "profile does NOT exists";
        }
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

    


}
