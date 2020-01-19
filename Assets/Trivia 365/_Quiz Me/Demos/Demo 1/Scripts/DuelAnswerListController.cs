using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelAnswerListController : MonoBehaviour
{
    [SerializeField]
    public GameObject duelAnswerTemplate;
    [SerializeField]
    public GameObject duelAnswerListContent;

    string yourAnswerStatus;
    string oppAnswerStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        foreach (Transform child in duelAnswerListContent.transform)
        {
            if (child.GetSiblingIndex() != 0) Destroy(child.gameObject);
        }

        CallGetDuelAnswer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallGetDuelAnswer()
    {
        StartCoroutine(GetDuelAnswer());
    }

    IEnumerator GetDuelAnswer()
    {
        //Your answer
        WWWForm yourForm = new WWWForm();
        yourForm.AddField("id_duel", PlayerPrefs.GetInt("idDuel"));
        WWW yourwww = new WWW(ApiConstant.SERVER + "/getyouranswers", yourForm);
        yield return yourwww;
        Debug.Log(yourwww.text);
        DuelAnswer yourAnswer = JsonUtility.FromJson<DuelAnswer>(yourwww.text);

        //Opponent answer
        WWWForm oppForm = new WWWForm();
        oppForm.AddField("id_duel", PlayerPrefs.GetInt("idDuel"));
        WWW oppwww = new WWW(ApiConstant.SERVER + "/getopponentanswers", oppForm);
        yield return oppwww;
        Debug.Log(oppwww.text);
        DuelAnswer oppAnswer = JsonUtility.FromJson<DuelAnswer>(oppwww.text);

        for (int i = 0; i < yourAnswer.data.Count; i++)
        {
            GameObject duel = Instantiate(duelAnswerTemplate) as GameObject;
            duel.SetActive(true);

            yourAnswerStatus = "Salah";
            oppAnswerStatus = "Salah";

            if (yourAnswer.data[i].answer.Equals(yourAnswer.data[i].correct_answer))
            {
                yourAnswerStatus = "Benar";
            }

            if (oppAnswer.data[i].answer.Equals(oppAnswer.data[i].correct_answer))
            {
                oppAnswerStatus = "Benar";
            }

            duel.GetComponent<DuelAnswerController>().SetText(
                "Soal " + yourAnswer.data[i].question_number,
                //yourAnswer.data[i].answer,
                " ",
                yourAnswerStatus,
                //oppAnswer.data[i].answer,
                " ",
                oppAnswerStatus
            );

            duel.transform.SetParent(duelAnswerTemplate.transform.parent, false);
        }
    }
}
