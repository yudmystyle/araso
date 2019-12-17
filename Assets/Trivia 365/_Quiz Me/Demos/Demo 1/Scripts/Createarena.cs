using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Createarena : MonoBehaviour
{
    public Dropdown idSoal;
    public Dropdown tahunMulai;
    public Dropdown bulanMulai;
    public Dropdown hariMulai;
    public Dropdown jamMulai;
    public Dropdown menitMulai;
    public Dropdown tahunSelesai;
    public Dropdown bulanSelesai;
    public Dropdown hariSelesai;
    public Dropdown jamSelesai;
    public Dropdown menitSelesai;

    public Text promptText;

    private string waktuMulai;
    private string waktuSelesai;

    public void CallCreatearena()
    {
        StartCoroutine(Makearena());
    }

    IEnumerator Makearena()
    {
        waktuMulai = tahunMulai.options[tahunMulai.value].text + "-" + bulanMulai.options[bulanMulai.value].text + "-" + hariMulai.options[hariMulai.value].text + " " + jamMulai.options[jamMulai.value].text + ":" + menitMulai.options[menitMulai.value].text + ":00";
        waktuSelesai = tahunSelesai.options[tahunSelesai.value].text + "-" + bulanSelesai.options[bulanSelesai.value].text + "-" + hariSelesai.options[hariSelesai.value].text + " " + jamSelesai.options[jamSelesai.value].text + ":" + menitSelesai.options[menitSelesai.value].text + ":00";

        //waktuSelesai = "2019-12-20 19:00:00";
        WWWForm form = new WWWForm();
        form.AddField("id_paketsoal", idSoal.value.ToString());
        form.AddField("waktu_mulai", waktuMulai);
        form.AddField("waktu_selesai", waktuSelesai);

        Debug.Log("waktu mulai :" + waktuMulai);
        Debug.Log("waktu selesai :" + waktuSelesai);

        WWW www = new WWW("localhost:8000/createarena", form);
        yield return www;
        if (www.text.Contains("\"success\":1"))
        {
            Debug.Log("Create Arena Successfully");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Demo1");
        }
        else if (www.text.Contains("arena tidak dapat dibuat karena batas waktu akhir sudah lewat"))
        {
            promptText.text = "arena tidak dapat dibuat karena batas waktu akhir sudah lewat";
        }
        else
        {
            Debug.Log("Create arena Failed.Error #" + www.text);
        }
    }
}
