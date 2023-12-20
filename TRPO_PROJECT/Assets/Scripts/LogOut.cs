using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class LogOut : MonoBehaviour
{
    public void OnClick()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene("Auth");
    }
}
