using System;

[Serializable]
public class DuelAnswerList
{
    public int id;
    public int id_user;
    public int id_duel;
    public string answer;
    public string correct_answer;
    public string question_number;
    public string created_at;
    public string updated_at;

    public int MyProperty { get; set; }
}
