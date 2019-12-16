#if !USE_DOTWEEN
#pragma warning disable 0414
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;
using System.Threading;
using UnityEngine.Networking;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class Demo1CategoryCreator : MonoBehaviour
{
#if UNITY_EDITOR
    public Font CategoryFont;
    public FontStyle CategoryFontStyle = FontStyle.Normal;
    public int CategoryNameFontSize = 35;
    public int CategoryImageSize = 140;
    public Font ProfileCategoryNameFont;
    public FontStyle ProfileCategoryFontStyle = FontStyle.Normal;
    public int ProfileCategoryNameFontSize = 35;
    public Font ProfilePerformanceFont;
    public FontStyle ProfilePerformanceFontStyle = FontStyle.Normal;
    public int ProfilePerformanceNameFontSize = 35;
    public int ProfileCategoryImageSize = 140;
    private List<Transform> CurCategories = new List<Transform>();
    private List<Transform> CurProfileCategories = new List<Transform>();
    private Demo1Controller Controller;
    private Transform GameCategories;
    private Transform ProfilePageCategories;
    private CategoryCanvasSetter CanvasSetter;
    private CategoryCanvasSetter ProfileCanvasSetter;
    private RectTransform CategoryCanvasPrefab;
    private RectTransform ProfilePagePrefab;
    public List<Demo1CategoryFormatClass> CategoryList = new List<Demo1CategoryFormatClass>();

    private QuizAppContainer Qac;

#if USE_DOTWEEN
    public void OnEnable()
    {
        if (!Controller)
            Controller = (Demo1Controller)FindObjectOfType(typeof(Demo1Controller));
        if (!GameCategories)
            GameCategories = Controller.CategoryGroup.gameObject.GetComponent<ScrollRect>().content;
        if (!ProfilePageCategories)
            ProfilePageCategories = Controller.ProfilePage.gameObject.GetComponent<ScrollRect>().content;
        if (!CanvasSetter && GameCategories)
            CanvasSetter = GameCategories.gameObject.GetComponent<CategoryCanvasSetter>();
        if (!ProfileCanvasSetter && ProfilePageCategories)
            ProfileCanvasSetter = ProfilePageCategories.gameObject.GetComponent<CategoryCanvasSetter>();
        if (!CategoryCanvasPrefab)
            CategoryCanvasPrefab = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/Editor Prefabs/Category Prefab.prefab", typeof(RectTransform)) as RectTransform;
        if (!ProfilePagePrefab)
            ProfilePagePrefab = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/Editor Prefabs/Profile Page Prefab.prefab", typeof(RectTransform)) as RectTransform;
    }

    public void UpdateCategories()
    {
        CurCategories.Clear();
        CurProfileCategories.Clear();

        for (int a = 0; a < GameCategories.childCount; a++)
            CurCategories.Add(GameCategories.GetChild(a));

        for (int x = 0; x < ProfilePageCategories.childCount; x++)
            CurProfileCategories.Add(ProfilePageCategories.GetChild(x));

        foreach (Transform Category in CurCategories)
        {
#if UNITY_2018_3_OR_NEWER
            if (PrefabUtility.IsPartOfPrefabInstance(Category))
                PrefabUtility.UnpackPrefabInstance(PrefabUtility.GetNearestPrefabInstanceRoot(Category.gameObject), PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
#endif
            DestroyImmediate(Category.gameObject);
        }

        foreach (Transform Category in CurProfileCategories)
        {
#if UNITY_2018_3_OR_NEWER
            if (PrefabUtility.IsPartOfPrefabInstance(Category))
                PrefabUtility.UnpackPrefabInstance(PrefabUtility.GetNearestPrefabInstanceRoot(Category.gameObject), PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
#endif
            DestroyImmediate(Category.gameObject);
        }

        Demo1XmlDownloader XMLDownloader;

        for (int a = 0; a < CategoryList.Count; a++)
        {
            RectTransform mainPrefab = (RectTransform)Instantiate(CategoryCanvasPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            mainPrefab.SetParent(GameCategories.transform);
            mainPrefab.localScale = new Vector3(1, 1, 1);
            mainPrefab.name = CategoryList[a].CategoryName;
            Image CatCanvasImage = mainPrefab.GetChild(0).gameObject.GetComponent<Image>();
            CatCanvasImage.sprite = CategoryList[a].CategoryImage;
            CatCanvasImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)CategoryImageSize, (float)CategoryImageSize);
            Text CatText = mainPrefab.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            CatText.text = CategoryList[a].CategoryName.ToString();
            if (CategoryFont)
                CatText.font = CategoryFont;
            else
                CatText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            CatText.fontSize = CategoryNameFontSize;
            CatText.fontStyle = CategoryFontStyle;

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

            XMLDownloader = mainPrefab.gameObject.GetComponent<Demo1XmlDownloader>();
            XMLDownloader.Name = CategoryList[a].CategoryName;
            XMLDownloader.Mode = CategoryList[a].Mode;
            XMLDownloader.OnlinePath = CategoryList[a].OnlinePath;
            XMLDownloader.OfflinePath = CategoryList[a].OfflinePath;
            XMLDownloader.LevelPrefName = CategoryList[a].LevelPrefName;
            XMLDownloader.AnswerPrefName = CategoryList[a].AnswerPrefName;
            XMLDownloader.ScorePrefName = CategoryList[a].ScorePrefName;
            XMLDownloader.ShuffleQuestions = CategoryList[a].ShuffleQuestions;
            XMLDownloader.PerformanceText = PerformanceCatText;
        }
        CanvasSetter.AlignComponents();
        ProfileCanvasSetter.AlignComponents();

        GUIUtility.keyboardControl = 0;
    }

    public void LoadCategories()
    {
        CurCategories.Clear();

        for (int a = 0; a < GameCategories.childCount; a++)
            CurCategories.Add(GameCategories.GetChild(a));

        if (CurCategories.Count < 1)
        {
            EditorUtility.DisplayDialog("Load Categories", "No categories found", "Ok");
            return;
        }

        for (int a = 0; a < CurCategories.Count; a++)
        {
            CategoryList.Add(new Demo1CategoryFormatClass());
            Demo1XmlDownloader ExistingCategory = CurCategories[a].gameObject.GetComponent<Demo1XmlDownloader>();
            CategoryList[a].CategoryName = ExistingCategory.Name;
            CategoryList[a].CategoryImage = CurCategories[a].GetChild(0).gameObject.GetComponent<Image>().sprite;
            CategoryList[a].Mode = ExistingCategory.Mode;
            CategoryList[a].OnlinePath = ExistingCategory.OnlinePath;
            CategoryList[a].OfflinePath = ExistingCategory.OfflinePath;
            CategoryList[a].LevelPrefName = ExistingCategory.LevelPrefName;
            CategoryList[a].AnswerPrefName = ExistingCategory.AnswerPrefName;
            CategoryList[a].ScorePrefName = ExistingCategory.ScorePrefName;
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

    public void ShowCategoryPage()
    {
        Controller.MenuCanvas.SetActive(false);
        Controller.CategoryCanvas.SetActive(true);
        Controller.GameCanvas.SetActive(false);
        Controller.GameOverCanvas.SetActive(false);
        Controller.PauseCanvas.SetActive(false);
        Controller.ScreenshotCanvas.SetActive(false);

        Controller.ProfilePage.SetActive(false);
        Controller.Homepage.SetActive(true);
        Controller.SettingsPage.SetActive(false);

        Controller.ErrorPanel.SetActive(false);
        Controller.CategoryGroup.SetActive(true);
        Controller.Loading.gameObject.SetActive(false);
    }

    [ContextMenu("DELETE ALL CATEGORIES")]
    public void DeleteAllCat()
    {
        EditorApplication.Beep();
        if (EditorUtility.DisplayDialog("Delete ALL categories", "Are you sure you want to DELETE ALL categories?", "Yes", "No"))
        {
            GUIUtility.keyboardControl = 0;
            CategoryList.Clear();
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
#endif
#endif
}