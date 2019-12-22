using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Uniquecode : MonoBehaviour
{
    public InputField uniquecodeField;
    public Text promptText;

    public void CallUniquecode()
    {
        StartCoroutine(JoinArena());
    }

    IEnumerator JoinArena()
    {
        WWWForm form = new WWWForm();
        form.AddField("uniquecode", uniquecodeField.text);
        WWW www = new WWW(ApiConstant.SERVER + "/joinarena", form);
        yield return www;
        if (www.text.Contains("\"success\":1"))
        {
            Debug.Log("Join Arena Successfully");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Demo1");
        }
        else if (www.text.Contains("user sudah masuk dalam arena"))
        {
            promptText.text = "user sudah masuk dalam arena";
        }
        else if (www.text.Contains("tidak dapat join, waktu permainan telah berakhir"))
        {
            promptText.text = "tidak dapat join waktu permainan telah berakhir";
        }
        else if (www.text.Contains("arena tidak ditemukan"))
        {
            promptText.text = "arena tidak ditemukan";
        }
        else
        {
            Debug.Log("Login User Failed.Error #" + www.text);
        }
    }
}
