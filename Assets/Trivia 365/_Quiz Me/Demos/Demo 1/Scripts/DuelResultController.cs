using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DuelResultController : MonoBehaviour
{
    [SerializeField]
    public Text promptText;
    [SerializeField]
    public Text you;
    [SerializeField]
    public Text opponent;
    [SerializeField]
    public Text vs;
    [SerializeField]
    public GameObject win;
    [SerializeField]
    public GameObject lost;
    [SerializeField]
    public GameObject draw;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        win.SetActive(false);
        lost.SetActive(false);
        draw.SetActive(false);
        you.text = "";
        vs.text = "";
        opponent.text = "";

        CallDuelResult();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallDuelResult()
    {
        StartCoroutine(GetDuelResult());
    }

    IEnumerator GetDuelResult()
    {
        WWWForm form = new WWWForm();
        form.AddField("id_duel", PlayerPrefs.GetInt("idDuel"));
        form.AddField("score", PlayerPrefs.GetString("duelScore"));

        WWW www = new WWW(ApiConstant.SERVER + "/submitduel", form);
        yield return www;

        Debug.Log(www.text);

        if (www.text.Contains("\"success\":1"))
        {
            DuelSubmit duelSubmit = new DuelSubmit();
            duelSubmit = JsonUtility.FromJson<DuelSubmit>(www.text);

            if (duelSubmit.status == "Playing")
            {
                promptText.text = "Sedang menunggu lawan menyelesaikan permainan";
                vs.text = "...";

                yield return new WaitForSeconds(3);
                CallDuelResult();
            }
            else if (duelSubmit.status == "Done")
            {
                promptText.text = "";
                vs.text = "VS";

                you.text = "You : " + PlayerPrefs.GetString("duelScore");
                opponent.text = duelSubmit.opponent + " : " + duelSubmit.score;

                int yourScore = System.Int16.Parse(PlayerPrefs.GetString("duelScore"));
                int opponentScore = System.Int16.Parse(duelSubmit.score);
                if (yourScore > opponentScore)
                {
                    win.SetActive(true);
                }
                else if(yourScore < opponentScore)
                {
                    lost.SetActive(true);
                }
                else
                {
                    draw.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("Create duel Failed.Error #" + www.text);
        }
    }
}
