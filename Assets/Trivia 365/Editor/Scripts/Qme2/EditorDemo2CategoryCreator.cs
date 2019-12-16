#if !USE_DOTWEEN
#pragma warning disable 0414
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;
using Mintonne.QuizApp;

[CustomEditor(typeof(Demo2CategoryCreator))]
public class EditorDemo2CategoryCreator : Editor
{
    private GUIStyle m_Background;

    Demo2CategoryCreator TargetScript;

    void OnEnable()
    {
        TargetScript = ((Demo2CategoryCreator)target);

		#if USE_DOTWEEN
        m_Background = new GUIStyle();
        m_Background.margin = new RectOffset(2, 2, 2, 2);
        m_Background.normal.background = MakeTex(new Color(0.3f, 0.3f, 0.3f, 0.3f));
		#endif
    }

    public override void OnInspectorGUI()
    {
		#if USE_DOTWEEN
		Undo.RecordObject(TargetScript, " ");

        bool DisabeUI = false;

        GUILayout.Space(10);

		GUI.color = Color.yellow;
        if (TargetScript.CategoryList.Count > 0)
            DisabeUI = false;
        else
            DisabeUI = true;

        EditorGUI.BeginDisabledGroup(DisabeUI);
        if (GUILayout.Button("Update Categories", GUILayout.Height(25)))
            this.TargetScript.UpdateCategories();
        EditorGUI.EndDisabledGroup();

		GUI.color = Color.green;

        if (GUILayout.Button("Add A New Category", GUILayout.Height(25)))
            this.TargetScript.CategoryList.Add(new Demo2CategoryFormatClass());

		GUI.color = Color.cyan;

        EditorGUI.BeginDisabledGroup(!DisabeUI);
        if (GUILayout.Button("Load Existing Categories", GUILayout.Height(25)))
            this.TargetScript.LoadCategories();
        EditorGUI.EndDisabledGroup();

        GUI.color = Color.white;

        if (TargetScript.CategoryList.Count > 0)
        {
            GUILayout.Space(5);

            EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

            GUIStyle myStyle = new GUIStyle("label");
            myStyle.richText = true;
            myStyle.wordWrap = true;
            myStyle.alignment = TextAnchor.MiddleCenter;

            string SettingsTitle = "<size=25>" + "Main Settings" + "</size>";

            GUILayout.Label(SettingsTitle, myStyle, GUILayout.Height(35f));

            GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"), GUILayout.ExpandWidth(true));

            EditorGUILayout.LabelField("Profile Page Settings", GUI.skin.FindStyle("boldLabel"));

			var style = new GUIStyle("toolbarButton");
			style.fontStyle = FontStyle.Bold;

			if (GUILayout.Button("H", style, GUILayout.Width(30)))
				this.TargetScript.ShowHomePage();

			if (GUILayout.Button("P", style, GUILayout.Width(30)))
				this.TargetScript.ShowProfilePage();

            GUILayout.EndHorizontal();

			GUILayout.BeginVertical(m_Background);

            this.TargetScript.ProfileCategoryNameFont = (Font)EditorGUILayout.ObjectField("Category Name Font", this.TargetScript.ProfileCategoryNameFont, typeof(Font), false);
            this.TargetScript.ProfileCategoryFontStyle = (FontStyle)EditorGUILayout.EnumPopup("Category Name Font Style", this.TargetScript.ProfileCategoryFontStyle);
			this.TargetScript.ProfileCategoryNameFontSize = EditorGUILayout.IntSlider("Category Name Font Size", this.TargetScript.ProfileCategoryNameFontSize, 0, 100);

            GUILayout.Space(5);

            this.TargetScript.ProfilePerformanceFont = (Font)EditorGUILayout.ObjectField("Performance Text Font", this.TargetScript.ProfilePerformanceFont, typeof(Font), false);
            this.TargetScript.ProfilePerformanceFontStyle = (FontStyle)EditorGUILayout.EnumPopup("Performance Text Font Style", this.TargetScript.ProfilePerformanceFontStyle);
			this.TargetScript.ProfilePerformanceNameFontSize = EditorGUILayout.IntSlider("Performance Text Font Size", this.TargetScript.ProfilePerformanceNameFontSize,0,100);

            GUILayout.Space(5);

			this.TargetScript.ProfileCategoryImageSize = EditorGUILayout.IntSlider("Category Image Size", this.TargetScript.ProfileCategoryImageSize, 50,200);

			GUILayout.EndVertical();

            GUILayout.Space(5);

            EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

            string CategoriesTitle = "<size=25>" + "Game Categories" + "</size>";

            GUILayout.Label(CategoriesTitle, myStyle, GUILayout.Height(35f));

            GUILayout.Space(10);

            DrawCategoryTabs();

            GUILayout.Space(5);

            EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

			GUI.color = Color.yellow;

			if (GUILayout.Button("Update Categories", GUILayout.Height(25)))
			{
				this.TargetScript.UpdateCategories();
			}

			GUI.color = Color.green;

			if (GUILayout.Button("Add A New Category", GUILayout.Height(25)))
			{
				this.TargetScript.CategoryList.Add(new Demo2CategoryFormatClass());
			}
			GUI.color = Color.white;
        }
		if(GUI.changed)
		{
			EditorUtility.SetDirty(TargetScript);
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
		#endif
    }

	#if USE_DOTWEEN
    private void DrawCategoryTabs()
    {
        for (int i = 0; i < this.TargetScript.CategoryList.Count; i++)
        {
            GUI.skin.textField.wordWrap = false;

            Demo2CategoryFormatClass format = this.TargetScript.CategoryList[i];

            GUILayout.BeginHorizontal(GUI.skin.FindStyle("toolbarButton"), GUILayout.ExpandWidth(true));
			EditorGUILayout.LabelField((i + 1).ToString() + ". " + format.CategoryName.ToString() + " ("+format.Mode.ToString()+" Mode)", GUI.skin.FindStyle("boldLabel"));

			GUI.color = Color.red;

			if (GUILayout.Button("Delete", EditorStyles.toolbarButton, GUILayout.Width(50)))
			{
				EditorApplication.Beep();
				if (EditorUtility.DisplayDialog("Delete Category", "Are you sure you want to delete the " + format.CategoryName.ToString() + " category?", "Yes", "No"))
				{
					GUIUtility.keyboardControl = 0;
					this.TargetScript.CategoryList.RemoveAt(i);
					EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
				}
			}
			GUI.color = Color.white;

            GUILayout.EndHorizontal();

            GUILayout.BeginVertical(m_Background);

            format.CategoryName = EditorGUILayout.TextField(new GUIContent("Category Name", "The Category Name"), format.CategoryName);
            format.CategoryImage = (Sprite)EditorGUILayout.ObjectField("Category Image", format.CategoryImage, typeof(Sprite), false, GUILayout.Height(16));
            format.Mode = (DownloadMode)EditorGUILayout.EnumPopup(new GUIContent("Download Mode", "Where should we get the XML file for this category."), format.Mode);

            if (format.Mode == DownloadMode.Online)
            {
                GUI.color = Color.red;
                if (format.OnlinePath.Length < 1)
                    EditorGUILayout.HelpBox("Enter the XML download link in the field below", MessageType.None);
                else
                {
                    GUI.color = Color.green;
                    EditorGUILayout.HelpBox("Download Link set", MessageType.None);
                }
                GUI.color = Color.white;

				GUILayout.BeginHorizontal ();

                format.OnlinePath = EditorGUILayout.TextField(new GUIContent("Online Path"), format.OnlinePath);

				if (GUILayout.Button ("Test", GUILayout.Width(50))) {
					GUIUtility.keyboardControl = 0;
					TargetScript.StartOnlineXMLTest(format.OnlinePath);
				}

				GUILayout.EndHorizontal ();
            }
            else if (format.Mode == DownloadMode.Offline)
            {
                GUI.color = Color.red;
                if (format.OfflinePath.Length < 1)
                    EditorGUILayout.HelpBox("Enter the local file path for the XML in the field below - Example - XML/OfflineXML", MessageType.None);
                else
                {
                    GUI.color = Color.green;
                    EditorGUILayout.HelpBox("Local file path set", MessageType.None);
                }
                GUI.color = Color.white;

				GUILayout.BeginHorizontal ();

                format.OfflinePath = EditorGUILayout.TextField(new GUIContent("Offline Path"), format.OfflinePath);

				if (GUILayout.Button ("Test", GUILayout.Width(50))) {
					GUIUtility.keyboardControl = 0;
					TargetScript.StartOfflineXMLTest("XML/"+format.OfflinePath);
				}

				GUILayout.EndHorizontal ();
            }
            else
            {
                GUI.color = Color.red;
                if (format.OnlinePath.Length < 1)
                    EditorGUILayout.HelpBox("Enter the XML download link in the field below", MessageType.None);
                else
                {
                    GUI.color = Color.green;
                    EditorGUILayout.HelpBox("Download Link set", MessageType.None);
                }
                GUI.color = Color.white;

				GUILayout.BeginHorizontal ();

                format.OnlinePath = EditorGUILayout.TextField(new GUIContent("Online Path"), format.OnlinePath);

				if (GUILayout.Button ("Test", GUILayout.Width(50))) {
					GUIUtility.keyboardControl = 0;
					TargetScript.StartOnlineXMLTest(format.OnlinePath);
				}

				GUILayout.EndHorizontal ();

                GUI.color = Color.red;
                if (format.OfflinePath.Length < 1)
                    EditorGUILayout.HelpBox("Enter the local file path for the XML in the field below - Example - XML/OfflineXML", MessageType.None);
                else
                {
                    GUI.color = Color.green;
                    EditorGUILayout.HelpBox("Local file path set", MessageType.None);
                }
                GUI.color = Color.white;

				GUILayout.BeginHorizontal ();

                format.OfflinePath = EditorGUILayout.TextField(new GUIContent("Offline Path"), format.OfflinePath);

				if (GUILayout.Button ("Test", GUILayout.Width(50))) {
					GUIUtility.keyboardControl = 0;
					TargetScript.StartOfflineXMLTest("XML/"+format.OfflinePath);
				}

				GUILayout.EndHorizontal ();
            }

			GUILayout.Space(10);

            format.QuestionPref = EditorGUILayout.TextField(new GUIContent("Level PlayerPref Name", "The string identifier used to save the current level of this category"), format.QuestionPref);

            format.AnswerPref = EditorGUILayout.TextField(new GUIContent("Answer PlayerPref Name", "The string identifier used to save the correct answers for this category"), format.AnswerPref);

            format.PerformancePref = EditorGUILayout.TextField(new GUIContent("Score PlayerPref Name", "The string identifier used to save the highscore for this category"), format.PerformancePref);

			if (GUILayout.Button("Autofill PlayerPrefs"))
			{
				string Catname = format.CategoryName.Replace(" ", string.Empty);
				if (Catname.Length > 20)
					Catname.Substring(0, 20);

				string NewQuestionPref = "Lvl" + Catname.ToString();
				string NewAnswerPref = "Ans" + Catname.ToString();
				string NewPerformancePref = "Perf" + Catname.ToString();

				if (format.QuestionPref == string.Empty)
					format.QuestionPref = NewQuestionPref;
				if (format.AnswerPref == string.Empty)
					format.AnswerPref = NewAnswerPref;
				if (format.PerformancePref == string.Empty)
					format.PerformancePref = NewPerformancePref;

				if (!String.Equals(format.QuestionPref, NewQuestionPref))
				{
					if (EditorUtility.DisplayDialog("Question PlayerPref Warning", "All previous saved progress will be lost if the Question PlayerPref name is changed. Do not change the name if you have published your app.\nAre you sure you want to change it?", "Yes", "Cancel"))
					{
						format.QuestionPref = NewQuestionPref;
					}
				}
				if (!String.Equals(format.AnswerPref, NewAnswerPref))
				{
					if (EditorUtility.DisplayDialog("Answer PlayerPref Warning", "All previous saved progress will be lost if the Answer PlayerPref name is changed. Do not change the name if you have published your app.\nAre you sure you want to change it?", "Yes", "Cancel"))
					{
						format.AnswerPref = NewAnswerPref;
					}
				}
				if (!String.Equals(format.PerformancePref, NewPerformancePref))
				{
					if (EditorUtility.DisplayDialog("Performance PlayerPref Warning", "All previous saved progress will be lost if the Performance PlayerPref name is changed. Do not change the name if you have published your app.\nAre you sure you want to change it?", "Yes", "Cancel"))
					{
						format.PerformancePref = NewPerformancePref;
					}
				}
			}

			GUILayout.Space(5);

            format.ShuffleQuestions = EditorGUILayout.Toggle(new GUIContent("Shuffle Questions", "Should we shuffle the questions in this category?"), format.ShuffleQuestions);
            GUILayout.EndVertical();

            GUILayout.Space(10);
        }

		EditorUtility.SetDirty(TargetScript);
    }

    private Texture2D MakeTex(Color col)
    {
        Color[] pix = new Color[1 * 1];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        result.hideFlags = HideFlags.HideAndDontSave;
        result.SetPixels(pix);
        result.Apply();

        return result;
    }
	#endif
}