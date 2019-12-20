using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject areaTemplate;

    void Start()
    {
        CallGetArena();
    }

    public void CallGetArena()
    {
        StartCoroutine(GetArena());
    }

    IEnumerator GetArena()
    {
        WWW www = new WWW("localhost:8000/historiarena");
        yield return www;

        ArenaObject arenaObject = new ArenaObject();
        arenaObject = JsonUtility.FromJson<ArenaObject>(www.text);
        Debug.Log(arenaObject.data.Count);
        for (int i = 0; i < arenaObject.data.Count + 1; i++)
        {
            GameObject area = Instantiate(areaTemplate) as GameObject;
            area.SetActive(true);

            area.GetComponent<ButtonListButton>().SetText(
                "ID Paket : " + arenaObject.data[i].id_paketsoal,
                "Unique Code : " + arenaObject.data[i].uniquecode,
                "Mulai : " + arenaObject.data[i].waktu_mulai,
                "Selesai : " + arenaObject.data[i].waktu_selesai,
                "Score : " + arenaObject.data[i].score
            );

            area.transform.SetParent(areaTemplate.transform.parent, false);
        }
    }

}
