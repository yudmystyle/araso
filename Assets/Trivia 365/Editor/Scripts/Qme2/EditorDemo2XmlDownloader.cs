using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Mintonne.QuizApp;

[CustomEditor(typeof(Demo2XmlDownloader))] 
public class EditorDemo2XmlDownloader : Editor 
{
	Demo2XmlDownloader TargetScript;

	void OnEnable()
	{
		TargetScript = ((Demo2XmlDownloader)target);
	}

	public override void OnInspectorGUI ()
	{
		GUI.color = Color.red;

		if(TargetScript.Name.Length < 1)
			EditorGUILayout.HelpBox ("Enter the category name below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Category Name Set", MessageType.None);
		}

		GUI.color = Color.white;

		TargetScript.Name = EditorGUILayout.TextField(new GUIContent("Category Name", "The Category Name"), TargetScript.Name);

		EditorGUILayout.Space();

		GUI.color = Color.red;

		if(TargetScript.CategoryImage == null)
			EditorGUILayout.HelpBox ("Drag a sprite from your assets folder to the field below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Category Image Set", MessageType.None);
		}

		GUI.color = Color.white;

		TargetScript.CategoryImage = (Sprite)EditorGUILayout.ObjectField(new GUIContent("Category Image", "The category image."), TargetScript.CategoryImage, typeof(Sprite),false, GUILayout.Height(16));

		EditorGUILayout.Space();

		TargetScript.Mode = (DownloadMode)EditorGUILayout.EnumPopup (new GUIContent("XML Location", "Where should we get the XML file for this category."), TargetScript.Mode);

		if(TargetScript.Mode == DownloadMode.Online)
		{
			GUI.color = Color.red;

			if(TargetScript.OnlinePath.Length < 1)
				EditorGUILayout.HelpBox ("Enter the URL to the XML in the field below", MessageType.None);
			else
			{
				GUI.color = Color.green;
				EditorGUILayout.HelpBox ("Download URL is set", MessageType.None);
			}

			GUI.color = Color.white;

			TargetScript.OnlinePath = EditorGUILayout.TextField("XML Download Link" , TargetScript.OnlinePath);

			EditorGUILayout.Space();
		}
		else if(TargetScript.Mode == DownloadMode.Offline)
		{
			GUI.color = Color.red;

			if(TargetScript.OfflinePath.Length < 1)
				EditorGUILayout.HelpBox ("Enter the Resources folder path to the XML in the field below", MessageType.None);
			else
			{
				GUI.color = Color.green;
				EditorGUILayout.HelpBox ("Resources path is set", MessageType.None);
			}

			GUI.color = Color.white;

			TargetScript.OfflinePath = EditorGUILayout.TextField("Resources Path", TargetScript.OfflinePath);

			EditorGUILayout.Space();
		}
		else if(TargetScript.Mode == DownloadMode.Hybrid)
		{
			EditorGUILayout.HelpBox ("Enter the URL (optional) & Resources folder path (required) to the XML respectively in the fields below", MessageType.Info);

			TargetScript.OnlinePath = EditorGUILayout.TextField("XML Download Link" , TargetScript.OnlinePath);
			TargetScript.OfflinePath = EditorGUILayout.TextField("Resources Path", TargetScript.OfflinePath);

			EditorGUILayout.Space();
		}

		GUI.color = Color.red;

		if(TargetScript.PerformanceText == null)
			EditorGUILayout.HelpBox ("Drag the category's performance text from the profile tab component to the field below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Performance Text Set", MessageType.None);
		}

		GUI.color = Color.white;

		TargetScript.PerformanceText = (Text)EditorGUILayout.ObjectField(new GUIContent("Performance Text", "The category's performance text from the profile page."), TargetScript.PerformanceText, typeof(Text),true);

		EditorGUILayout.Space();

		GUI.color = Color.red;

		if(TargetScript.QuestionPref.Length < 1)
			EditorGUILayout.HelpBox ("Enter the Questions PlayerPref String identifier below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Questions PlayerPref String Set", MessageType.None);
		}

		GUI.color = Color.white;

		TargetScript.QuestionPref = EditorGUILayout.TextField(new GUIContent("Questions PlayerPref Name", "The string identifier used to save the questions asked for this category"), TargetScript.QuestionPref);

		EditorGUILayout.Space();

		GUI.color = Color.red;

		if(TargetScript.AnswerPref.Length < 1)
			EditorGUILayout.HelpBox ("Enter the Answers PlayerPref String identifier below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Answers PlayerPref String Set", MessageType.None);
		}

		GUI.color = Color.white;

		TargetScript.AnswerPref = EditorGUILayout.TextField(new GUIContent("Answers PlayerPref Name", "The string identifier used to save the total correct answers for this category"), TargetScript.AnswerPref);

		EditorGUILayout.Space();

		GUI.color = Color.red;

		if(TargetScript.PerformancePref.Length < 1)
			EditorGUILayout.HelpBox ("Enter the Performance PlayerPref String identifier below", MessageType.None);
		else
		{
			GUI.color = Color.green;
			EditorGUILayout.HelpBox ("Performance PlayerPref String Set", MessageType.None);
		}

		GUI.color = Color.white;

		TargetScript.PerformancePref = EditorGUILayout.TextField(new GUIContent("Performance PlayerPref Name", "The string identifier used to save the performance stats for this category"), TargetScript.PerformancePref);

		EditorGUILayout.Space();

		GUI.color = Color.green;

		EditorGUILayout.HelpBox ("Should we shuffle the questions in this category?", MessageType.None);

		GUI.color = Color.white;

		TargetScript.ShuffleQuestions = EditorGUILayout.Toggle(new GUIContent("Shuffle Questions", "Should we shuffle the questions in this category?"), TargetScript.ShuffleQuestions);

		if (GUI.changed) 
			EditorUtility.SetDirty (TargetScript);
	}
}