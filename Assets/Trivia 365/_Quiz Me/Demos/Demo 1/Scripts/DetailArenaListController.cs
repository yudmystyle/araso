using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailArenaListController : MonoBehaviour
{
    [SerializeField]
    private GameObject detailArenaTemplate;
    [SerializeField]
    private GameObject detailArenaListContent;

    [SerializeField]
    Demo1Controller Controller;

    //Data
    string idArena;

    void Start()
    {
        CallGetDetailArena();
    }

    private void Update()
    {
        //if (PlayerPrefs.GetInt("isDataUpdated", 0) == 1)
        //{
        //    Debug.Log("Child Count : " + detailArenaListContent.transform.childCount);

        //    foreach (Transform child in detailArenaListContent.transform)
        //    {
        //        if (child.GetSiblingIndex() != 0) Destroy(child.gameObject);
        //    }

        //    CallGetDetailArena();
        //    PlayerPrefs.SetInt("isDataUpdated", 0);
        //}
    }

    public void CallGetDetailArena()
    {
        StartCoroutine(GetDetailArena());
    }

    IEnumerator GetDetailArena()
    {
        WWWForm form = new WWWForm();
        form.AddField("id_arena", PlayerPrefs.GetInt("idArena"));
        WWW www = new WWW("localhost:8000/detailarena", form);
        yield return www;

        Debug.Log(www.text);

        DetailArenaObject detailArenaObject = new DetailArenaObject();
        detailArenaObject = JsonUtility.FromJson<DetailArenaObject>(www.text);

        for (int i = 0; i < detailArenaObject.data.Count; i++)
        {
            GameObject detailArena = Instantiate(detailArenaTemplate) as GameObject;
            detailArena.SetActive(true);

            detailArena.GetComponent<DetailArenaList>().SetText(
                detailArenaObject.data[i].name,
                detailArenaObject.data[i].score,
                detailArenaObject.data[i].isonline
            );

            detailArena.transform.SetParent(detailArenaTemplate.transform.parent, false);
        }
    }
}
