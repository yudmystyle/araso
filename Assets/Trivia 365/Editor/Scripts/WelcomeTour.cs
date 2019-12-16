using UnityEngine;
using UnityEditor.SceneManagement;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(AdmobManager))]
public class EditorAdmobManager : Editor
{
    public override void OnInspectorGUI()
    {
    }
}

[CustomEditor(typeof(CamScreenshot))]
public class EditorCamScreenshot : Editor
{
    public override void OnInspectorGUI()
    {
    }
}

[CustomEditor(typeof(CategoryCanvasSetter))]
public class EditorCanvasSetter : Editor
{
    public override void OnInspectorGUI()
    {
    }
}


public class WelcomeTour : EditorWindow
{
    private const string DotweenKey = "USE_DOTWEEN";

    private const string AdmobKey = "USE_ADMOB";

    private const string UnityAdsKey = "USE_UNITYADS";

    private static bool DotweenDirAdded = false;

    private static readonly char[] defineSeperators = new char[] { ';', ',', ' ' };

    // GUI textures
    private Texture2D m_slideOne;
    private Texture2D m_slideTwo;
    private Texture2D m_slideThree;
    private Texture2D m_slideFour;
    private Texture2D m_slideFive;
    private Texture2D m_slideSix;

    private Texture2D m_DOTweenFree;
    private Texture2D m_DOTweenPro;
    private Texture2D m_doneSprite;

    private Texture2D m_forwardArrow;
    private Texture2D m_backArrow;

    private Texture2D m_completeSetup;
    private Texture2D m_closeWindow;
    private Texture2D m_vidTutorials;


    // Related to window
    protected Vector2 m_windowSize = new Vector2(800, 535);

    //ints
    private int m_currentSlide = 1;

    private Color m_originalColor;

    private string dotweenPath;

    [MenuItem("Tools/QuizApp Panel", false, -25)]
    internal static void ShowWindow()
    {
        WelcomeTour _window = EditorWindow.GetWindow<WelcomeTour>(true, "Welcome");

        // Show window
        _window.Show();
    }

    [MenuItem("Tools/Video Tutorials", false, -24)]
    internal static void Tutorials()
    {
        Application.OpenURL("www.mintonne.com/tutorials");
    }

    [MenuItem("Window/QuizApp Helpers/Disable Integrations/Admob Ads", false)]
    internal static void disableAdmob()
    {
        RemoveDefine(BuildTargetGroup.Android, AdmobKey);
        RemoveDefine(BuildTargetGroup.iOS, AdmobKey);
    }

    [MenuItem("Window/QuizApp Helpers/Disable Integrations/DOTween", false)]
    internal static void disableDotween()
    {
        RemoveScriptingDefineSymbols();
    }

    [MenuItem("Window/QuizApp Helpers/Disable Integrations/Unity Ads", false)]
    internal static void disableUnityAds()
    {
        RemoveDefine(BuildTargetGroup.Android, UnityAdsKey);
        RemoveDefine(BuildTargetGroup.iOS, UnityAdsKey);
    }

    [MenuItem("Window/QuizApp Helpers/Open Scenes/QuizMe Demo 1", false)]
    internal static void OpenQme1()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Trivia 365/_Quiz Me/Demos/Demo 1/Demo1.unity");
    }

    [MenuItem("Window/QuizApp Helpers/Open Scenes/QuizMe Demo 2", false)]
    internal static void OpenQme2()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Trivia 365/_Quiz Me/Demos/Demo 2/Demo2.unity");
    }

    [MenuItem("Window/QuizApp Helpers/Open Scenes/True or False", false)]
    internal static void OpenToF()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Trivia 365/_ToF/Demo/Demo.unity");
    }

    [MenuItem("Window/QuizApp Helpers/Open Scenes/Trivia Kingdom", false)]
    internal static void OpenTk()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Trivia 365/_Trivia Kingdom/Demo/Demo.unity");
    }

    [MenuItem("Window/QuizApp Helpers/Switch Platform/Android", false)]
    internal static void SwitchAndroid()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
    }

    [MenuItem("Window/QuizApp Helpers/Switch Platform/iOS", false)]
    internal static void SwitchIos()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
    }

    [MenuItem("Window/QuizApp Helpers/Quick Fixes/Activate DOTween", false)]
    internal static void EnableDotween()
    {
        SetDefine(BuildTargetGroup.Android);
        SetDefine(BuildTargetGroup.iOS);
        SetDefine(BuildTargetGroup.WSA);
        SetDefine(BuildTargetGroup.Standalone);
        SetDefine(BuildTargetGroup.WebGL);
    }

    private void OnEnable()
    {
        dotweenPath = Path.Combine("Assets", "Demigiant");

        EditorApplication.update += CheckDir;

        m_currentSlide = EditorPrefs.GetInt("Slide", 1);
        //Load sprites
        m_slideOne = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Slide 1.png", typeof(Texture2D)) as Texture2D;
        m_slideTwo = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Slide 2.png", typeof(Texture2D)) as Texture2D;
        m_slideThree = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Slide 3.png", typeof(Texture2D)) as Texture2D;
        m_slideFour = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Slide 4.png", typeof(Texture2D)) as Texture2D;
        m_slideFive = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Slide 5.png", typeof(Texture2D)) as Texture2D;
        m_slideSix = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Slide 6.png", typeof(Texture2D)) as Texture2D;

        m_DOTweenFree = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/DOT.png", typeof(Texture2D)) as Texture2D;
        m_DOTweenPro = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/DOT Pro.png", typeof(Texture2D)) as Texture2D;
        m_doneSprite = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Completed.png", typeof(Texture2D)) as Texture2D;

        m_backArrow = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Back.png", typeof(Texture2D)) as Texture2D;
        m_forwardArrow = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Forward.png", typeof(Texture2D)) as Texture2D;

        m_completeSetup = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Complete Setup.png", typeof(Texture2D)) as Texture2D;
        m_closeWindow = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Close Window.png", typeof(Texture2D)) as Texture2D;
        m_vidTutorials = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Tutorials.png", typeof(Texture2D)) as Texture2D;
    }

    public void CheckDir()
    {
        if (Directory.Exists(dotweenPath))
        {
            DotweenDirAdded = true;
            EditorApplication.update -= CheckDir;
        }
        else
        {
            DotweenDirAdded = false;
            RemoveScriptingDefineSymbols();
        }
    }

    private void SetScriptingDefineSymbols()
    {
        SetDefine(BuildTargetGroup.Android);
        SetDefine(BuildTargetGroup.iOS);
        SetDefine(BuildTargetGroup.WSA);
        SetDefine(BuildTargetGroup.Standalone);
        SetDefine(BuildTargetGroup.WebGL);
    }

    static void SetDefine(BuildTargetGroup Target)
    {
        var curDefineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(Target);

        if (!curDefineSymbols.Contains(DotweenKey))
        {
            string[] DefineSymbols = curDefineSymbols.Split(defineSeperators, StringSplitOptions.RemoveEmptyEntries);
            List<string> newDefineSymbols = new List<string>(DefineSymbols);
            newDefineSymbols.Add(DotweenKey);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(Target, string.Join(";", newDefineSymbols.ToArray()));
        }
    }

    static void RemoveScriptingDefineSymbols()
    {
        RemoveDefine(BuildTargetGroup.Android, DotweenKey);
        RemoveDefine(BuildTargetGroup.iOS, DotweenKey);
        RemoveDefine(BuildTargetGroup.WSA, DotweenKey);
        RemoveDefine(BuildTargetGroup.Standalone, DotweenKey);
        RemoveDefine(BuildTargetGroup.WebGL, DotweenKey);
    }

    static void RemoveDefine(BuildTargetGroup Target, string define)
    {
        var curDefineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(Target);

        if (curDefineSymbols.Contains(define))
        {
            string[] DefineSymbols = curDefineSymbols.Split(defineSeperators, StringSplitOptions.RemoveEmptyEntries);
            List<string> newDefineSymbols = new List<string>(DefineSymbols);
            newDefineSymbols.Remove(define);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(Target, string.Join(";", newDefineSymbols.ToArray()));
        }
    }

    private void OnGUI()
    {
        minSize = m_windowSize;
        maxSize = minSize;

        m_originalColor = GUI.backgroundColor;

        // Set background
        SetSlide();

        GUI.backgroundColor = new Color(1, 1, 1, 0);

        DrawNavigationButtons();

        GUI.backgroundColor = m_originalColor;
    }

    private void SetSlide()
    {
        // Set background
        if (m_slideOne != null && m_currentSlide == 1)
            GUI.Label(new Rect(0f, 0f, 800f, 535f), m_slideOne);
        else if (m_slideTwo != null && m_currentSlide == 2)
            GUI.Label(new Rect(0f, 0f, 800f, 535f), m_slideTwo);
        else if (m_slideThree != null && m_currentSlide == 3)
        {
            GUI.Label(new Rect(0f, 0f, 800f, 535f), m_slideThree);

            GUI.backgroundColor = new Color(1, 1, 1, 0);

            if (!DotweenDirAdded)
            {
                if (GUI.Button(new Rect(115, 300, 256, 55), m_DOTweenFree))
                    UnityEditorInternal.AssetStore.Open("content/27676");
                //Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/27676");
                if (GUI.Button(new Rect(415, 300, 256, 55), m_DOTweenPro))
                    UnityEditorInternal.AssetStore.Open("content/32416");
                //Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/32416");
            }
            else
            {
                GUI.Label(new Rect(260, 300, 256, 77), m_doneSprite);
            }

            GUI.backgroundColor = m_originalColor;
        }
        else if (m_slideFour != null && m_currentSlide == 4)
        {
            GUI.Label(new Rect(0f, 0f, 800f, 535f), m_slideFour);
        }
        else if (m_slideFive != null && m_currentSlide == 5)
        {
            GUI.Label(new Rect(0f, 0f, 800f, 535f), m_slideFive);

            GUI.backgroundColor = new Color(1, 1, 1, 0);

            if (GUI.Button(new Rect(270, 280, 256, 55), m_completeSetup))
            {
                if (!DotweenDirAdded)
                {
                    if (EditorUtility.DisplayDialog("Directory Not Found", "We couldn't find the DOTween directory.", "Ok"))
                    {
                        m_currentSlide = 3;
                        EditorPrefs.SetInt("Slide", m_currentSlide);
                        return;
                    }
                }

                SetScriptingDefineSymbols();

                m_currentSlide++;

                EditorPrefs.SetInt("Slide", m_currentSlide);
            }
        }
        else if (m_slideSix != null && m_currentSlide == 6)
        {
            GUI.Label(new Rect(0f, 0f, 800f, 535f), m_slideSix);

            GUI.backgroundColor = new Color(1, 1, 1, 0);

            if (GUI.Button(new Rect(270, 220, 256, 55), m_vidTutorials))
                Application.OpenURL("www.mintonne.com/tutorials");

            if (GUI.Button(new Rect(270, 300, 256, 55), m_closeWindow))
                EditorWindow.GetWindow<WelcomeTour>().Close();

            GUI.backgroundColor = m_originalColor;
        }
    }

    private void DrawNavigationButtons()
    {
        if (m_currentSlide > 1 && m_currentSlide <= 5)
        {
            if (GUI.Button(new Rect(50, 450, 55, 55), m_backArrow))
            {
                m_currentSlide--;
                EditorPrefs.SetInt("Slide", m_currentSlide);
            }
        }

        if (m_currentSlide < 5)
        {
            if (GUI.Button(new Rect(700, 450, 55, 55), m_forwardArrow))
            {
                m_currentSlide++;
                EditorPrefs.SetInt("Slide", m_currentSlide);
            }
        }
    }

    private void OnDestroy()
    {
        EditorPrefs.DeleteKey("Slide");
    }
}