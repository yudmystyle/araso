using System;
using UnityEngine;
using UnityEngine.UI;

public class DetailArenaList : MonoBehaviour
{
    [SerializeField]
    private Text name;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text status;

    [SerializeField]
    DetailArenaListController detailArenaListController;

    private DateTime endDateTime;

    public void SetText(string name, string score, int status)
    {
        this.name.text = name;

        endDateTime = DateTime.ParseExact(PlayerPrefs.GetString("endDate"), "yyyy-MM-dd HH:mm:ss", null);
        if (DateTime.Now > endDateTime)
        {
            this.score.text = score.ToString();
        }
        else
        {
            this.score.text = "-";
        }
        
        if(status == 1)
        {
            this.status.text = "Online";
        }
        else
        {
            this.status.text = "Offline";
        }
        
    }
}
