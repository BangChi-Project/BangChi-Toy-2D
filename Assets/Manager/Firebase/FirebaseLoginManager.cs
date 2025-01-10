using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Google;
using AppleAuth;
using System.Threading.Tasks;

public class FirebaseLoginManager : MonoBehaviour
{
    // Reference to buttons in the Unity scene
    public Button googleLoginButton;
    public Button appleLoginButton;

    private FirebaseAuth auth;

    void Start()
    {
        // Initialize Firebase Auth
        auth = FirebaseAuth.DefaultInstance;

        // Add listeners to buttons
        googleLoginButton.onClick.AddListener(() => SignInWithGoogle());
        appleLoginButton.onClick.AddListener(() => SignInWithApple());
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
        Debug.Log("Apple login started");

        GetAppleIdToken().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Failed to get Apple ID token: " + task.Exception);
                return;
            }

            string appleIdToken = task.Result;
            Credential credential = OAuthProvider.GetCredential("apple.com", appleIdToken, null, null);
            SignInWithCredential(credential, "Apple");
        });
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

    private Task<string> GetAppleIdToken()
    {
        var taskCompletionSource = new TaskCompletionSource<string>();

        var quickLoginArgs = new AppleAuthQuickLoginArgs();
        AppleAuthManager.Instance.LoginWithAppleId(quickLoginArgs, 
            credentialState =>
            {
                if (credentialState.IsAuthorized)
                {
                    taskCompletionSource.SetResult(credentialState.IdToken);
                }
                else
                {
                    taskCompletionSource.SetException(new System.Exception("Apple Sign-In failed"));
                }
            },
            error =>
            {
                taskCompletionSource.SetException(new System.Exception("Apple Sign-In encountered an error: " + error));
            });

        return taskCompletionSource.Task;
    }
}
