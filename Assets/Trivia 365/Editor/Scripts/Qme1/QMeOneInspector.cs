#if (UNITY_ANDROID || UNITY_IOS)
#define SUPPORTED_PLATFORM
#endif

#if !USE_DOTWEEN
#pragma warning disable 0414
#endif

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Demo1Controller))]
public class QMeOneInspector : Editor
{
	private GUIStyle m_Background;

	#if USE_DOTWEEN

	#if SUPPORTED_PLATFORM
	private bool ShowAdmob = false;
	private bool ShowUnityAds = false;
	private static readonly char[] defineSeperators = new char[] { ';', ',', ' ' };
	#endif

	#if USE_ADMOB	
	SerializedProperty appId = null;
	SerializedProperty InterstitialAdUnitId = null;
    SerializedProperty AskForConsent = null;
    SerializedProperty ShowAfterHowManyGameovers = null;
	SerializedProperty TestMode = null;
#endif

#if USE_UNITYADS
	SerializedProperty GameId = null;
	SerializedProperty AdLimit = null;
	SerializedProperty UnityTestMode = null;
#endif

    SerializedProperty ConsentCanvas = null;
    SerializedProperty MenuCanvas = null;
	SerializedProperty CategoryCanvas = null;
	SerializedProperty GameCanvas = null;
	SerializedProperty ScreenshotCanvas = null;
	SerializedProperty PauseCanvas = null;
	SerializedProperty GameOverCanvas = null;

	SerializedProperty ProfileButton = null;
    SerializedProperty LogoutButton = null;
    SerializedProperty BackButton = null;
	SerializedProperty SettingsButton = null;
	SerializedProperty Homepage = null;
	SerializedProperty ProfilePage = null;
	SerializedProperty SettingsPage = null;
    SerializedProperty CachePanel = null;
    SerializedProperty UniquecodePanel = null;
    SerializedProperty DuelPanel = null;
    SerializedProperty CreateArenaPanel = null;
    SerializedProperty HistoriArenaPanel = null;
    SerializedProperty DetailArenaPanel = null;
    SerializedProperty TopBarText = null;
	SerializedProperty ProfileAvatar = null;
	SerializedProperty Username = null;
	SerializedProperty SoundButton = null;
	SerializedProperty VibrateButton = null;
    SerializedProperty AdExperienceButton = null;

    SerializedProperty CategoryGroup = null;
	SerializedProperty ErrorPanel = null;
	SerializedProperty Loading = null;

	SerializedProperty CurrentCategoryText = null;
	SerializedProperty QuestionDisplay = null;
    SerializedProperty AltQuestionDisplay = null;
    SerializedProperty ImageDispay = null;
	SerializedProperty ScoreText = null;
	SerializedProperty ImageLoadingAnimation = null;
	SerializedProperty ImageErrorPanel = null;
	SerializedProperty TimerText = null;
	SerializedProperty LivesText = null;
	SerializedProperty LongBarAnswers = null;
	SerializedProperty ShortBarAnswers = null;
	SerializedProperty ContinueButton = null;
	SerializedProperty QuitPanel = null;
	SerializedProperty ReviewButton = null;
	SerializedProperty ReviewPanel = null;
	SerializedProperty ScreenshotView = null;
	SerializedProperty VideoAdPanel = null;

	SerializedProperty FinalScore = null;
	SerializedProperty CurHighscore = null;

	SerializedProperty TimerAmount = null;
	SerializedProperty DownloadTimeout = null;
	SerializedProperty LivesCount = null;
	SerializedProperty Avatars = null;
	SerializedProperty CorrectSound = null;
	SerializedProperty FailSound = null;
	SerializedProperty TickingSound = null;
	SerializedProperty CorrectColor = null;
	SerializedProperty FailColor = null;
	SerializedProperty EnablePausing = null;
	SerializedProperty Questions = null;
	SerializedProperty AnswersAvailable = null;

	SerializedProperty QuestionDisplaySc = null;
	SerializedProperty ImageDispaySc = null;
	SerializedProperty LongBarAnswersSc = null;
	SerializedProperty ShortBarAnswersSc = null;
	#endif

	void OnEnable()
	{
		m_Background = new GUIStyle();
		m_Background.margin = new RectOffset(2, 2, 2, 2);
		m_Background.normal.background = MakeTex(new Color(0.3f, 0.3f, 0.3f, 0.3f));

		#if USE_ADMOB
		appId = serializedObject.FindProperty ("appId");
		InterstitialAdUnitId = serializedObject.FindProperty ("InterstitialAdUnitId");
        AskForConsent = serializedObject.FindProperty("AskForConsent");
        ShowAfterHowManyGameovers = serializedObject.FindProperty ("ShowAfterHowManyGameovers");
		TestMode = serializedObject.FindProperty ("TestMode");
#endif

#if USE_UNITYADS
		GameId = serializedObject.FindProperty("GameId");
		AdLimit = serializedObject.FindProperty("AdLimit");
		UnityTestMode = serializedObject.FindProperty("UnityTestMode");
#endif

#if USE_DOTWEEN
        ConsentCanvas = serializedObject.FindProperty("ConsentCanvas");
        MenuCanvas = serializedObject.FindProperty("MenuCanvas");
		CategoryCanvas = serializedObject.FindProperty("CategoryCanvas");
		GameCanvas = serializedObject.FindProperty("GameCanvas");
		ScreenshotCanvas = serializedObject.FindProperty("ScreenshotCanvas");
		PauseCanvas = serializedObject.FindProperty("PauseCanvas");
		GameOverCanvas = serializedObject.FindProperty("GameOverCanvas");

		ProfileButton = serializedObject.FindProperty("ProfileButton");
        LogoutButton = serializedObject.FindProperty("LogoutButton");
        BackButton = serializedObject.FindProperty("BackButton");
		SettingsButton = serializedObject.FindProperty("SettingsButton");
		Homepage = serializedObject.FindProperty("Homepage");
		ProfilePage = serializedObject.FindProperty("ProfilePage");
		SettingsPage = serializedObject.FindProperty("SettingsPage");
        CachePanel = serializedObject.FindProperty("CachePanel");
        UniquecodePanel = serializedObject.FindProperty("UniquecodePanel");
        DuelPanel = serializedObject.FindProperty("DuelPanel");
        CreateArenaPanel = serializedObject.FindProperty("CreateArenaPanel");
        HistoriArenaPanel = serializedObject.FindProperty("HistoriArenaPanel");
        DetailArenaPanel = serializedObject.FindProperty("DetailArenaPanel");
        TopBarText = serializedObject.FindProperty("TopBarText");
		ProfileAvatar = serializedObject.FindProperty("ProfileAvatar");
		Username = serializedObject.FindProperty("Username");
		SoundButton = serializedObject.FindProperty("SoundButton");
		VibrateButton = serializedObject.FindProperty("VibrateButton");
        AdExperienceButton = serializedObject.FindProperty("AdExperienceButton");

        CategoryGroup = serializedObject.FindProperty("CategoryGroup");
		ErrorPanel = serializedObject.FindProperty("ErrorPanel");
		Loading = serializedObject.FindProperty("Loading");

		CurrentCategoryText = serializedObject.FindProperty("CurrentCategoryText");
		QuestionDisplay = serializedObject.FindProperty("QuestionDisplay");
        AltQuestionDisplay = serializedObject.FindProperty("AltQuestionDisplay");
        ImageDispay = serializedObject.FindProperty("ImageDispay");
		ScoreText = serializedObject.FindProperty("ScoreText");
		ImageLoadingAnimation = serializedObject.FindProperty("ImageLoadingAnimation");
		ImageErrorPanel = serializedObject.FindProperty("ImageErrorPanel");
		TimerText = serializedObject.FindProperty("TimerText");
		LivesText = serializedObject.FindProperty("LivesText");
		LongBarAnswers = serializedObject.FindProperty("LongBarAnswers");
		ShortBarAnswers = serializedObject.FindProperty("ShortBarAnswers");
		ContinueButton = serializedObject.FindProperty("ContinueButton");
		QuitPanel = serializedObject.FindProperty("QuitPanel");
		ReviewButton = serializedObject.FindProperty("ReviewButton");
		ReviewPanel = serializedObject.FindProperty("ReviewPanel");
		ScreenshotView = serializedObject.FindProperty("ScreenshotView");
		VideoAdPanel = serializedObject.FindProperty("VideoAdPanel");

		FinalScore = serializedObject.FindProperty("FinalScore");
		CurHighscore = serializedObject.FindProperty("CurHighscore");

		TimerAmount = serializedObject.FindProperty("TimerAmount");
		DownloadTimeout = serializedObject.FindProperty("DownloadTimeout");
		LivesCount = serializedObject.FindProperty("LivesCount");
		Avatars = serializedObject.FindProperty("Avatars");
		CorrectSound = serializedObject.FindProperty("CorrectSound");
		FailSound = serializedObject.FindProperty("FailSound");
		TickingSound = serializedObject.FindProperty("TickingSound");
		CorrectColor = serializedObject.FindProperty("CorrectColor");
		FailColor = serializedObject.FindProperty("FailColor");
		EnablePausing = serializedObject.FindProperty("EnablePausing");
		Questions = serializedObject.FindProperty("Questions");
		AnswersAvailable = serializedObject.FindProperty("AnswersAvailable");

		QuestionDisplaySc = serializedObject.FindProperty("QuestionDisplaySc");
		ImageDispaySc = serializedObject.FindProperty("ImageDispaySc");
		LongBarAnswersSc = serializedObject.FindProperty("LongBarAnswersSc");
		ShortBarAnswersSc = serializedObject.FindProperty("ShortBarAnswersSc");
#endif
	}

	public override void OnInspectorGUI()
	{
	/*	#if UNITY_5_6_OR_NEWER
		serializedObject.UpdateIfRequiredOrScript();
		#else
		serializedObject.UpdateIfDirtyOrScript();
		#endif
        
		GUIStyle myStyle = new GUIStyle("label");
		myStyle.richText = true;
		myStyle.wordWrap = true;

		GUILayout.BeginHorizontal();
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Space(10f);

				Texture2D Logo = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/QMe1.png", typeof(Texture2D)) as Texture2D;
				if (Logo)
					GUILayout.Label(Logo);
			}
			GUILayout.EndVertical();

			GUILayout.BeginVertical();
			{
				string Title = "<size=25>" + "QuizME Template" + "</size>";
				string Demo = "<size=11>" + "Demo One" + "</size>";
				string Copyright = "<size=10.5>" + "Copyright © 2018 Mintonne. All rights reserved." + "</size>";

				GUILayout.Label(Title, myStyle, GUILayout.Height(35f));
				GUILayout.Label(Demo, myStyle);
				GUILayout.Label(Copyright, myStyle);
			}
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
		}
		GUILayout.EndHorizontal();

		EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		if (GUILayout.Button(new GUIContent("Video Tutorials", "We hope you enjoy the music."), GUILayout.Height(30)))
			Application.OpenURL("www.mintonne.com/tutorials");

		if (GUILayout.Button(new GUIContent("Official Forum", "Stay in the loop."), GUILayout.Height(30)))
			Application.OpenURL("https://forum.unity3d.com/threads/quizapp-ultimate-trivia-template.402269/");

		if (GUILayout.Button(new GUIContent("Contact Us", "Please leave a message after the beep."), GUILayout.Height(30)))
		{
			if (EditorUtility.DisplayDialog("Support", "Wohoo. We thought we'd never hear from you.\n\nDrop us an email and we will get back to you asap, unless it is a Friday afternoon or a Monday morning :)\n\n-A friend.", "Send email", "Cancel"))
				Application.OpenURL("mailto:mintonne@gmail.com");
		}

		if (GUILayout.Button(new GUIContent("Rate this asset", "Share your experience with the world."), GUILayout.Height(30)))
			Application.OpenURL("http://u3d.as/oQJ");

		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.Space(10f);*/

#if !USE_DOTWEEN
		GUIStyle newStyle = new GUIStyle(GUI.skin.button);
		newStyle.fontStyle = FontStyle.Bold;
		newStyle.fontSize = 22;

		if (EditorGUIUtility.isProSkin)
			newStyle.normal.textColor = Color.white;
		else
			newStyle.normal.textColor = Color.black;

        GUI.color = Color.green;
		if (GUILayout.Button("Setup QuizApp", newStyle, GUILayout.Height(40)))
            WelcomeTour.ShowWindow();
        GUI.color = Color.white;
#else

		GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"), GUILayout.ExpandWidth(true));

		GUILayout.Label("Dashboard");

		if (GUILayout.Button("Expand All", EditorStyles.toolbarButton, GUILayout.Width(65)))
		{
#if SUPPORTED_PLATFORM
			ShowUnityAds = true;
			ShowAdmob = true;
#endif
			MenuCanvas.isExpanded = true;
			CategoryGroup.isExpanded = true;
			ProfileButton.isExpanded = true;
			QuestionDisplay.isExpanded = true;
			FinalScore.isExpanded = true;
			DownloadTimeout.isExpanded = true;
			QuestionDisplaySc.isExpanded = true;
		}

		if (GUILayout.Button("Collapse All", EditorStyles.toolbarButton, GUILayout.Width(71)))
		{
#if SUPPORTED_PLATFORM
			ShowAdmob = false;
			ShowUnityAds = false;
#endif
			MenuCanvas.isExpanded = false;
			CategoryGroup.isExpanded = false;
			ProfileButton.isExpanded = false;
			QuestionDisplay.isExpanded = false;
			FinalScore.isExpanded = false;
			DownloadTimeout.isExpanded = false;
			QuestionDisplaySc.isExpanded = false;
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(5);

		#if (SUPPORTED_PLATFORM)
		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(8);
		ShowAdmob = EditorGUILayout.Foldout(ShowAdmob, "Google Admob Configuration");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (ShowAdmob)
		{
		#if USE_ADMOB
			EditorGUILayout.HelpBox ("The ad appears when the game ends based on the frequency you set below.", MessageType.Info);

			TestMethod tm = (TestMethod)TestMode.enumValueIndex;

			switch(tm)
			{
				case TestMethod.DisableTestMode:
                    EditorGUILayout.PropertyField(TestMode);

                    GUILayout.Space(10);

                    EditorGUILayout.PropertyField(appId);

					GUILayout.Space(10);

					EditorGUILayout.PropertyField(InterstitialAdUnitId);
					EditorGUILayout.PropertyField(ShowAfterHowManyGameovers);

                    GUILayout.Space(10);

                    EditorGUILayout.HelpBox("You are required to get consent from users in the European Economic Area (EEA).", MessageType.Error);

                    EditorGUILayout.PropertyField(AskForConsent);

                    GUI.color = Color.green;

                    if (GUILayout.Button("More information on Admob Advertising Consent."))
                    {
                        Application.OpenURL("https://developers.google.com/admob/unity/eu-consent");
                    }

                    GUI.color = Color.white;

                    GUILayout.Space(10);
					EditorGUILayout.HelpBox ("Use this once you are ready to publish your application.", MessageType.Error);
					EditorGUILayout.HelpBox ("DO NOT CLICK ON LIVE ADS IN YOUR OWN APP. SERIOUSLY, DON'T DO IT.", MessageType.Error);
					break;

			case TestMethod.EnableTestMode:
                    EditorGUILayout.HelpBox("Uses Google-Provided test ad units. This is the safest method to test ads.", MessageType.Info);
                    EditorGUILayout.PropertyField(TestMode);

                    GUILayout.Space(10);

                    EditorGUILayout.PropertyField(ShowAfterHowManyGameovers);

                    GUILayout.Space(10);

                    EditorGUILayout.HelpBox("You are required to get consent from users in the European Economic Area (EEA).", MessageType.Error);

                    EditorGUILayout.PropertyField(AskForConsent);

                    GUI.color = Color.green;

                    if(GUILayout.Button("More information on Admob Advertising Consent."))
                    {
                        Application.OpenURL("https://developers.google.com/admob/unity/eu-consent");
                    }

                    GUI.color = Color.white;

					break;
			}

			GUILayout.Space(20);

			GUI.color = Color.red;
			if (GUILayout.Button ("Disable Admob Ads", GUILayout.Height (20)))
			{
				var option = EditorUtility.DisplayDialog("Disable Admob Ads","Do you want to disable Admob Ads?","Yes","No");
				if(option)
				{
					#if UNITY_ANDROID
					BuildTargetGroup TargetPlatform = BuildTargetGroup.Android;
					#elif UNITY_IOS
					BuildTargetGroup TargetPlatform = BuildTargetGroup.iOS;
					#endif

					var DefineTarget = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetPlatform);

					if (DefineTarget.Contains ("USE_ADMOB"))
					{
						string[] curDefineSymbols = DefineTarget.Split (defineSeperators, StringSplitOptions.RemoveEmptyEntries);
						List<string>	newDefineSymbols = new List<string> (curDefineSymbols);
						newDefineSymbols.Remove ("USE_ADMOB");
						PlayerSettings.SetScriptingDefineSymbolsForGroup (TargetPlatform, string.Join (";", newDefineSymbols.ToArray ()));
					}
				}
			}
			GUI.color = Color.white;
			#elif (!USE_ADMOB && SUPPORTED_PLATFORM)
			GUI.color = Color.green;
			if (GUILayout.Button("Enable Admob Ads", GUILayout.Height(20)))
			{
				var option = EditorUtility.DisplayDialogComplex("Enable Admob Ads", "Have you imported the Admob Unity Plugin?", "Yes", "No", "Cancel");
				switch (option)
				{
					case 0:
						#if UNITY_ANDROID
                        BuildTargetGroup TargetPlatform = BuildTargetGroup.Android;
						#elif UNITY_IOS
						BuildTargetGroup TargetPlatform = BuildTargetGroup.iOS;
						#endif

						var Defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetPlatform);
						if (Defines.Contains("USE_ADMOB"))
							return;
						else
						{
							string[] curDefineSymbols = Defines.Split(defineSeperators, StringSplitOptions.RemoveEmptyEntries);
							List<string> newDefineSymbols = new List<string>(curDefineSymbols);
							newDefineSymbols.Add("USE_ADMOB");
							PlayerSettings.SetScriptingDefineSymbolsForGroup(TargetPlatform, string.Join(";", newDefineSymbols.ToArray()));
						}
						break;
					case 1:
						Application.OpenURL("https://github.com/googleads/googleads-mobile-unity/releases");
						break;
				}
			}

			GUI.color = Color.white;
			#endif
		}

		GUILayout.EndVertical();

		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(8);
		ShowUnityAds = EditorGUILayout.Foldout(ShowUnityAds, "Unity Ads Configuration");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (ShowUnityAds)
		{
		#if (USE_UNITYADS && SUPPORTED_PLATFORM)
			EditorGUILayout.HelpBox ("A dialog appears when the player's lives are over. The player earns an extra life if they watch the video ad without skipping or cancelling it.", MessageType.Info);
			EditorGUILayout.PropertyField(GameId);
			EditorGUILayout.PropertyField(AdLimit);

			GUILayout.Space(10);

			EditorGUILayout.PropertyField(UnityTestMode);

			GUILayout.Space(20);

			GUI.color = Color.red;
			if (GUILayout.Button ("Disable Unity Ads", GUILayout.Height (20)))
			{
				var option = EditorUtility.DisplayDialog("Disable Unity Ads","Do you want to disable Unity Ads?","Yes","No");
				if(option)
				{

		#if UNITY_ANDROID
		BuildTargetGroup TargetPlatform = BuildTargetGroup.Android;
		#elif UNITY_IOS
		BuildTargetGroup TargetPlatform = BuildTargetGroup.iOS;
		#endif

		var DefineTarget = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetPlatform);

					if (DefineTarget.Contains ("USE_UNITYADS"))
		{
		string[] curDefineSymbols = DefineTarget.Split (defineSeperators, StringSplitOptions.RemoveEmptyEntries);
		List<string>	newDefineSymbols = new List<string> (curDefineSymbols);
						newDefineSymbols.Remove ("USE_UNITYADS");
		PlayerSettings.SetScriptingDefineSymbolsForGroup (TargetPlatform, string.Join (";", newDefineSymbols.ToArray ()));
					}
				}
		}
		GUI.color = Color.white;
		#elif (!USE_UNITYADS && SUPPORTED_PLATFORM)
			GUI.color = Color.green;
			if (GUILayout.Button("Enable Unity Ads", GUILayout.Height(20)))
			{
				var option = EditorUtility.DisplayDialogComplex("Enable Unity Ads", "Have you imported the Unity Ads package?", "Yes", "No", "Cancel");
				switch (option)
				{
					case 0:
		#if UNITY_ANDROID
						BuildTargetGroup TargetPlatform = BuildTargetGroup.Android;
		#elif UNITY_IOS
		BuildTargetGroup TargetPlatform = BuildTargetGroup.iOS;
		#endif

						var Defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetPlatform);
						if (Defines.Contains("USE_UNITYADS"))
							return;
						else
						{
							string[] curDefineSymbols = Defines.Split(defineSeperators, StringSplitOptions.RemoveEmptyEntries);
							List<string> newDefineSymbols = new List<string>(curDefineSymbols);
							newDefineSymbols.Add("USE_UNITYADS");
							PlayerSettings.SetScriptingDefineSymbolsForGroup(TargetPlatform, string.Join(";", newDefineSymbols.ToArray()));
						}
						break;
					case 1:
						UnityEditorInternal.AssetStore.Open("content/66123");
						break;
				}
			}

			GUI.color = Color.white;
		#endif
		}
		GUILayout.EndVertical();
		#endif

		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(5);
		MenuCanvas.isExpanded = EditorGUILayout.Foldout(MenuCanvas.isExpanded, "Game Canvases");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (MenuCanvas.isExpanded)
		{
            EditorGUILayout.PropertyField(ConsentCanvas);
            EditorGUILayout.PropertyField(MenuCanvas);
			EditorGUILayout.PropertyField(CategoryCanvas);
			EditorGUILayout.PropertyField(GameCanvas);
			EditorGUILayout.PropertyField(ScreenshotCanvas);
			EditorGUILayout.PropertyField(PauseCanvas);
			EditorGUILayout.PropertyField(GameOverCanvas);
		}

		GUILayout.EndVertical();

		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(5);
		ProfileButton.isExpanded = EditorGUILayout.Foldout(ProfileButton.isExpanded, "Menu UI");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (ProfileButton.isExpanded)
		{
			EditorGUILayout.PropertyField(ProfileButton);
            EditorGUILayout.PropertyField(LogoutButton);
            EditorGUILayout.PropertyField(BackButton);
			EditorGUILayout.PropertyField(SettingsButton);
			EditorGUILayout.PropertyField(Homepage);
			EditorGUILayout.PropertyField(ProfilePage);
			EditorGUILayout.PropertyField(SettingsPage);
            EditorGUILayout.PropertyField(CachePanel);
            EditorGUILayout.PropertyField(UniquecodePanel);
            EditorGUILayout.PropertyField(DuelPanel);
            EditorGUILayout.PropertyField(CreateArenaPanel);
            EditorGUILayout.PropertyField(HistoriArenaPanel);
            EditorGUILayout.PropertyField(DetailArenaPanel);
            EditorGUILayout.PropertyField(TopBarText);
			EditorGUILayout.PropertyField(ProfileAvatar);
			EditorGUILayout.PropertyField(Username);
			EditorGUILayout.PropertyField(SoundButton);
			EditorGUILayout.PropertyField(VibrateButton);
            EditorGUILayout.PropertyField(AdExperienceButton);
        }

        GUILayout.EndVertical();

		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(5);
		CategoryGroup.isExpanded = EditorGUILayout.Foldout(CategoryGroup.isExpanded, "Category UI");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (CategoryGroup.isExpanded)
		{
			EditorGUILayout.PropertyField(CategoryGroup);
			EditorGUILayout.PropertyField(ErrorPanel);
			EditorGUILayout.PropertyField(Loading);
		}

		GUILayout.EndVertical();

		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(5);
		QuestionDisplay.isExpanded = EditorGUILayout.Foldout(QuestionDisplay.isExpanded, "Game UI");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (QuestionDisplay.isExpanded)
		{
			EditorGUILayout.PropertyField(CurrentCategoryText);
			EditorGUILayout.PropertyField(QuestionDisplay);
            EditorGUILayout.PropertyField(AltQuestionDisplay);
            EditorGUILayout.PropertyField(ImageDispay);
			EditorGUILayout.PropertyField(ScoreText);
			EditorGUILayout.PropertyField(ImageLoadingAnimation);
			EditorGUILayout.PropertyField(ImageErrorPanel);
			EditorGUILayout.PropertyField(TimerText);
			EditorGUILayout.PropertyField(LivesText);
			EditorGUILayout.PropertyField(LongBarAnswers);
			EditorGUILayout.PropertyField(ShortBarAnswers);
			EditorGUILayout.PropertyField(ContinueButton);
			EditorGUILayout.PropertyField(QuitPanel);
			EditorGUILayout.PropertyField(ReviewButton);
			EditorGUILayout.PropertyField(ReviewPanel);
			EditorGUILayout.PropertyField(ScreenshotView);
			EditorGUILayout.PropertyField(VideoAdPanel);
		}

		GUILayout.EndVertical();

		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(5);
		QuestionDisplaySc.isExpanded = EditorGUILayout.Foldout(QuestionDisplaySc.isExpanded, "Screenshot UI");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (QuestionDisplaySc.isExpanded)
		{
			EditorGUILayout.PropertyField(QuestionDisplaySc);
			EditorGUILayout.PropertyField(ImageDispaySc);
			EditorGUILayout.PropertyField(LongBarAnswersSc);
			EditorGUILayout.PropertyField(ShortBarAnswersSc);
		}
		GUILayout.EndVertical();

		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(5);
		FinalScore.isExpanded = EditorGUILayout.Foldout(FinalScore.isExpanded, "GameOver UI");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (FinalScore.isExpanded)
		{
			EditorGUILayout.PropertyField(FinalScore);
			EditorGUILayout.PropertyField(CurHighscore);
		}
		GUILayout.EndVertical();


		GUILayout.BeginVertical(m_Background);
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		GUILayout.Space(5);
		DownloadTimeout.isExpanded = EditorGUILayout.Foldout(DownloadTimeout.isExpanded, "Game Variables");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (DownloadTimeout.isExpanded)
		{
			EditorGUILayout.PropertyField(DownloadTimeout);
			EditorGUILayout.PropertyField(TimerAmount);
			EditorGUILayout.PropertyField(LivesCount);
			EditorGUILayout.PropertyField(CorrectSound);
			EditorGUILayout.PropertyField(FailSound);
			EditorGUILayout.PropertyField(TickingSound);
			EditorGUILayout.PropertyField(CorrectColor);
			EditorGUILayout.PropertyField(FailColor);
			EditorGUILayout.PropertyField(EnablePausing);
			EditorGUILayout.PropertyField(Avatars, true);
			EditorGUILayout.PropertyField(Questions, true);
			EditorGUILayout.PropertyField(AnswersAvailable, true);
		}

		GUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();
#endif
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

	private void OnInspectorUpdate()
	{
		this.Repaint();
	}
}