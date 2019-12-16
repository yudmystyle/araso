using Mintonne.QuizApp;
using UnityEngine;
using System;

[Serializable]
public class Demo2CategoryFormatClass
{
    public string CategoryName;
    public Sprite CategoryImage;
    public DownloadMode Mode;
    public string OnlinePath;
    public string OfflinePath;
    public string QuestionPref;
    public string AnswerPref;
    public string PerformancePref;
    public bool ShuffleQuestions;

    public Demo2CategoryFormatClass()
    {
        this.CategoryName = "New";
        this.OnlinePath =
        this.OfflinePath =
        this.QuestionPref =
        this.AnswerPref =
        this.PerformancePref = string.Empty;
        this.Mode = DownloadMode.Online;
        this.ShuffleQuestions = true;
        this.CategoryImage = null;
    }
}
