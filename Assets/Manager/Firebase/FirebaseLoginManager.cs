using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Google;
using AppleAuth;
using AppleAuth.Interfaces;
using AppleAuth.Enums;
using System.Threading.Tasks;

public class FirebaseLoginManager : MonoBehaviour
{
    // Reference to buttons in the Unity scene
    public Button googleLoginButton;
    public Button appleLoginButton;

    private IAppleAuthManager _appleAuthManager;

    private FirebaseAuth auth;

    void Start()
    {
        // Initialize Firebase Auth
        auth = FirebaseAuth.DefaultInstance;

        // Initialize AppleAuthManager (only for iOS)
#if UNITY_IOS
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            _appleAuthManager = new AppleAuthManager(new PayloadDeserializer());
        }
#endif

        // Add listeners to buttons
        googleLoginButton.onClick.AddListener(() => SignInWithGoogle());
        appleLoginButton.onClick.AddListener(() => SignInWithApple());
    }

    void Update()
    {
#if UNITY_IOS
        if (_appleAuthManager != null)
        {
            _appleAuthManager.Update();
        }
#endif
    }

    private void SignInWithGoogle()
    {
        Debug.Log("Google login started");

        GetGoogleIdToken().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Failed to get Google ID token: " + task.Exception);
                return;
            }

            string googleIdToken = task.Result;
            Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, null);
            SignInWithCredential(credential, "Google");
        });
    }

    private void SignInWithApple()
    {
#if UNITY_IOS
        Debug.Log("Apple login started");

        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);
        _appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    string appleUserId = appleIdCredential.User;
                    string idToken = appleIdCredential.IdentityToken;

                    Credential firebaseCredential = OAuthProvider.GetCredential("apple.com", idToken, null, null);
                    SignInWithCredential(firebaseCredential, "Apple");
                }
            },
            error =>
            {
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogError("Apple Sign-In failed: " + authorizationErrorCode + " " + error);
            });
#else
        Debug.LogError("Apple Sign-In is not supported on this platform.");
#endif
    }

    private void SignInWithCredential(Credential credential, string providerName)
    {
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError($"{providerName} sign-in was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError($"{providerName} sign-in encountered an error: {task.Exception}");
                return;
            }

            FirebaseUser user = task.Result;
            Debug.Log($"{providerName} login successful. User: {user.DisplayName}, Email: {user.Email}");
        });
    }

    private Task<string> GetGoogleIdToken()
    {
        var taskCompletionSource = new TaskCompletionSource<string>();

        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            RequestIdToken = true,
            WebClientId = "YOUR_WEB_CLIENT_ID"
        };

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                taskCompletionSource.SetException(task.Exception ?? new System.Exception("Google Sign-In failed"));
                return;
            }

            taskCompletionSource.SetResult(task.Result.IdToken);
        });

        return taskCompletionSource.Task;
    }
}