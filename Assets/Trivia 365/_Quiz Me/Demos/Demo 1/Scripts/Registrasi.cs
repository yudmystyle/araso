using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registrasi : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;

    public Text promptText;

    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("localhost:8000/register", form);
        yield return www;
        if (www.text.Contains("\"success\":1"))
        {
            Debug.Log("User Created Successfully");
            UnityEngine.SceneManagement.SceneManager.LoadScene("loginmenu");
        }
        else if(www.text.Contains("\"name\":[\"The name has already been taken.\""))
        {
            promptText.text = "The name has already been taken.";
        }
        else if(www.text.Contains("\"password\":[\"The password must be at least 8 characters.\""))
        {
            promptText.text = "The password must be at least 8 characters.";
        }
        else
        {
            Debug.Log("Create User Failed.Error #" + www.text);
        }
    }

    

}
