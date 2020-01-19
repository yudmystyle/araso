using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DetailArenaDataObject
{
    public int id_user;
    public string name;
    public int isonline;
    public string score;
    public string created_at;
    public string updated_at;

    public int MyProperty { get; set; }
}
