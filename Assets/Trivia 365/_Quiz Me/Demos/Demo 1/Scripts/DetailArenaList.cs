using System.Collections;
using System.Collections.Generic;
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

    public void SetText(string name, string score, int status)
    {
        this.name.text = name;
        this.score.text = score.ToString();
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
