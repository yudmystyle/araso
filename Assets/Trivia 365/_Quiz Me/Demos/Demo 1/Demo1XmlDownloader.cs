using Mintonne.QuizApp;
using UnityEngine.UI;
using UnityEngine;

public class Demo1XmlDownloader : MonoBehaviour 
{
	Demo1Controller Controller;
	public string Name = "";
	public DownloadMode Mode = DownloadMode.Online;
	public string OnlinePath = "";
	public string OfflinePath = "";
	public Text PerformanceText;
	public string LevelPrefName = "";
	public string AnswerPrefName = "";
	public string ScorePrefName = "";
	public bool ShuffleQuestions = true;

	#if USE_DOTWEEN
	private void Awake()
	{
		Controller = FindObjectOfType(typeof(Demo1Controller)) as Demo1Controller;

		gameObject.GetComponent<Button> ().onClick.AddListener (() => {
			LoadXML ();
		});
	}

	public void LoadXML() 
	{
		if(Mode == DownloadMode.Online)
			Controller.PlayGame(Name,OnlinePath,"",1,LevelPrefName,AnswerPrefName,ScorePrefName,ShuffleQuestions);
		else if(Mode == DownloadMode.Offline)
			Controller.PlayGame(Name,"",OfflinePath,2,LevelPrefName,AnswerPrefName,ScorePrefName,ShuffleQuestions);
		else
			Controller.PlayGame(Name,OnlinePath,OfflinePath,3,LevelPrefName,AnswerPrefName,ScorePrefName,ShuffleQuestions);
	}

	void OnDestroy()
	{
		gameObject.GetComponent<Button> ().onClick.RemoveListener (() => {
			LoadXML ();
		});
	}
	#endif
}