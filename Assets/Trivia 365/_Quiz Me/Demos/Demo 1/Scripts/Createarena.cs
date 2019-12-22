using System;
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

    private string year;
    private DateTime dateTime;
    private List<string> yearList = new List<string>();
    private List<string> monthList = new List<string>();
    private List<string> dayList = new List<string>();
    private List<string> hourList = new List<string>();
    private List<string> minuteList = new List<string>();

    public void Start()
    {
        //Year List
        for(int i = 0; i < 5; i++)
        {
            dateTime = DateTime.Now.AddYears(i);
            year = dateTime.ToString("yyyy");
            Debug.Log(year);
            yearList.Add(year);
        }
        tahunMulai.AddOptions(yearList);
        tahunSelesai.AddOptions(yearList);

        //Month
        for (int i = 1; i <= 12; i++)
        {
            monthList.Add(i.ToString());
        }
        bulanMulai.AddOptions(monthList);
        bulanSelesai.AddOptions(monthList);

        //Day
        for (int i = 1; i <= 31; i++)
        {
            dayList.Add(i.ToString());
        }
        hariMulai.AddOptions(dayList);
        hariSelesai.AddOptions(dayList);

        //Hour
        for (int i = 0; i <= 24; i++)
        {
            if (i < 10)
            {
                hourList.Add("0" + i.ToString());
            }
            else
            {
                hourList.Add(i.ToString());
            }
        }
        jamMulai.AddOptions(hourList);
        jamSelesai.AddOptions(hourList);

        //Minute
        for (int i = 0; i <= 60; i++)
        {
            if(i < 10)
            {
                minuteList.Add("0" + i.ToString());
            }
            else
            {
                minuteList.Add(i.ToString());
            }
        }
        menitMulai.AddOptions(minuteList);
        menitSelesai.AddOptions(minuteList);
    }

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
        form.AddField("id_paketsoal", idSoal.options[idSoal.value].text);
        form.AddField("waktu_mulai", waktuMulai);
        form.AddField("waktu_selesai", waktuSelesai);

        Debug.Log("waktu mulai :" + waktuMulai);
        Debug.Log("waktu selesai :" + waktuSelesai);

        WWW www = new WWW(ApiConstant.SERVER + "/createarena", form);
        yield return www;
        if (www.text.Contains("\"success\":1"))
        {
            Debug.Log("Create Arena Successfully");
            UniqueCodeObject uniqueCodeObject = new UniqueCodeObject();
            uniqueCodeObject = JsonUtility.FromJson<UniqueCodeObject>(www.text);
            promptText.text = "Uniquecode : " + uniqueCodeObject.uniquecode;
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Demo1");
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
