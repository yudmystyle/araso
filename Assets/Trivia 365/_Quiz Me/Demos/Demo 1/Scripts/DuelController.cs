using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DuelController : MonoBehaviour
{
    [SerializeField]
    public Text promptText;
    [SerializeField]
    public Text opponentName;
    [SerializeField]
    public Text soal;

    [SerializeField]
    Demo1Controller Controller;

    private Duel duel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        promptText.text = "Sedang mencari lawan...";
        soal.text = "";
        opponentName.text = "...";

        CallCreateDuel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallCreateDuel()
    {
        StartCoroutine(GetDuel());
    }

    IEnumerator GetDuel()
    {
        WWW www = new WWW(ApiConstant.SERVER + "/createduel");
        yield return www;

        Debug.Log(www.text);

        if (www.text.Contains("\"success\":1"))
        {
            duel = new Duel();
            duel = JsonUtility.FromJson<Duel>(www.text);

            PlayerPrefs.SetInt("idDuel", duel.id_duel);
            PlayerPrefs.SetInt("isDuel", 1);
            PlayerPrefs.Save();

            if (duel.status == "Playing")
            {
                promptText.text = "Lawan berhasil ditemukan";
                yield return new WaitForSeconds(2);

                promptText.text = "Lawanmu adalah";
                opponentName.text = duel.opponent;
                yield return new WaitForSeconds(1);

                CallStartGame();
            }
            else if(duel.status == "Waiting")
            {
                //Add delay to let other player join
                yield return new WaitForSeconds(3);

                CallCheckDuel();
            }
        }
        else
        {
            Debug.Log("Create duel Failed.Error #" + www.text);
        }
    }

    public void CallCheckDuel()
    {
        StartCoroutine(CheckDuel());
    }

    IEnumerator CheckDuel()
    {
        WWWForm form = new WWWForm();
        form.AddField("id_duel", PlayerPrefs.GetInt("idDuel"));
        WWW www = new WWW(ApiConstant.SERVER + "/checkduel", form);
        yield return www;

        Debug.Log(www.text);

        if (www.text.Contains("\"success\":1"))
        {
            duel = new Duel();
            duel = JsonUtility.FromJson<Duel>(www.text);

            if (duel.status == "Playing")
            {
                promptText.text = "Lawan berhasil ditemukan";
                yield return new WaitForSeconds(2);

                promptText.text = "Lawanmu adalah";
                opponentName.text = duel.opponent;
                yield return new WaitForSeconds(1);

                CallStartGame();
            }
            else if (duel.status == "Waiting")
            {
                //Add delay to let other player join
                yield return new WaitForSeconds(3);

                CallCheckDuel();
            }
            else if (duel.status == "Done")
            {
                //Add delay to let other player join
                yield return new WaitForSeconds(3);

                CallCheckDuel();
            }
        }
        else
        {
            Debug.Log("Create duel Failed.Error #" + www.text);
        }

    }

    //Play game if ready
    public void CallStartGame()
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        if (duel.paketsoal == 1)
        {
            soal.text = "Soal Bahasa Indonesia";
            yield return new WaitForSeconds(2);
            Controller.PlayGame("Bahasa Indonesia", "", "bahasa", 2, "", "", "", true);
        }
        else if (duel.paketsoal == 2)
        {
            soal.text = "Soal Matematika";
            yield return new WaitForSeconds(2);
            Controller.PlayGame("Matematika", "", "matematika", 2, "", "", "", true);
        }
        else if (duel.paketsoal == 3)
        {
            soal.text = "Soal Sains";
            yield return new WaitForSeconds(2);
            Controller.PlayGame("Sains", "", "sains", 2, "", "", "", true);
        }
        else if (duel.paketsoal == 4)
        {
            soal.text = "Soal Kuliner";
            yield return new WaitForSeconds(2);
            Controller.PlayGame("Kuliner", "", "kuliner", 2, "", "", "", true);
        }
        else if (duel.paketsoal == 5)
        {
            soal.text = "Soal Tubuh dan Kesehatan";
            yield return new WaitForSeconds(2);
            Controller.PlayGame("Tubuh dan kesehatan", "", "tubuh dan kesehatan", 2, "", "", "", true);
        }
    }
}
