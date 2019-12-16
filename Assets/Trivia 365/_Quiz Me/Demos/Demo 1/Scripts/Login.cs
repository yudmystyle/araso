using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;

    public Text promptText;

    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(SignIn());
    }

    IEnumerator SignIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("localhost:8000/login", form);
        yield return www;
        if (www.text.Contains("\"success\":1"))
        {
            Debug.Log("User login Successfully");
            PlayerPrefs.SetString("Username", usernameField.text);
            PlayerPrefs.Save();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Demo1");
        }
        else if (www.text.Contains("Name not found"))
        {
            promptText.text = "Name not found";
        }
        else if (www.text.Contains("Wrong name\\/password"))
        {
            promptText.text = "Wrong name/password.";
        }
        else if (www.text.Contains("User already online on other device"))
        {
            promptText.text = "User already online on other device";
        }
        else
        {
            Debug.Log("Login User Failed.Error #" + www.text);
        }
    }



}
