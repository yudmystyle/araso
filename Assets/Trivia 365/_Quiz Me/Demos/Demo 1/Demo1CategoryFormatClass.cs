using Mintonne.QuizApp;
using UnityEngine;

[System.Serializable]
public class Demo1CategoryFormatClass
{
	public string CategoryName;
	public Sprite CategoryImage;
	public DownloadMode Mode;
	public string OnlinePath;
	public string OfflinePath;
	public string LevelPrefName;
	public string AnswerPrefName;
	public string ScorePrefName;
	public bool ShuffleQuestions;

	public Demo1CategoryFormatClass()
	{
		this.CategoryName="New";
		this.OnlinePath=
		this.OfflinePath=
		this.LevelPrefName=
		this.AnswerPrefName=
		this.ScorePrefName=string.Empty;
		this.Mode = DownloadMode.Online;
		this.ShuffleQuestions = true;
		this.CategoryImage = null;
	}
}
