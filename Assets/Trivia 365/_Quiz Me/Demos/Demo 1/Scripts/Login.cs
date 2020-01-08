using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Text.Json;
//using System.Text.Json.Serialization;
using System;
using UnityEngine;

public class Login : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;

    public Text promptText;

    public Button submitButton;

    [Serializable]
    public class UserModel
    {
        public int id_user;
        public string name;
    }


    [Serializable]
    public class Response
    {
        public int success;
        public string message;
        public UserModel user;
    }


    public void CloseLogin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
    }

    public void CallLogin()
    {
        StartCoroutine(SignIn());
    }

    IEnumerator SignIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW(ApiConstant.SERVER + "/login", form);
        yield return www;
        Response response = JsonUtility.FromJson<Response>(www.text);
        Debug.Log(www.text);
        // if (www.text.Contains("\"success\":1"))
        if (response.success == 1)
        {
            Debug.Log("User login Successfully");
            PlayerPrefs.SetString("Username", usernameField.text);
            PlayerPrefs.SetInt("UserId", response.user.id_user);
            PlayerPrefs.Save();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Demo1");
        }
        //else if (www.text.Contains("Name not found"))
        else if (response.message == "Name not found")
        {
            promptText.text = "Name not found";
        }
        // else if (www.text.Contains("Wrong name\\/password"))
        else if (response.message == "Wrong name/password")
        {
            promptText.text = "Wrong name/password.";
        }
        // else if (www.text.Contains("User already online on other device"))
        else if (response.message == "User already online on other device")
        {
            promptText.text = "User already online on other device";
        }
        else
        {
            Debug.Log("Login User Failed.Error #" + www.text);
        }
    }
}
