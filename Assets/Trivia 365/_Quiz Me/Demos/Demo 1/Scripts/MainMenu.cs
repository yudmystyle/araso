using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToRegister()
    {
        SceneManager.LoadScene("registermenu");

    }

    public void GoToLogin()
    {
        SceneManager.LoadScene("loginmenu");

    }
}
