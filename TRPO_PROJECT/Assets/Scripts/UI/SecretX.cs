using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;



public class SecretX : MonoBehaviour
{   
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;


    public void CloaseGameplay(){
        SceneManager.LoadScene("MainMenu");
    }
    public void FillPassword(){
        emailInputField.text = "admin@gmail.com" ;
        passwordInputField.text = "111111";
    }
}
