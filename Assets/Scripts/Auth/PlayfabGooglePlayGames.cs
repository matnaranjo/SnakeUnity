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
        //if there is a connection, procees with the authentication
        if (status == SignInStatus.Success)
        {
            // text.text = "Cat3 auth succ";
            PlayGamesPlatform.Instance.RequestServerSideAccess(false, ProcessServerAuthCode);
        }
        else{
            // text.text = "here4 auth NO succ" + status;
        }
    }

    ///<summary>
    /// Sends credentials to confirm the user is authenticated<br/>
    ///if he is calls for success, if not, calls for manual authentication
    ///</summary>
    private void ProcessServerAuthCode(string serverAuthCode)
    {
        // text.text = "Server Auth Code: " + serverAuthCode;

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

    ///<summary>
    ///If the user is automatically authenticated, checks for the name in the player profile.<br/>
    ///if the name doesn't exists, activates the UI to create a new name<br/>
    ///if the name exists, saves the name and shows the welcome text and start text
    ///</summary>
    private void OnLoginWithGooglePlayGamesServicesSuccess(LoginResult result)
    {
        isAuth =true;
        //Name doesn't exist, that means new player who must select a player name
        if (result.InfoResultPayload.PlayerProfile.DisplayName== null){
            uiManager.UserNoName();
            // nameText.text = result.InfoResultPayload.PlayerProfile.DisplayName + "profile exists";
        }
        //Name exists so, user data is saved for posterior uses.
        else{
            string userName = result.InfoResultPayload.PlayerProfile.DisplayName;

            //Save name in playerprefs
            PlayerPrefs.SetString("userid", result.PlayFabId);
            PlayerPrefs.SetString("username", userName);

            uiManager.UserIsAuth(userName);
            // nameText.text = "profile does NOT exists";
        }
        // text.text = "PF Login Success LoginWithGooglePlayGamesServices";
    }

    ///<summary>
    ///If player is not authomatically authenticated, the manual authentication button will appear.
    ///</summary>
    private void OnLoginWithGooglePlayGamesServicesFailure(PlayFabError error)
    {
        isAuth =false;
        uiManager.UserIsNotAuth();
        // text.text = "PF Login Failure LoginWithGooglePlayGamesServices: " + error.GenerateErrorReport();
    }

    ///<summary>
    ///activates manual authentication.
    ///</summary>
    public void TryAuthentication(){
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    


}
