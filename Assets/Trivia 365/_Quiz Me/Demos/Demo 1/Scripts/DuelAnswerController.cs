using UnityEngine;
using UnityEngine.UI;

public class DuelAnswerController : MonoBehaviour
{
    [SerializeField]
    public Text question;
    [SerializeField]
    public Text yourAnswer;
    [SerializeField]
    public Text yourStatus;
    [SerializeField]
    public Text oppAnswer;
    [SerializeField]
    public Text oppStatus;
    [SerializeField]
    public Image list;
    [SerializeField]
    public Sprite redList;
    [SerializeField]
    public Sprite greenList;

    public void SetText(string strQuestion, string strYourAnswer, string strYourStatus, string strOppAnswer, string strOppStatus)
    {
        question.text = strQuestion;
        yourAnswer.text = strYourAnswer;
        yourStatus.text = strYourStatus;
        if (strYourStatus.Equals("Benar"))
        {
            list.sprite = greenList;
        }
        else
        {
            list.sprite = redList;
        }
        oppAnswer.text = strOppAnswer;
        oppStatus.text = strOppStatus;
    }
}
