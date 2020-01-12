using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DuelSubmit
{
    public int success;
    public string opponent;
    public string status;
    public string score;

    public int MyProperty { get; set; }
}
