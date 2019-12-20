using System.Collections;
using System.Collections.Generic;
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

    public void SetText(string idSoalString, string uniqueCodeString, string waktuMulaiString, string waktuSelesaiString, string scoreString)
    {
        idSoal.text = idSoalString;
        uniqueCode.text = uniqueCodeString;
        waktuMulai.text = waktuMulaiString;
        waktuSelesai.text = waktuSelesaiString;
        score.text = scoreString;
    }

    public void OnClick()
    {

    }
}
