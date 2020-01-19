using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text idSoal;
    [SerializeField]
    private Text uniqueCode;
    [SerializeField]
    private Text waktuMulai;
    [SerializeField]
    private Text waktuSelesai;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Button joinButton;
    [SerializeField]
    private Button detailButton;

    [SerializeField]
    ButtonListControl buttonListControl;

    private int currentIdSoal;
    private DateTime startDateTime;
    private DateTime endDateTime;
    private int idArena;

    public void SetText(int idSoal, string uniqueCodeString, string waktuMulaiString, string waktuSelesaiString, string scoreString, int idArena)
    {
        this.idSoal.text = idSoal.ToString();
        uniqueCode.text = uniqueCodeString;
        waktuMulai.text = waktuMulaiString;
        waktuSelesai.text = waktuSelesaiString;

        //Button Checker
        startDateTime = DateTime.ParseExact(waktuMulaiString, "yyyy-MM-dd HH:mm:ss", null);
        endDateTime = DateTime.ParseExact(waktuSelesaiString, "yyyy-MM-dd HH:mm:ss", null);

        if (DateTime.Now > endDateTime)
        {
            score.text = scoreString;
        }
        else
        {
            score.text = "-";
        }

        if(DateTime.Now < startDateTime || DateTime.Now > endDateTime || !scoreString.Equals("-"))
        {
            joinButton.interactable = false;
        }
        else
        {
            //Click Action
            currentIdSoal = idSoal;
            this.idArena = idArena;
            joinButton.onClick.AddListener(() => JoinOnClick(idSoal, idArena));
        }
        detailButton.onClick.AddListener(() => DetailOnClick(idArena, waktuSelesaiString));
    }

    public void JoinOnClick(int idSoal, int idArena)
    {
        buttonListControl.JoinButtonClicked(currentIdSoal, idArena);
    }

    public void DetailOnClick(int idArena, string endDate)
    {
        Debug.Log("ID Arena : " + idArena);
        buttonListControl.DetailButtonClicked(idArena, endDate);
    }
}
