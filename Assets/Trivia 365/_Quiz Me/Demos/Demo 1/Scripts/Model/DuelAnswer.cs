using System;
using System.Collections.Generic;

[Serializable]
public class DuelAnswer
{
    public int success;
    public List<DuelAnswerList> data;

    public int MyProperty { get; set; }
}
