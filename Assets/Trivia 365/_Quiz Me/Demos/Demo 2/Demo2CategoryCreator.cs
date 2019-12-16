#if !USE_DOTWEEN
#pragma warning disable 0414
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using System.Collections.Generic;
using System;
using System.IO;
using System.Collections;
using System.Threading;
using UnityEngine.Networking;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class Demo2CategoryCreator : MonoBehaviour
{
#if UNITY_EDITOR
    public List<Demo2CategoryFormatClass> CategoryList = new List<Demo2CategoryFormatClass>();
    private Transform ParentObj;
    private Transform MasterPref;
    private Transform ProfilePageCategories;
    private CategoryCanvasSetter ProfileCanvasSetter;
    private RectTransform ProfilePagePrefab;
    private Demo2XmlDownloader[] CurCategories;
    private List<Transform> CurProfileCategories = new List<Transform>();
    private Demo2Controller Controller;
    public Font ProfileCategoryNameFont;
    public FontStyle ProfileCategoryFontStyle = FontStyle.Normal;
    public int ProfileCategoryNameFontSize = 35;
    public Font ProfilePerformanceFont;
    public FontStyle ProfilePerformanceFontStyle = FontStyle.Normal;
    public int ProfilePerformanceNameFontSize = 35;
    public int ProfileCategoryImageSize = 140;

    QuizAppContainer Qac;

#if USE_DOTWEEN
    public void OnEnable()
    {
        if (!Controller)
            Controller = (Demo2Controller)FindObjectOfType(typeof(Demo2Controller));
    }

    public void UpdateCategories()
    {
        if (!Controller)
            Controller = (Demo2Controller)FindObjectOfType(typeof(Demo2Controller));
        if (!ProfilePageCategories)
            ProfilePageCategories = Controller.ProfilePage.gameObject.GetComponent<ScrollRect>().content;
        if (!ProfileCanvasSetter && ProfilePageCategories)
            ProfileCanvasSetter = ProfilePageCategories.gameObject.GetComponent<CategoryCanvasSetter>();
        if (!MasterPref)
            MasterPref = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/Editor Prefabs/Category Prefab Dm2.prefab", typeof(Transform)) as Transform;
        if (!ProfilePagePrefab)
            ProfilePagePrefab = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/Editor Prefabs/Profile Page Prefab.prefab", typeof(RectTransform)) as RectTransform;

        int xCount = FindObjectsOfType(typeof(ParentGameObject)).Length;

        ParentGameObject[] PreviousParent = new ParentGameObject[xCount];

        PreviousParent = FindObjectsOfType(typeof(ParentGameObject)) as ParentGameObject[];

        if (PreviousParent.Length > 0)
            foreach (ParentGameObject T in PreviousParent)
                DestroyImmediate(T.gameObject);

        CurProfileCategories.Clear();

        for (int x = 0; x < ProfilePageCategories.childCount; x++)
            CurProfileCategories.Add(ProfilePageCategories.GetChild(x));

        foreach (Transform C in CurProfileCategories)
            DestroyImmediate(C.gameObject);

        int Count = FindObjectsOfType(typeof(Demo2XmlDownloader)).Length;

        CurCategories = new Demo2XmlDownloader[Count];

        CurCategories = FindObjectsOfType(typeof(Demo2XmlDownloader)) as Demo2XmlDownloader[];

        if (CurCategories.Length > 0)
        {
            foreach (Demo2XmlDownloader T in CurCategories)
                DestroyImmediate(T.gameObject);
        }

        GameObject NewParentObj = new GameObject("Game Categories");
        NewParentObj.AddComponent(typeof(ParentGameObject));

        for (int a = 0; a < CategoryList.Count; a++)
        {
            Transform mainPrefab = (Transform)Instantiate(MasterPref, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            mainPrefab.SetParent(NewParentObj.transform);
            mainPrefab.name = CategoryList[a].CategoryName;

            RectTransform mainprofilePrefab = (RectTransform)Instantiate(ProfilePagePrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            mainprofilePrefab.SetParent(ProfilePageCategories.transform);
            mainprofilePrefab.localScale = new Vector3(1, 1, 1);
            mainprofilePrefab.name = CategoryList[a].CategoryName;
            Image ProfileCategoryImage = mainprofilePrefab.GetChild(0).gameObject.GetComponent<Image>();
            ProfileCategoryImage.sprite = CategoryList[a].CategoryImage;
            ProfileCategoryImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)ProfileCategoryImageSize, (float)ProfileCategoryImageSize);

            Text ProfCatName = mainprofilePrefab.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            ProfCatName.text = CategoryList[a].CategoryName.ToString();
            if (ProfileCategoryNameFont)
                ProfCatName.font = ProfileCategoryNameFont;
            else
                ProfCatName.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            ProfCatName.fontSize = ProfileCategoryNameFontSize;
            ProfCatName.fontStyle = ProfileCategoryFontStyle;

            Text PerformanceCatText = mainprofilePrefab.GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
            if (ProfilePerformanceFont)
                PerformanceCatText.font = ProfilePerformanceFont;
            else
                PerformanceCatText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            PerformanceCatText.fontSize = ProfilePerformanceNameFontSize;
            PerformanceCatText.fontStyle = ProfilePerformanceFontStyle;

            Demo2XmlDownloader XMLDownloader = mainPrefab.gameObject.GetComponent<Demo2XmlDownloader>();
            XMLDownloader.Name = CategoryList[a].CategoryName;
            XMLDownloader.Mode = CategoryList[a].Mode;
            XMLDownloader.OnlinePath = CategoryList[a].OnlinePath;
            XMLDownloader.OfflinePath = CategoryList[a].OfflinePath;
            XMLDownloader.QuestionPref = CategoryList[a].QuestionPref;
            XMLDownloader.AnswerPref = CategoryList[a].AnswerPref;
            XMLDownloader.PerformancePref = CategoryList[a].PerformancePref;
            XMLDownloader.CategoryImage = CategoryList[a].CategoryImage;
            XMLDownloader.PerformanceText = PerformanceCatText;
        }
        ProfileCanvasSetter.AlignComponents();

        GUIUtility.keyboardControl = 0;
    }

    public void LoadCategories()
    {
        int Count = FindObjectsOfType(typeof(Demo2XmlDownloader)).Length;

        CurCategories = new Demo2XmlDownloader[Count];

        CurCategories = FindObjectsOfType(typeof(Demo2XmlDownloader)) as Demo2XmlDownloader[];

        if (CurCategories.Length < 1)
        {
            EditorUtility.DisplayDialog("Load Categories", "No categories found", "Ok");
            return;
        }

        System.Array.Reverse(CurCategories);

        for (int a = 0; a < CurCategories.Length; a++)
        {
            CategoryList.Add(new Demo2CategoryFormatClass());
            Demo2XmlDownloader ExistingCategory = CurCategories[a];
            CategoryList[a].CategoryName = ExistingCategory.Name;
            CategoryList[a].CategoryImage = ExistingCategory.CategoryImage;
            CategoryList[a].Mode = ExistingCategory.Mode;
            CategoryList[a].OnlinePath = ExistingCategory.OnlinePath;
            CategoryList[a].OfflinePath = ExistingCategory.OfflinePath;
            CategoryList[a].QuestionPref = ExistingCategory.QuestionPref;
            CategoryList[a].AnswerPref = ExistingCategory.AnswerPref;
            CategoryList[a].PerformancePref = ExistingCategory.PerformancePref;
            CategoryList[a].ShuffleQuestions = ExistingCategory.ShuffleQuestions;
        }
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }

    //Load the question from the xml to the questions array
    public void StartOfflineXMLTest(string OfflinePath)
    {
        StopAllCoroutines();

        TextAsset _xml = Resources.Load<TextAsset>(OfflinePath);

        if (_xml == null)
        {
            EditorUtility.DisplayDialog("XML file not found", "XML file does not exist.\n\nMake sure the offline path matches the XML filename in the Resources/XML folder", "Ok");
            return;
        }


        try
        {
            //load the questions from the XML
            Qac = QuizAppContainer.Load(OfflinePath);
        }
        catch (Exception e)
        {
            EditorUtility.DisplayDialog("XML Error", "The XML parsing failed with error " + e, "Ok");

            throw;
        }

        EditorUtility.DisplayDialog("Success", "XML loaded and parsed successfully.\n" + Qac.Quizes.Count + " questions found.", "Ok");
    }

    public void StartOnlineXMLTest(string OnlineXML)
    {
        StopAllCoroutines();
        StartCoroutine(TestOnlineXML(OnlineXML));
    }

    //Load the question from the xml we download
    private IEnumerator TestOnlineXML(string Link)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(Link))
        {
            uwr.timeout = 30;

            yield return uwr.SendWebRequest();

            if (uwr.isHttpError || uwr.isNetworkError)
            {
                EditorUtility.DisplayDialog("Error", "Download failed.", "OK");
                Debug.LogError(uwr.error);
            }
            else
            {
                try
                {
                    StringReader reader = new StringReader(uwr.downloadHandler.text);
                    Qac = QuizAppContainer.DownloadedXML(reader);
                }
                catch (Exception e)
                {
                    EditorUtility.DisplayDialog("XML Error", "The XML parsing failed with error " + e, "Ok");

                    throw;
                }

                EditorUtility.DisplayDialog("Success", "XML downloaded and parsed successfully.\n" + Qac.Quizes.Count + " questions found.", "Ok");
            }
        }
    }

    public void ShowHomePage()
    {
        Controller.MenuCanvas.SetActive(true);
        Controller.CategoryCanvas.SetActive(false);
        Controller.GameCanvas.SetActive(false);
        Controller.GameOverCanvas.SetActive(false);
        Controller.PauseCanvas.SetActive(false);
        Controller.ScreenshotCanvas.SetActive(false);

        Controller.ProfilePage.SetActive(false);
        Controller.Homepage.SetActive(true);
        Controller.SettingsPage.SetActive(false);
    }

    public void ShowProfilePage()
    {
        Controller.MenuCanvas.SetActive(true);
        Controller.CategoryCanvas.SetActive(false);
        Controller.GameCanvas.SetActive(false);
        Controller.GameOverCanvas.SetActive(false);
        Controller.PauseCanvas.SetActive(false);
        Controller.ScreenshotCanvas.SetActive(false);

        Controller.ProfilePage.SetActive(true);
        Controller.Homepage.SetActive(false);
        Controller.SettingsPage.SetActive(false);
    }

    [ContextMenu("DELETE ALL CATEGORIES")]
    public void DeleteAllCat()
    {
        EditorApplication.Beep();
        if (EditorUtility.DisplayDialog("Delete ALL categories", "Are you sure you want to delete all categories?", "Yes", "No"))
        {
            GUIUtility.keyboardControl = 0;
            CategoryList.Clear();
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
#endif
#endif
}