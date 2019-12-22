using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject areaTemplate;
    [SerializeField]
    private GameObject areaListContent;

    [SerializeField]
    Demo1Controller Controller;

    void Start()
    {
        CallGetArena();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("isDataUpdated", 0) == 1)
        {
            Debug.Log("Child Count : " + areaListContent.transform.childCount);

            foreach (Transform child in areaListContent.transform)
            {
                if(child.GetSiblingIndex() != 0) Destroy(child.gameObject);
            }

            CallGetArena();
            PlayerPrefs.SetInt("isDataUpdated", 0);
        }
    }

    public void CallGetArena()
    {
        StartCoroutine(GetArena());
    }

    IEnumerator GetArena()
    {
        WWW www = new WWW("localhost:8000/historiarena");
        yield return www;

        //string jsonString = "{\r\n  \"data\": [\r\n    {\r\n      \"id_arena\": 1,\r\n      \"id_paketsoal\": 1,\r\n      \"uniquecode\": \"jokodfs\",\r\n      \"waktu_mulai\": \"2019-12-22 02:00:00\",\r\n      \"waktu_selesai\": \"2019-12-25 12:00:00\",\r\n      \"score\": \"\"\r\n    },\r\n    {\r\n      \"id_arena\": 2,\r\n      \"id_paketsoal\": 2,\r\n      \"uniquecode\": \"jrrodfs\",\r\n      \"waktu_mulai\": \"2019-12-23 12:00:00\",\r\n      \"waktu_selesai\": \"2019-12-25 12:00:00\",\r\n      \"score\": \"\"\r\n    },\r\n    {\r\n      \"id_arena\": 3,\r\n      \"id_paketsoal\": 3,\r\n      \"uniquecode\": \"jokwwfs\",\r\n      \"waktu_mulai\": \"2019-12-23 12:00:00\",\r\n      \"waktu_selesai\": \"2019-12-25 12:00:00\",\r\n      \"score\": \"\"\r\n    }\r\n  ] \r\n}";

        ArenaObject arenaObject = new ArenaObject();
        arenaObject = JsonUtility.FromJson<ArenaObject>(www.text);
        //Debug.Log(arenaObject.data.Count);

        for (int i = 0; i < arenaObject.data.Count; i++)
        {
            GameObject area = Instantiate(areaTemplate) as GameObject;
            area.SetActive(true);

            area.GetComponent<ButtonListButton>().SetText(
                arenaObject.data[i].id_paketsoal,
                arenaObject.data[i].uniquecode,
                arenaObject.data[i].waktu_mulai,
                arenaObject.data[i].waktu_selesai,
                arenaObject.data[i].score,
                arenaObject.data[i].id_arena
            );

            area.transform.SetParent(areaTemplate.transform.parent, false);
        }
    }

    public void ButtonClicked(int idSoal, int idArena)
    {
        Debug.Log(idSoal);

        PlayerPrefs.SetInt("idArena", idArena);
        PlayerPrefs.SetInt("isArenaOnline", 1);

        if (idSoal == 1)
        {
            Controller.PlayGame("Bahasa Indonesia", "", "bahasa", 2, "", "", "", true);
        }
        else if (idSoal == 2)
        {
            Controller.PlayGame("Matematika", "", "matematika", 2, "", "", "", true);
        }
    }

}
