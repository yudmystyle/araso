using Mintonne.QuizApp;
using UnityEngine.UI;
using UnityEngine;

public class Demo2XmlDownloader : MonoBehaviour
{
	public string Name = "";

	public Sprite CategoryImage;

	public DownloadMode Mode = DownloadMode.Online;

	public string OnlinePath = "";

	public string OfflinePath = "";

	public Text PerformanceText;

	public string QuestionPref = "";

	public string AnswerPref = "";

	public string PerformancePref = "";

	public bool ShuffleQuestions = true;
}