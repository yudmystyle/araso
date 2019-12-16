using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Mintonne.QuizApp;

[CustomEditor(typeof(Demo1XmlDownloader))] 
public class EditorDemo1XmlDownloader : Editor 
{
	#if USE_DOTWEEN
	Demo1XmlDownloader XMLDownloaderSR;

	void OnEnable()
	{
		XMLDownloaderSR = ((Demo1XmlDownloader)target);
	}

	public override void OnInspectorGUI ()
	{
		GUI.color = Color.red;

		if(XMLDownloaderSR.Name.Length < 1)
			EditorGUILayout.HelpBox ("Enter the category name below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Category Name Set", MessageType.None);
		}
		
		GUI.color = Color.white;

		XMLDownloaderSR.Name = EditorGUILayout.TextField(new GUIContent("Category Name", "The Category Name"), XMLDownloaderSR.Name);

		EditorGUILayout.Space();

		XMLDownloaderSR.Mode = (DownloadMode)EditorGUILayout.EnumPopup (new GUIContent("XML Location", "Where should we get the XML file for this category."), XMLDownloaderSR.Mode);

		if(XMLDownloaderSR.Mode == DownloadMode.Online)
		{
			GUI.color = Color.red;

			if(XMLDownloaderSR.OnlinePath.Length < 1)
				EditorGUILayout.HelpBox ("Enter the URL to the XML in the field below", MessageType.None);
			else
			{
				GUI.color = Color.green;
				EditorGUILayout.HelpBox ("Download URL is set", MessageType.None);
			}

			GUI.color = Color.white;

			XMLDownloaderSR.OnlinePath = EditorGUILayout.TextField("XML URL Link" , XMLDownloaderSR.OnlinePath);

			EditorGUILayout.Space();
		}
		else if(XMLDownloaderSR.Mode == DownloadMode.Offline)
		{
			GUI.color = Color.red;

			if(XMLDownloaderSR.OfflinePath.Length < 1)
				EditorGUILayout.HelpBox ("Enter the Resources folder path to the XML in the field below", MessageType.None);
			else
			{
				GUI.color = Color.green;
				EditorGUILayout.HelpBox ("Resources path is set", MessageType.None);
			}

			GUI.color = Color.white;

			XMLDownloaderSR.OfflinePath = EditorGUILayout.TextField("Resources Path", XMLDownloaderSR.OfflinePath);

			EditorGUILayout.Space();
		}
		else
		{
			EditorGUILayout.HelpBox ("Enter the URL (optional) & Resources folder path (required) to the XML respectively in the fields below", MessageType.Info);
			XMLDownloaderSR.OnlinePath = EditorGUILayout.TextField("XML URL Link" , XMLDownloaderSR.OnlinePath);
			XMLDownloaderSR.OfflinePath = EditorGUILayout.TextField("Resources Path", XMLDownloaderSR.OfflinePath);

			EditorGUILayout.Space();
		}

		GUI.color = Color.red;

		if(XMLDownloaderSR.PerformanceText == null)
			EditorGUILayout.HelpBox ("Drag the category's performance text from the profile tab component to the field below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Performance text is set", MessageType.None);
		}

		GUI.color = Color.white;

		XMLDownloaderSR.PerformanceText = (Text)EditorGUILayout.ObjectField(new GUIContent("Performance Text", "The category's performance text on the profile page."), XMLDownloaderSR.PerformanceText, typeof(Text),true);

		EditorGUILayout.Space();

		GUI.color = Color.red;

		if(XMLDownloaderSR.LevelPrefName.Length < 1)
			EditorGUILayout.HelpBox ("Enter the Level Playerpref String identifier below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Level Playerpref String is set", MessageType.None);
		}

		GUI.color = Color.white;

		XMLDownloaderSR.LevelPrefName = EditorGUILayout.TextField(new GUIContent("Level PlayerPref Name", "The string identifier used to save the current level of this category"), XMLDownloaderSR.LevelPrefName);

		EditorGUILayout.Space();

		GUI.color = Color.red;

		if(XMLDownloaderSR.AnswerPrefName.Length < 1)
			EditorGUILayout.HelpBox ("Enter the Answer Playerpref String identifier below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Answer Playerpref String is set", MessageType.None);
		}

		GUI.color = Color.white;

		XMLDownloaderSR.AnswerPrefName = EditorGUILayout.TextField(new GUIContent("Answer PlayerPref Name", "The string identifier used to save the correct answers for this category"), XMLDownloaderSR.AnswerPrefName);

		EditorGUILayout.Space();

		GUI.color = Color.red;

		if(XMLDownloaderSR.ScorePrefName.Length < 1)
			EditorGUILayout.HelpBox ("Enter the Score Playerpref String identifier below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Score Playerpref String is set", MessageType.None);
		}

		GUI.color = Color.white;

		XMLDownloaderSR.ScorePrefName = EditorGUILayout.TextField(new GUIContent("Score PlayerPref Name", "The string identifier used to save the highscore for this category"), XMLDownloaderSR.ScorePrefName);

		EditorGUILayout.Space();

		GUI.color = Color.green;

		EditorGUILayout.HelpBox ("Should we shuffle the questions in this category?", MessageType.None);

		GUI.color = Color.white;

		XMLDownloaderSR.ShuffleQuestions = EditorGUILayout.Toggle(new GUIContent("Shuffle Questions", "Should we shuffle the questions in this category?"), XMLDownloaderSR.ShuffleQuestions);

		EditorUtility.SetDirty (XMLDownloaderSR);
	}
	#endif
}