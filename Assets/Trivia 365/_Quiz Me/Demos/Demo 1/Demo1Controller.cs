#if (UNITY_ANDROID || UNITY_IOS)
#define SUPPORTED_PLATFORM
#endif

#if UNITY_METRO
#pragma warning disable 0649
#endif

#if !USE_DOTWEEN
#pragma warning disable 0414
#endif

using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
#if USE_UNITYADS
using UnityEngine.Advertisements;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif
#if USE_DOTWEEN
using DG.Tweening;
#endif
#if USE_ADMOB
using GoogleMobileAds.Api;
#endif

public class Demo1Controller : MonoBehaviour
{
#if USE_ADMOB
#if UNITY_ANDROID
    [SerializeField]
    private string appId = "INSERT_APP_ID_HERE";
    [SerializeField]
    private string InterstitialAdUnitId = "INSERT_INTERSTITIAL_AD_UNIT_ID_HERE";
#elif UNITY_IOS
	[SerializeField]
	private string appId = "INSERT_APP_ID_HERE";
	[SerializeField]
	private string InterstitialAdUnitId = "INSERT_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
	[SerializeField]
	private string appId = "unexpected_platform";
	[SerializeField]
	private const string InterstitialAdUnitId = "unexpected_platform";
#endif

    [Tooltip("Show Consent")]
    public bool AskForConsent = true;

    [Tooltip("How often should the ad appear?")]
    [Range(1, 10)]
    public int ShowAfterHowManyGameovers = 2;

    [Tooltip("Enable Admob Test Mode.")]
    public TestMethod TestMode = TestMethod.EnableTestMode;

    private int AdCount;
#endif

#if (USE_UNITYADS)
#if UNITY_ANDROID
    [SerializeField]
    private string GameId = "INSERT_UNITY_ADS_GAME_ID";
#elif UNITY_IOS
	[SerializeField]
	private string GameId = "INSERT_UNITY_ADS_GAME_ID";
#else
	[SerializeField]
	private const string GameId = "unexpected_platform";
#endif

    [Tooltip("How many times should the ad dialog appear per game?")]
    [Range(1, 30)]
    public int AdLimit = 2;

    public bool UnityTestMode = false;
#endif
    [Tooltip("The Consent Canvas")]
    public GameObject ConsentCanvas;
    [Tooltip("The Menu Canvas")]
    public GameObject MenuCanvas;
    [Tooltip("The Category Canvas")]
    public GameObject CategoryCanvas;
    [Tooltip("The Game Canvas")]
    public GameObject GameCanvas;
    [Tooltip("The Screenshot Canvas")]
    public GameObject ScreenshotCanvas;
    [Tooltip("The Pause Canvas")]
    public GameObject PauseCanvas;
    [Tooltip("The GameOver Canvas")]
    public GameObject GameOverCanvas;


    [Tooltip("The profile button")]
    public GameObject ProfileButton;
    [Tooltip("The Logout Button")]
    public GameObject LogoutButton;
    [Tooltip("The back button")]
    public GameObject BackButton;
    [Tooltip("The Settings button")]
    public GameObject SettingsButton;
    [Tooltip("The homepage")]
    public GameObject Homepage;
    [Tooltip("The profile page")]
    public GameObject ProfilePage;
    [Tooltip("The Settings page")]
    public GameObject SettingsPage;
    [Tooltip("The cache panel")]
    public GameObject CachePanel;
    [Tooltip("The uniquecode panel")]
    public GameObject UniquecodePanel;
    [Tooltip("The create arena panel")]
    public GameObject CreateArenaPanel;
    [Tooltip("The histori arena panel")]
    public GameObject HistoriArenaPanel;
    [Tooltip("The detail arena panel")]
    public GameObject DetailArenaPanel;
    [Tooltip("The topbar text")]
    public Text TopBarText;
    [Tooltip("The avatar display component")]
    public Button ProfileAvatar;
    [Tooltip("The player's name input field")]
    public InputField Username;
    [Tooltip("The sound button")]
    public Image SoundButton;
    [Tooltip("The vibrate button image")]
    public Image VibrateButton;
    [Tooltip("The consent button")]
    public GameObject AdExperienceButton;

    [Tooltip("The category objects we will hide before showing the loading animation.")]
    public GameObject CategoryGroup;
    [Tooltip("The error panel")]
    public GameObject ErrorPanel;
    [Tooltip("The loading graphic")]
    public Animation Loading;

    [Tooltip("Text component used to display the current category name")]
    public Text CurrentCategoryText;
    [Tooltip("Text component used to display the question")]
    public Text QuestionDisplay;
    [Tooltip("Alt text component used to display the question")]
    public Text AltQuestionDisplay;
    [Tooltip("Image component used to display the image")]
    public Image ImageDispay;
    [Tooltip("Text component used to display the current score")]
    public Text ScoreText;
    [Tooltip("The Loading Animation gameoject")]
    public Animation ImageLoadingAnimation;
    [Tooltip("Text component used to display the time left to answer a question")]
    public Text TimerText;
    [Tooltip("Text component used to display the lives left")]
    public Text LivesText;
    [Tooltip("Button set when displaying text only questions")]
    public GameObject LongBarAnswers;
    [Tooltip("Button set when displaying text+picture questions")]
    public GameObject ShortBarAnswers;
    [Tooltip("The continue button displayed after a question")]
    public GameObject ContinueButton;
    [Tooltip("The quit game panel")]
    public GameObject QuitPanel;
    [Tooltip("The review button displayed after a question")]
    public GameObject ReviewButton;
    [Tooltip("The review panel")]
    public GameObject ReviewPanel;
    [Tooltip("The screenshot display view")]
    public Image ScreenshotView;
    [Tooltip("The Video Ad Panel")]
    public GameObject VideoAdPanel;

    [Tooltip("Text component used to display the score on GameOver")]
    public Text FinalScore;
    [Tooltip("Text component used to display the highscore on GameOver")]
    public Text CurHighscore;


    [Tooltip("The amount of time available to answer a question")]
    [Range(5, 1000)]
    public float TimerAmount;
    [Tooltip("The time in seconds before a download is declared to have failed. Make sure to factor devices with slow internet.")]
    [Range(5, 60)]
    public int DownloadTimeout = 10;
    [Tooltip("The number of lives in the game")]
    [Range(1, 999)]
    public int LivesCount = 5;
    [Tooltip("The avatar images")]
    public Sprite[] Avatars;
    [Tooltip("The correct answer sound effects")]
    public AudioClip CorrectSound;
    [Tooltip("The fail sound effects")]
    public AudioClip FailSound;
    [Tooltip("The ticking clock sound effects")]
    public AudioClip TickingSound;
    [Tooltip("The color to mark the button with the correct answer")]
    public Color CorrectColor;
    [Tooltip("The color to mark the button with the wrong answer")]
    public Color FailColor;
    [Tooltip("Enable pausing in-game")]
    public bool EnablePausing = true;


    [Tooltip("Text component used to display the question on the screenshot canvas")]
    public Text QuestionDisplaySc;
    [Tooltip("Image component used to display the image on the screenshot canvas")]
    public Image ImageDispaySc;
    [Tooltip("Button set when displaying text only questions")]
    public GameObject LongBarAnswersSc;
    [Tooltip("Button set when displaying text+picture questions")]
    public GameObject ShortBarAnswersSc;

    [HideInInspector]
    [Tooltip("A list we use to store the questions")]
    public List<QuizApp> Questions;
    [HideInInspector]
    [Tooltip("A list we use to store the answers to the current question")]
    public List<Answers> AnswersAvailable;

    private Texture2D curTex;
    private Sprite curSprite;
    private List<String> ImageLinks = new List<string>();
    private List<String> FaultyLinks = new List<string>();
    private Texture2D ScreenshotTexture;
    private Sprite ScreenshotSp;
    private byte[] ImageBytes;
    //The screenshot names
    private string ScreenshotName = "Screenshot";
    internal string ShareImageName = "OriginalImg";
    //A general use index
    private int index = 0;
    //The current question
    private int CurrentQuestion;
    //Answer of currently displayed question
    private int CorrectAnswer = 0;
    //A bool we will use to check if the current question is text only or text+picture
    internal bool TextOnly = false;
    //Countdown timer for a question
    private float TimeLeft;
    //A bool we will use to check if the user answered the question
    private bool Answered = false;
    //A bool we will use to check if the game is paused
    private bool Paused = false;
    //The floats we use to keep track of the score
    private float score = 0;
    private Animation scoreAnimation;
    //The float we use to keep track of the highscore
    internal float highscore = 0;
    //The text that appears before the score. ex: 'Score'
    private string Prefix = "Score: ";
    //A bool we will use to check if the game is over
    private bool GameOver = true;
    //A bool we will use to check if the sound ticking effect is playing
    private bool ticking = false;
    //Number of available lives.
    internal int AvailableLives;
    private Animation livesAnimation;
    //Audiosource component
    private AudioSource SoundFX;
    //An int we will use to store the sound state (Sound off or Sound on)
    private int Sound = 0;
    //The sound text
    private Text SoundText;
#if (SUPPORTED_PLATFORM || UNITY_EDITOR)
    //An int we will use to store the vibration state (vibration off or vibration on)
    private int VibrateState = 0;
    //The vibration text
    private Text VibrateText;
    //Vibration bool
    private bool CanVibrate;
#endif
    //A bool we will use to check if the category canvas is active
    private bool CategoryShow = false;
    //The current active avatar sprite
    private int ActiveAvatar;
    //Check if we are on the main menu
    private bool isMenu;
    //Check if we are playing
    private bool Playing;
    //Should we shuffle the questions
    private bool RandomOrder;
    //Player's current level for a particular level
    private int CurrentLevel;
    //Answer text holders
    private Text[] AnswerA, AnswerB, AnswerC, AnswerD = new Text[2];
    //The categories in your game
    private Demo1XmlDownloader[] GameCategories = new Demo1XmlDownloader[0];
    //A count of the correctly answered questions for a particular category
    private int CorrectlyAnswered;
    //Level player pref string
    private string LevelPref;
    //Correct Answer pref string
    private string AnswerPref;
    //Highscore pref string
    private string hScorePref;
    //Check if we are using the online mode or offline mode
    private bool OnlineMode;
    //The timer coroutine
    private Coroutine Timer;

    private Transform LongBarA,
        LongBarB,
        LongBarC,
        LongBarD,
        ShortBarA,
        ShortBarB,
        ShortBarC,
        ShortBarD;

    private Transform LongBarScC,
    LongBarScD,
    ShortBarScC,
    ShortBarScD;

    private Text[] AnswervA, AnswervB, AnswervC, AnswervD = new Text[2];

    private const float fadeDuration = 0.5f;

    //Consent PlayerPref
    const string consented = "UserConsent";

#if USE_UNITYADS
    private int VideoCounter;

    private bool ImageShowing;
#endif

#if USE_DOTWEEN
    // Use this for initialization
    void Start()
    {
#if (USE_UNITYADS)
        InitializeAds(GameId, UnityTestMode);
#endif

#if USE_ADMOB
        AdCount = ShowAfterHowManyGameovers;

        if (TestMode == TestMethod.DisableTestMode)
        {
            MobileAds.Initialize(appId);

            AdmobManager.instance.TestMode = TestMethod.DisableTestMode;
        }
        else
        {
            MobileAds.Initialize(AdmobManager.instance.GoogleSampleAppId);

            AdmobManager.instance.TestMode = TestMethod.EnableTestMode;
        }
#endif

        //Get all the categories
        CategoryCanvas.SetActive(true);
        int Count = FindObjectsOfType(typeof(Demo1XmlDownloader)).Length;
        GameCategories = new Demo1XmlDownloader[Count];
        GameCategories = FindObjectsOfType(typeof(Demo1XmlDownloader)) as Demo1XmlDownloader[];

        LongBarA = LongBarAnswers.transform.GetChild(0);
        LongBarB = LongBarAnswers.transform.GetChild(1);
        LongBarC = LongBarAnswers.transform.GetChild(2);
        LongBarD = LongBarAnswers.transform.GetChild(3);

        ShortBarA = ShortBarAnswers.transform.GetChild(0);
        ShortBarB = ShortBarAnswers.transform.GetChild(1);
        ShortBarC = ShortBarAnswers.transform.GetChild(2);
        ShortBarD = ShortBarAnswers.transform.GetChild(3);

        AnswerA = new Text[2];
        AnswerB = new Text[2];
        AnswerC = new Text[2];
        AnswerD = new Text[2];

        AnswerA[0] = LongBarA.GetChild(0).GetComponent<Text>();
        AnswerB[0] = LongBarB.GetChild(0).GetComponent<Text>();
        AnswerC[0] = LongBarC.GetChild(0).GetComponent<Text>();
        AnswerD[0] = LongBarD.GetChild(0).GetComponent<Text>();

        AnswerA[1] = ShortBarA.GetChild(0).GetComponent<Text>();
        AnswerB[1] = ShortBarB.GetChild(0).GetComponent<Text>();
        AnswerC[1] = ShortBarC.GetChild(0).GetComponent<Text>();
        AnswerD[1] = ShortBarD.GetChild(0).GetComponent<Text>();

        LongBarScC = LongBarAnswersSc.transform.GetChild(2);
        LongBarScD = LongBarAnswersSc.transform.GetChild(3);

        ShortBarScC = ShortBarAnswersSc.transform.GetChild(2);
        ShortBarScD = ShortBarAnswersSc.transform.GetChild(3);

        AnswervA = new Text[2];
        AnswervB = new Text[2];
        AnswervC = new Text[2];
        AnswervD = new Text[2];

        AnswervA[0] = LongBarAnswersSc.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        AnswervB[0] = LongBarAnswersSc.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        AnswervC[0] = LongBarAnswersSc.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        AnswervD[0] = LongBarAnswersSc.transform.GetChild(3).GetChild(0).GetComponent<Text>();

        AnswervA[1] = ShortBarAnswersSc.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        AnswervB[1] = ShortBarAnswersSc.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        AnswervC[1] = ShortBarAnswersSc.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        AnswervD[1] = ShortBarAnswersSc.transform.GetChild(3).GetChild(0).GetComponent<Text>();

        scoreAnimation = ScoreText.gameObject.GetComponent<Animation>();
        livesAnimation = LivesText.gameObject.GetComponent<Animation>();

        Time.timeScale = 1;
        //Set menu bool to true
        isMenu = true;
        //Get the audiosource component
        SoundFX = GetComponent<AudioSource>();
        //Get the current avatar
        ActiveAvatar = PlayerPrefs.GetInt("Avatar", 0);
        //Get the player's name
        string Name = PlayerPrefs.GetString("Username");
        //Set the players avatar icon on the profile page
        ProfileAvatar.GetComponent<Image>().sprite = Avatars[ActiveAvatar];
        //Set the player's name on the profile page
        Username.text = Name;
        //Get the sound state( 1 - Sound on. 0 - Sound off)
        Sound = PlayerPrefs.GetInt("Sound", 1);
        //Cache the sound text
        SoundText = SoundButton.GetComponentInChildren<Text>();
        //Update the sound settings
        if (Sound == 0)
        {
            AudioListener.pause = true;
            SoundText.text = "Sound OFF";
            SoundButton.color = Color.red;
        }
        else if (Sound == 1)
        {
            AudioListener.pause = false;
            SoundText.text = "Sound ON";
            SoundButton.color = Color.green;
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            AudioListener.pause = false;
            SoundText.text = "Sound ON";
            SoundButton.color = Color.green;
        }

#if (UNITY_EDITOR || SUPPORTED_PLATFORM)
        //Get the vibration state( 1 - vibration on. 0 - vibration off)
        VibrateState = PlayerPrefs.GetInt("Vibration", 1);
        //Cache the vibration text
        VibrateText = VibrateButton.GetComponentInChildren<Text>();
        //Update the sound settings
        if (VibrateState == 0)
        {
            CanVibrate = false;
            VibrateText.text = "Vibration OFF";
            VibrateButton.color = Color.red;
        }
        else if (VibrateState == 1)
        {
            CanVibrate = true;
            VibrateText.text = "Vibration ON";
            VibrateButton.color = Color.green;
        }
        else
        {
            CanVibrate = true;
            PlayerPrefs.SetInt("Vibration", 1);
            VibrateText.text = "Vibration ON";
            VibrateButton.color = Color.green;
        }
#else
		VibrateButton.gameObject.SetActive(false);
#endif

        //Disable all other canvases
        GameCanvas.SetActive(false);
        CategoryCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        ScreenshotCanvas.SetActive(false);

#if USE_ADMOB
        if (!AskForConsent || PlayerPrefs.HasKey(consented))
        {
            if (AskForConsent)
                AdmobManager.instance.NPA = PlayerPrefs.GetInt(consented) == 1 ? false : true;
            else
                AdmobManager.instance.NPA = false;

            ConsentCanvas.SetActive(false);
        }
        else
        {
            ConsentCanvas.SetActive(true);
        }

        AdExperienceButton.SetActive(true);
#else
        AdExperienceButton.SetActive(false);

        ConsentCanvas.SetActive(false);
#endif

        MenuCanvas.SetActive(true);

        //Setup the menu components
        ProfileButton.SetActive(true);
        LogoutButton.SetActive(false);
        BackButton.SetActive(false);
        SettingsButton.SetActive(true);
        Homepage.SetActive(true);
        ProfilePage.SetActive(false);
        SettingsPage.SetActive(false);
        TopBarText.text = "Home";

        //Set up the bools
        GameOver = true;
        CategoryShow = false;
        Paused = false;
        isMenu = true;
        Playing = false;
    }

#if USE_UNITYADS
    public static void InitializeAds(string gameId, bool testMode)
    {
        if ((gameId == null) || (gameId.Trim().Length == 0))
        {
#if UNITY_EDITOR
            Debug.Log("Game ID is null");
#endif

            return;
        }

        Advertisement.Initialize(gameId, testMode);
    }
#endif

    // Update is called once per frame
    void Update()
    {
        //Pause or resume when the back key/escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOver && !Paused)
            PauseGame();
        else if (Input.GetKeyDown(KeyCode.Escape) && !GameOver && Paused)
            ResumeGame();

        if (Input.GetKeyDown(KeyCode.Escape) && CategoryShow)
            HideCategory();
        else if (Input.GetKeyDown(KeyCode.Escape) && GameOver && !Paused && !CategoryShow && !isMenu && !Playing)
            ShowMainMenu();
        else if (Input.GetKeyDown(KeyCode.Escape) && GameOver && !Paused && !CategoryShow && isMenu && !Playing)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();

            //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

#if USE_ADMOB
    public void ShowConsentScreen()
    {
        ConsentCanvas.SetActive(true);
    }

    public void AcceptConsent()
    {
        PlayerPrefs.SetInt(consented, 1);

        AdmobManager.instance.NPA = false;

        ConsentCanvas.SetActive(false);
    }

    public void DeclineConsent()
    {
        PlayerPrefs.SetInt(consented, 0);

        AdmobManager.instance.NPA = true;

        ConsentCanvas.SetActive(false);
    }
#endif

    public void ShowMainMenu()
    {
        ProfilePage.SetActive(false);
        SettingsPage.SetActive(false);

        BackButton.SetActive(false);
        ProfileButton.SetActive(true);
        LogoutButton.SetActive(false);
        SettingsButton.SetActive(true);

        TopBarText.text = "Home";

        isMenu = true;
    }

    public void ShowProfile()
    {
        UpdateProfile();

        ProfilePage.SetActive(true);

        ProfileButton.SetActive(false);
        SettingsButton.SetActive(false);
        BackButton.SetActive(true);
        LogoutButton.SetActive(true);

        TopBarText.text = "Profile";

        isMenu = false;
    }

    public void Logout()
    {
        WWW www = new WWW(ApiConstant.SERVER + "/logout");
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
    }

    //Used to update the details on the profile page
    void UpdateProfile()
    {
        for (int i = 0; i < GameCategories.Length; i++)
            if (GameCategories[i].PerformanceText)
                GameCategories[i].PerformanceText.text = "Level " + PlayerPrefs.GetInt(GameCategories[i].LevelPrefName, 1).ToString();
    }

    //Change the current username and save it
    /*public void SaveUsername()
    {
        if (Username.text.Length < 1)
        {
            PlayerPrefs.SetString("Username", "Username");
            PlayerPrefs.Save();
            String Name = PlayerPrefs.GetString("Username", "Username");
            Username.text = Name;
        }
        else
        {
            PlayerPrefs.SetString("Username", Username.text);
            PlayerPrefs.Save();
            String Name = PlayerPrefs.GetString("Username", "Username");
            Username.text = Name;
        }
    }*/

    //Change the current avatar icon and saves it
    public void ChangeAvatar()
    {
        if (ActiveAvatar < Avatars.Length - 1)
            ActiveAvatar++;
        else
            ActiveAvatar = 0;

        ProfileAvatar.GetComponent<Image>().sprite = Avatars[ActiveAvatar];
        PlayerPrefs.SetInt("Avatar", ActiveAvatar);
    }

    //Show settings
    public void ShowSettings()
    {
        SettingsPage.SetActive(true);

        SettingsButton.SetActive(false);
        SettingsButton.SetActive(false);
        ProfileButton.SetActive(false);
        BackButton.SetActive(true);
        LogoutButton.SetActive(false);

        isMenu = false;

        TopBarText.text = "Settings";
    }

    //Change sound state
    public void ToggleSound()
    {
        //Get sound state
        Sound = PlayerPrefs.GetInt("Sound", 1);

        //Mute or unmute depending on the current state
        if (Sound == 0)
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt("Sound", 1);
            SoundText.text = "Sound ON";
            SoundButton.color = Color.green;
        }
        else if (Sound == 1)
        {
            AudioListener.pause = true;
            PlayerPrefs.SetInt("Sound", 0);
            SoundText.text = "Sound OFF";
            SoundButton.color = Color.red;
        }

        SoundFX.Stop();
    }

#if (UNITY_EDITOR || SUPPORTED_PLATFORM)
    //Change vibration state
    public void ToggleVibration()
    {
        //Get vibration state
        VibrateState = PlayerPrefs.GetInt("Vibration", 1);

        if (VibrateState == 0)
        {
            CanVibrate = true;
            PlayerPrefs.SetInt("Vibration", 1);
            VibrateText.text = "Vibration ON";
            VibrateButton.color = Color.green;
        }
        else if (VibrateState == 1)
        {
            CanVibrate = false;
            PlayerPrefs.SetInt("Vibration", 0);
            VibrateText.text = "Vibration OFF";
            VibrateButton.color = Color.red;
        }
    }
#endif

    //Hide Category Canvas
    public void HideCategory()
    {
        if (!CategoryShow)
            return;

        CategoryCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
        CategoryShow = false;
        isMenu = true;
    }

    //Start the game
    public void StartGame()
    {
        //Hide loading animation
        if (Loading)
            Loading.Stop();
        if (Loading)
            Loading.gameObject.SetActive(false);

        //Disable menu canvas
        MenuCanvas.SetActive(false);

        //Display category canvas
        CategoryCanvas.SetActive(true);

        //Set up the bools
        GameOver = true;
        CategoryShow = true;
        Paused = false;
        isMenu = false;
        Playing = false;

        //Active the category group
        CategoryGroup.SetActive(true);

        //Hide the error panel
        ErrorPanel.SetActive(false);
    }

    internal void PlayGame(string CategoryName = "", string OnlinePath = "", string OfflinePath = "", int Mode = 3, string LevelPrefName = "", string AnswerPrefName = "", string ScorePrefName = "", bool ShuffleQuestions = true)
    {
        RandomOrder = ShuffleQuestions;

        CurrentCategoryText.text = CategoryName;

        LevelPref = LevelPrefName;
        AnswerPref = AnswerPrefName;
        hScorePref = ScorePrefName;

        //Get the correctly answered and current level ints. We will use this later to update the player's level for the loaded category
        CorrectlyAnswered = PlayerPrefs.GetInt(AnswerPref, 0);
        CurrentLevel = PlayerPrefs.GetInt(LevelPref, 1);

        //Set the Playing bool to true
        Playing = true;

        //Set the category check bool to false
        CategoryShow = false;

        //Show loading animation
        CategoryGroup.SetActive(false);
        if (Loading)
            Loading.gameObject.SetActive(true);
        if (Loading)
            Loading.Play();

        OfflinePath = "XML/" + OfflinePath;

        if (Mode == 1)
            StartCoroutine(DownlaodXML(OnlinePath));
        else if (Mode == 2)
            StartCoroutine(LoadQuestions(OfflinePath));
        else
            StartCoroutine(DownlaodXML(OnlinePath, OfflinePath, true));

#if (USE_ADMOB && SUPPORTED_PLATFORM)
        if (TestMode == TestMethod.DisableTestMode)
            AdmobManager.instance.RequestInterstitial(InterstitialAdUnitId);
        else
            AdmobManager.instance.RequestInterstitial(AdmobManager.instance.GoogleSampleTestID);
#endif
    }

    //Load the question from the xml to the questions array
    private IEnumerator LoadQuestions(string category)
    {
        //load the questions from the XML
        QuizAppContainer ic = QuizAppContainer.Load(category);

        if (ic == null)
        {
            if (Loading)
                Loading.Stop();
            if (Loading)
                Loading.gameObject.SetActive(false);
            ErrorPanel.SetActive(true);

            yield break;
        }

        //Clear List<QuizApp> contents
        if (Questions.Count > 0)
            Questions.Clear();

        //Parse the questions from the XML file to the list 
        foreach (QuizApp item in ic.Quizes)
            Questions.Add(item);

        OnlineMode = false;

        //Initialize new game
        PrepareNewGame();
    }

    //Load the question from the xml we download
    private IEnumerator DownlaodXML(string OnlinePath, string OfflinePath = null, bool HybridMode = false)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(OnlinePath))
        {
            uwr.timeout = DownloadTimeout;

            yield return uwr.SendWebRequest();

            if (uwr.isHttpError || uwr.isNetworkError)
            {
                if (HybridMode)
                {
                    StartCoroutine(LoadQuestions(OfflinePath));
                }
                else
                {
                    ErrorPanel.SetActive(true);
                    if (Loading)
                        Loading.Stop();
                    if (Loading)
                        Loading.gameObject.SetActive(false);
                }
            }
            else
            {
                StringReader reader = new StringReader(uwr.downloadHandler.text);
                QuizAppContainer Qac = QuizAppContainer.DownloadedXML(reader);

                //Clear List<QuizApp> contents
                if (Questions.Count > 0)
                    Questions.Clear();

                //Parse the questions from the XML file to the list 
                foreach (QuizApp item in Qac.Quizes)
                    Questions.Add(item);

                OnlineMode = true;

                //Initialize new game
                PrepareNewGame();
            }
        }
    }

    //Closes the error panel
    public void CloseError()
    {
        ErrorPanel.SetActive(false);

        StartGame();
    }

    public void ShowCachePanel()
    {
        CachePanel.SetActive(true);
    }

    public void ShowUniquecodePanel()
    {
        UniquecodePanel.SetActive(true);
    }

    public void ShowDetailArenaPanel()
    {
        PlayerPrefs.SetInt("isDataUpdated", 1);
        DetailArenaPanel.SetActive(true);
    }

    public void ShowHistoriArenaPanel()
    {
        HistoriArenaPanel.SetActive(true);
    }

    public void ShowCreateArenaPanel()
    {
        CreateArenaPanel.SetActive(true);
    }

    public void CloseCreateArenaPanel()
    {
        CreateArenaPanel.SetActive(false);
    }
    public void CloseCachePanel()
    {
        CachePanel.SetActive(false);
    }

    public void CloseUnicquecodePanel()
    {
        UniquecodePanel.SetActive(false);
    }

    public void CloseDetailArenaPanel()
    {
        DetailArenaPanel.SetActive(false);
    }

    public void CloseHistoriArenaPanel()
    {
        HistoriArenaPanel.SetActive(false);
    }

    public void DeleteCache()
    {
        string path = Application.temporaryCachePath;

        DirectoryInfo di = new DirectoryInfo(path);

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }

        CloseCachePanel();
    }

    public string CalculateMD5Hash(string input)
    {
        UTF8Encoding ue = new UTF8Encoding();
        byte[] bytes = ue.GetBytes(input);

        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }

        return sb.ToString();
    }

    private IEnumerator CacheImages()
    {
        if (ImageLinks.Count <= 0 || GameOver)
            yield break;

        string EncodedString = CalculateMD5Hash(ImageLinks[0]);

        string filePath = Path.Combine(Application.temporaryCachePath, EncodedString);

        //Check if the image already exists
        if (!File.Exists(filePath))
        {
            //Initialize a new bool and set it to false. We will use this to retry a download in case it fails on the first try
            int retried = 0;

        //We will use this if we need to retry a download in case it fails on the first try
        TryAgain:

            //Start a new download from the provided URL
            using (UnityWebRequest CacheRequest = UnityWebRequestTexture.GetTexture(ImageLinks[0]))
            {
                CacheRequest.timeout = DownloadTimeout;

                yield return CacheRequest.SendWebRequest();

                //Check if the downloaded successfully
                if (!CacheRequest.isNetworkError && !CacheRequest.isHttpError)
                {
                    Texture2D testTexture = DownloadHandlerTexture.GetContent(CacheRequest);

                    if (testTexture.width == 8 && testTexture.height == 8)
                    {
#if DEBUG
                        Debug.LogWarning("Unsupported image was downloaded. Deleting file...");
#endif
                        if (File.Exists(filePath))
                            File.Delete(filePath);

                        if (retried < 2)
                        {
                            yield return new WaitForSeconds(1);
                            retried++;
                            goto TryAgain;
                        }
                        else
                        {
                            FaultyLinks.Add(ImageLinks[0]);
                            ImageLinks.RemoveAt(0);
                            StartCoroutine(CacheImages());
                        }

                    }
                    else
                    {
                        File.WriteAllBytes(filePath, CacheRequest.downloadHandler.data);
                        ImageLinks.RemoveAt(0);
                        StartCoroutine(CacheImages());
                    }

                    Destroy(testTexture);
                }
                else
                {
                    //If the download failed, retry again in x seconds if we haven't retried it before
                    if (retried < 2)
                    {
                        yield return new WaitForSeconds(1);
                        retried++;
                        goto TryAgain;
                    }
                    else
                    {
                        FaultyLinks.Add(ImageLinks[0]);
                        ImageLinks.RemoveAt(0);
                        StartCoroutine(CacheImages());
                    }
                }
            }
        }
        else
        {
            ImageLinks.RemoveAt(0);
            StartCoroutine(CacheImages());
        }
    }

    //Shuffles the question list
    void ShuffleQuestions()
    {
        if (!RandomOrder)
            return;

        // Go through all the questions and shuffle them
        for (index = 0; index < Questions.Count; index++)
        {
            // Hold the questions in a temporary variable
            QuizApp tempNumber = Questions[index];

            // Choose a random index from the text list
            int randomIndex = UnityEngine.Random.Range(index, Questions.Count);

            // Assign a random text from the list
            Questions[index] = Questions[randomIndex];

            // Assign the temporary text to the random question we chose
            Questions[randomIndex] = tempNumber;
        }
    }

    //Shuffles the answer list
    void ShuffleAnswers()
    {
        // Go through all the answers and shuffle them
        for (index = 0; index < AnswersAvailable.Count; index++)
        {
            // Hold the answers in a temporary variable
            Answers tempNumber = AnswersAvailable[index];

            // Choose a random index from the text list
            int randomIndex = UnityEngine.Random.Range(index, AnswersAvailable.Count);

            // Assign a random text from the list
            AnswersAvailable[index] = AnswersAvailable[randomIndex];

            // Assign the temporary text to the random answer we chose
            AnswersAvailable[randomIndex] = tempNumber;
        }
    }

    //Receives the player's answer and then checks if it is correct
    public void Answers(int ButtonIndex)
    {
        //Set "Answered" bool to true
        Answered = true;

        ticking = false;

        //Disable the buttons to prevent further user input
        if (TextOnly)
            LongBarAnswers.GetComponent<CanvasGroup>().blocksRaycasts = false;
        else
            ShortBarAnswers.GetComponent<CanvasGroup>().blocksRaycasts = false;

        if (CorrectAnswer == ButtonIndex)
        {
            //Check if the audiosource is playing a sound effect, stop it and play new effect. Otherwise just play the effect
            if (SoundFX.isPlaying)
            {
                SoundFX.Stop();
                SoundFX.PlayOneShot(CorrectSound);
            }
            else
                SoundFX.PlayOneShot(CorrectSound);

            SetScore();

            //Set the correct answer button to green to display a pass
            InputCorrect();
        }
        else
        {
            //Check if the audiosource is playing a sound effect, stop it and play new effect. Otherwise just play the effect
            if (SoundFX.isPlaying)
            {
                SoundFX.Stop();
                SoundFX.PlayOneShot(FailSound);
            }
            else
                SoundFX.PlayOneShot(FailSound);

            livesAnimation.Play();

#if (SUPPORTED_PLATFORM || UNITY_EDITOR)
            if (CanVibrate)
                Handheld.Vibrate();
#endif

            //Set the user input to red to display a fail and show the correct color
            InputWrong(ButtonIndex);
            InputCorrect();

            //Subtract one life
            if (AvailableLives > 0)
                AvailableLives -= 1;

            //Update the UI 
            LivesText.text = "Lives: " + AvailableLives.ToString();
        }

        StartCoroutine(CaptureScreenshot());
    }

    private void InputCorrect()
    {
        if (CorrectAnswer == 0)
        {
            if (TextOnly)
            {
                AnswerA[0].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                AnswervA[0].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
            }
            else
            {
                AnswerA[1].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                AnswervA[1].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
            }
        }
        else if (CorrectAnswer == 1)
        {
            if (TextOnly)
            {
                AnswerB[0].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                AnswervB[0].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
            }
            else
            {
                AnswerB[1].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                AnswervB[1].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
            }
        }
        else if (CorrectAnswer == 2)
        {
            if (TextOnly)
            {
                AnswerC[0].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                AnswervC[0].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
            }
            else
            {
                AnswerC[1].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                AnswervC[1].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
            }
        }
        else if (CorrectAnswer == 3)
        {
            if (TextOnly)
            {
                AnswerD[0].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                AnswervD[0].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
            }
            else
            {
                AnswerD[1].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                AnswervD[1].transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
            }
        }
    }

    private void InputWrong(int ButtonPressed)
    {
        if (ButtonPressed == 0)
        {
            if (TextOnly)
            {
                AnswerA[0].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                AnswervA[0].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
            }
            else
            {
                AnswerA[1].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                AnswervA[1].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
            }
        }
        else if (ButtonPressed == 1)
        {
            if (TextOnly)
            {
                AnswerB[0].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                AnswervB[0].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
            }
            else
            {
                AnswerB[1].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                AnswervB[1].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
            }
        }
        else if (ButtonPressed == 2)
        {
            if (TextOnly)
            {
                AnswerC[0].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                AnswervC[0].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
            }
            else
            {
                AnswerC[1].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                AnswervC[1].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
            }
        }
        else if (ButtonPressed == 3)
        {
            if (TextOnly)
            {
                AnswerD[0].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                AnswervD[0].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
            }
            else
            {
                AnswerD[1].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                AnswervD[1].transform.parent.gameObject.GetComponent<Image>().color = FailColor;
            }
        }
    }

    private IEnumerator CaptureScreenshot(float delay = 0.5f)
    {
        yield return new WaitForSeconds(delay);
        //Hide timer, score text and life displays
        TimerText.gameObject.SetActive(false);

        CamScreenshot.instance.CaptureScreenshot(ScreenshotName);

        StartCoroutine(ShowContinue());
    }

    //Set the score and update the level progress stats
    void SetScore()
    {
        //Increase score
        float a = 10 * TimeLeft;
        score += a;

        UpdateScore();

        //Increase the correct answers counter by one
        CorrectlyAnswered += 1;

        //This is the number of correct answers required to the next level
        int maxValue = CurrentLevel * 5;

        //If the correct answers so far are less than the value above increase the correct answer counter
        if (CorrectlyAnswered < maxValue)
        {
            PlayerPrefs.SetInt(AnswerPref, CorrectlyAnswered);
        }

        //If the value is equal or greater than the max value, increase the level by one and reset the correct answers counter to zero
        else if (CorrectlyAnswered >= maxValue)
        {
            CurrentLevel += 1;
            PlayerPrefs.SetInt(LevelPref, CurrentLevel);
            PlayerPrefs.SetInt(AnswerPref, 0);
        }

        //Save the playerprefs
        PlayerPrefs.Save();
    }

    //Displays the current score on UI
    void UpdateScore()
    {
        scoreAnimation.Play();

        ScoreText.text = Prefix + score.ToString();

    }

    private IEnumerator ShowContinue(float delay = 0.5f)
    {
        Resources.UnloadUnusedAssets();

        yield return new WaitForSeconds(delay);

        //Show the continue button
        ContinueButton.transform.parent.gameObject.SetActive(true);
        ContinueButton.SetActive(true);

#if USE_UNITYADS
        if (AvailableLives <= 0 && VideoCounter > 0 && Advertisement.IsReady())
        {
            if (ImageDispay.gameObject.activeInHierarchy)
                ImageShowing = true;
            else
                ImageShowing = false;

            ImageDispay.DOFade(0, fadeDuration).OnComplete(() =>
                {
                    ImageDispay.gameObject.SetActive(false);
                });

            VideoAdPanel.SetActive(true);
        }
#endif

#if (SUPPORTED_PLATFORM || UNITY_EDITOR)
        ReviewButton.SetActive(true);
#else
		ReviewButton.SetActive(false);
#endif
        //Hide the answer buttons
        if (LongBarAnswers)
            LongBarAnswers.SetActive(false);
        if (ShortBarAnswers)
            ShortBarAnswers.SetActive(false);

        LongBarA.localScale = new Vector3(0, 0, 0);
        LongBarB.localScale = new Vector3(0, 0, 0);
        LongBarC.localScale = new Vector3(0, 0, 0);
        LongBarD.localScale = new Vector3(0, 0, 0);

        ShortBarA.localScale = new Vector3(0, 0, 0);
        ShortBarB.localScale = new Vector3(0, 0, 0);
        ShortBarC.localScale = new Vector3(0, 0, 0);
        ShortBarD.localScale = new Vector3(0, 0, 0);

        //Reset the answer buttons color to white
        for (int t = 0; t < AnswerA.Length; t++)
        {
            AnswerA[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswerB[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswerC[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswerD[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;

            AnswervA[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswervB[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswervC[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswervD[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
        }

        CamScreenshot.instance.CaptureScreenshot(ShareImageName);

#if (SUPPORTED_PLATFORM || UNITY_EDITOR)
        //Load the image from disk
        ImageBytes = File.ReadAllBytes(CamScreenshot.instance.ScreenShotName(ScreenshotName));

        if (ScreenshotTexture)
            Texture2D.Destroy(ScreenshotTexture);
        if (ScreenshotSp)
            Sprite.Destroy(ScreenshotSp);

        ScreenshotView.sprite = null;
#endif
    }

#if (USE_UNITYADS && SUPPORTED_PLATFORM)
    public void ShowVideoAd()
    {
        if (Advertisement.IsReady())
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                VideoCounter--;
                AvailableLives++;
                LivesText.text = "Lives: " + AvailableLives.ToString();
                VideoAdPanel.SetActive(false);
                if (ImageShowing)
                {
                    ImageDispay.gameObject.SetActive(true);
                    ImageDispay.DOFade(1, fadeDuration);
                }
                break;

            case ShowResult.Failed:
                VideoAdPanel.SetActive(false);
                if (ImageShowing)
                {
                    ImageDispay.gameObject.SetActive(true);
                    ImageDispay.DOFade(1, fadeDuration);
                }
                break;

            case ShowResult.Skipped:
                VideoAdPanel.SetActive(false);
                if (ImageShowing)
                {
                    ImageDispay.gameObject.SetActive(true);
                    ImageDispay.DOFade(1, fadeDuration);
                }
                break;
        }
    }
#endif

    public void ReviewQuestion()
    {
#if (SUPPORTED_PLATFORM || UNITY_EDITOR)
        //Hide the continue button
        ContinueButton.SetActive(false);
        ReviewButton.SetActive(false);

        //Show the review panel
        if (ReviewPanel)
            ReviewPanel.SetActive(true);

        if (ScreenshotView.sprite != null)
            return;

        ScreenshotTexture = new Texture2D(2, 2, TextureFormat.RGB24, false, true);
        ScreenshotTexture.filterMode = FilterMode.Bilinear;

        ScreenshotTexture.LoadImage(ImageBytes);

        ScreenshotSp = Sprite.Create(ScreenshotTexture, new Rect(0, 0, ScreenshotTexture.width, ScreenshotTexture.height), new Vector2(0.5f, 0), 1);

        //Display the screenshot on the screenshot view
        ScreenshotView.sprite = ScreenshotSp;
#endif
    }

    public void HideReview()
    {
        //Show the continue button
        ContinueButton.SetActive(true);
#if (SUPPORTED_PLATFORM || UNITY_EDITOR)
        ReviewButton.SetActive(true);
#endif

        //hide the review panel
        if (ReviewPanel)
            ReviewPanel.SetActive(false);
    }

    public void Continue()
    {
        //Check if we have available lives remaining
        CheckLives();

        //Reset Timer
        TimeLeft = TimerAmount;

        //Reset timer text
        TimerText.text = TimeLeft.ToString("0") + "'";

        //Show timers, score text and life display
        TimerText.gameObject.SetActive(true);
        //ScoreText.gameObject.SetActive(true);
        //LifeFill.transform.parent.gameObject.SetActive(true);
    }

    //Submit Arena
    IEnumerator SubmitArena(float score)
    {
        WWWForm form = new WWWForm();
        form.AddField("id_arena", PlayerPrefs.GetInt("idArena"));
        form.AddField("score", score.ToString());

        WWW www = new WWW(ApiConstant.SERVER + "/submitarena", form);
        yield return www;

        Debug.Log("Submit Arena : " + www.text);

        //Set Arena Offline Again
        PlayerPrefs.SetInt("isArenaOnline", 0);
        PlayerPrefs.SetInt("isDataUpdated", 1);
    }

    //Checks if we have available lives and either end the game or continue
    void CheckLives()
    {
        if (AvailableLives <= 0)
        {
            //Set gameOver bool to true
            GameOver = true;

            //Check highscore
            if (score > highscore)
            {
                PlayerPrefs.SetFloat(hScorePref, score);
            }
            //Disable game canvas
            GameCanvas.SetActive(false);
            //Show gameOver canvas
            GameOverCanvas.SetActive(true);
            //Display score for the round
            FinalScore.text = score.ToString("0");
            //Display highscore
            CurHighscore.text = "Highscore: " + PlayerPrefs.GetFloat(hScorePref, 0).ToString("0");

            //Call API Submit Arena
            if (PlayerPrefs.GetInt("isArenaOnline", 0) == 1)
            {
                StartCoroutine(SubmitArena(PlayerPrefs.GetFloat(hScorePref, 0)));
            }
        }
        else
        {
            //Check if all questions are answers
            if (CurrentQuestion < Questions.Count - 1)
            {
                //Increment the current question
                CurrentQuestion++;

                //Display the next question
                StartCoroutine(NextQuestion());
            }
            else
            {
                //Set gameOver bool to true
                GameOver = true;

                //Check highscore
                if (score > highscore)
                {
                    PlayerPrefs.SetFloat(hScorePref, score);
                }
                //Disable game canvas
                GameCanvas.SetActive(false);
                //Show gameOver canvas
                GameOverCanvas.SetActive(true);

                FinalScore.text = "High Score";
                //Display scores
                CurHighscore.text = "\nScore: " + score.ToString("0") + "\n" +
                "Highscore: " + PlayerPrefs.GetFloat(hScorePref, 0).ToString("0");

                //Call API Submit Arena
                if (PlayerPrefs.GetInt("isArenaOnline", 0) == 1)
                {
                    StartCoroutine(SubmitArena(PlayerPrefs.GetFloat(hScorePref, 0)));
                }
            }
        }

        if (GameOver)
        {
#if USE_ADMOB
            if (AdCount <= 1)
            {
                AdmobManager.instance.ShowInterstitial();

                //Reset the ad counter
                AdCount = ShowAfterHowManyGameovers;
            }
            else
                AdCount--;
#endif
        }
    }

    //Diplays the next question is single player mode
    private IEnumerator NextQuestion()
    {
#if USE_ADMOB
        AdmobManager.instance.CheckStatus();
#endif

        //Hide the question and image display
        AltQuestionDisplay.gameObject.SetActive(false);
        QuestionDisplay.gameObject.SetActive(false);
        ImageDispay.gameObject.SetActive(false);

        Color ImageColor = ImageDispay.color;
        ImageColor.a = 0;
        ImageDispay.color = ImageColor;

        //Hide the continue and review buttons
        ContinueButton.SetActive(false);
        ReviewButton.SetActive(false);
        if (ReviewPanel)
            ReviewPanel.SetActive(false);

        //Clear List<Answers> contents
        if (AnswersAvailable.Count > 0)
            AnswersAvailable.Clear();

        //Load the answers to the answers list
        for (int q = 0; q < Questions[CurrentQuestion].Answer.Length; q++)
            AnswersAvailable.Add(Questions[CurrentQuestion].Answer[q]);

        //Shuffle the answers
        ShuffleAnswers();


        if (!OnlineMode && Questions[CurrentQuestion].Image.Length > 1)
        {
            ImageDispay.sprite = Resources.Load<Sprite>("Images/" + Questions[CurrentQuestion].Image);
            ImageDispaySc.sprite = Resources.Load<Sprite>("Images/" + Questions[CurrentQuestion].Image);
            TextOnly = false;
        }
        else if (OnlineMode && Questions[CurrentQuestion].Image.Length > 1)
        {
        TryAgain:

            string EncodedString = CalculateMD5Hash(Questions[CurrentQuestion].Image);

            string filePath = Path.Combine(Application.temporaryCachePath, EncodedString);

            //Check if the image already exists
            if (File.Exists(filePath))
            {
                //Load the image from the phone storage
                using (UnityWebRequest ImageRequest = UnityWebRequestTexture.GetTexture("file:///" + filePath))
                {
                    //Wait for the image to load successfully
                    yield return ImageRequest.SendWebRequest();

                    if (ImageRequest.isHttpError || ImageRequest.isNetworkError)
                    {
#if DEBUG
                        Debug.Log("Image Request error - " + ImageRequest.error);
#endif
                        Continue();
                        yield break;
                    }

                    Texture2D imgTex = DownloadHandlerTexture.GetContent(ImageRequest);

                    if (curSprite)
                        Destroy(curSprite);

                    //Create a sprite from the Texture2D
                    curSprite = Sprite.Create(imgTex, new Rect(0, 0, imgTex.width, imgTex.height), new Vector2(0.5f, 0), 1);

                    //Load the sprite to the image display
                    ImageDispay.sprite = curSprite;
                    ImageDispaySc.sprite = curSprite;

                    TextOnly = false;
                }
            }
            else
            {
                //Start the animation
                ImageLoadingAnimation.gameObject.SetActive(true);
                ImageLoadingAnimation.Play();

                //Set the timeout to the set time (in seconds)
                int timer = DownloadTimeout;

                while (!File.Exists(filePath))
                {
                    yield return new WaitForSeconds(1);
                    timer--;

                    for (int count = 0; count < FaultyLinks.Count; count++)
                    {
                        if (Questions[CurrentQuestion].Image.Equals(FaultyLinks[count]))
                        {
                            ImageLoadingAnimation.Stop();
                            ImageLoadingAnimation.gameObject.SetActive(false);
                            Continue();
                            yield break;
                        }
                    }

                    if (timer >= DownloadTimeout)
                    {
                        ImageLoadingAnimation.Stop();
                        ImageLoadingAnimation.gameObject.SetActive(false);
                        Continue();
                        yield break;
                    }
                }

                ImageLoadingAnimation.Stop();
                ImageLoadingAnimation.gameObject.SetActive(false);

                goto TryAgain;
            }
        }
        else
        {
            TextOnly = true;
            ImageDispay.sprite = null;
        }

        //Stop and hide the loading animation
        ImageLoadingAnimation.Stop();
        ImageLoadingAnimation.gameObject.SetActive(false);

        //Load the answers to the buttons
        for (int t = 0; t < AnswerA.Length; t++)
        {
            if (AnswersAvailable.Count > 0)
            {
                AnswerA[t].text = AnswersAvailable[0].Choices;
                AnswervA[t].text = AnswersAvailable[0].Choices;
            }

            if (AnswersAvailable.Count > 1)
            {
                AnswerB[t].text = AnswersAvailable[1].Choices;
                AnswervB[t].text = AnswersAvailable[1].Choices;
            }

            if (AnswersAvailable.Count > 2)
            {
                AnswerC[t].text = AnswersAvailable[2].Choices;
                AnswervC[t].text = AnswersAvailable[2].Choices;
            }

            if (AnswersAvailable.Count > 3)
            {
                AnswerD[t].text = AnswersAvailable[3].Choices;
                AnswervD[t].text = AnswersAvailable[3].Choices;
            }
        }

        //Display the question
        AltQuestionDisplay.text = Questions[CurrentQuestion].Question;
        QuestionDisplay.text = Questions[CurrentQuestion].Question;
        QuestionDisplaySc.text = Questions[CurrentQuestion].Question;

        //Adapt the gameUI based on whether the current question is a text only question or text+picture question
        if (TextOnly)
        {
            QuestionDisplay.gameObject.SetActive(false);
            AltQuestionDisplay.gameObject.SetActive(true);

            LongBarAnswers.SetActive(true);
            LongBarAnswersSc.SetActive(true);
            ShortBarAnswers.SetActive(false);
            ShortBarAnswersSc.SetActive(false);
            ImageDispay.gameObject.SetActive(false);
            ImageDispaySc.gameObject.SetActive(false);

            if (AnswersAvailable.Count > 2)
            {
                LongBarC.gameObject.SetActive(true);
                LongBarD.gameObject.SetActive(true);
                LongBarScC.gameObject.SetActive(true);
                LongBarScD.gameObject.SetActive(true);
            }
            else
            {
                LongBarC.gameObject.SetActive(false);
                LongBarD.gameObject.SetActive(false);
                LongBarScC.gameObject.SetActive(false);
                LongBarScD.gameObject.SetActive(false);
            }

            LongBarA.DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            LongBarB.DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            LongBarC.DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            LongBarD.DOScale(new Vector3(1, 1, 1), 0.25f);

            LongBarAnswers.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            QuestionDisplay.gameObject.SetActive(true);
            AltQuestionDisplay.gameObject.SetActive(false);

            LongBarAnswers.SetActive(false);
            LongBarAnswersSc.SetActive(false);
            ShortBarAnswers.SetActive(true);
            ShortBarAnswersSc.SetActive(true);
            ImageDispaySc.gameObject.SetActive(true);
            ImageDispay.gameObject.SetActive(true);
            ImageDispay.DOFade(1, 1f);

            if (AnswersAvailable.Count > 2)
            {
                ShortBarC.gameObject.SetActive(true);
                ShortBarD.gameObject.SetActive(true);
                ShortBarScC.gameObject.SetActive(true);
                ShortBarScD.gameObject.SetActive(true);
            }
            else
            {
                ShortBarC.gameObject.SetActive(false);
                ShortBarD.gameObject.SetActive(false);
                ShortBarScC.gameObject.SetActive(false);
                ShortBarScD.gameObject.SetActive(false);
            }

            ShortBarA.DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            ShortBarB.DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            ShortBarC.DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            ShortBarD.DOScale(new Vector3(1, 1, 1), 0.25f);

            ShortBarAnswers.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        //Stop all playing sounds
        if (SoundFX.isPlaying)
            SoundFX.Stop();

        //Set "Answered" bool to false
        Answered = false;

        //Start the timer
        Timer = StartCoroutine(UpdateTimer());

        //Get the correct answer from the available answers. We will use this to compare with the user input
        for (int q = 0; q < AnswersAvailable.Count; q++)
        {
            if (AnswersAvailable[q].Correct)
            {
                CorrectAnswer = q;
                yield break;
            }
        }
    }

    //Countdown the time left to answer a question
    IEnumerator UpdateTimer()
    {
        while (TimeLeft > 0)
        {
            if (TimeLeft <= 5)
            {
                //Play ticking sound
                if (!ticking)
                {
                    SoundFX.Stop();
                    SoundFX.PlayOneShot(TickingSound);
                }
                ticking = true;
            }

            yield return new WaitForSeconds(1);
            if (Answered || GameOver || Paused)
            {
                //Pause audiosource
                if (ticking)
                    SoundFX.Stop();

                ticking = false;
                yield break;
            }

            TimeLeft--;
            TimerText.text = TimeLeft.ToString("0") + "'";
        }

        if (!Answered && !GameOver)
        {
            //Disable the buttons to prevent further user input
            LongBarAnswers.GetComponent<CanvasGroup>().blocksRaycasts = false;
            ShortBarAnswers.GetComponent<CanvasGroup>().blocksRaycasts = false;

            //Stop any audioclip playing
            SoundFX.Stop();

            //Play Sound Effect
            SoundFX.PlayOneShot(FailSound);

            livesAnimation.Play();

#if (SUPPORTED_PLATFORM || UNITY_EDITOR)
            if (CanVibrate)
                Handheld.Vibrate();
#endif

            //Remove one life
            if (AvailableLives >= 0)
                AvailableLives--;

            //Set the correct answer button to green to display a pass
            InputCorrect();

            //Update the UI 
            LivesText.text = "Lives: " + AvailableLives.ToString();

            StartCoroutine(CaptureScreenshot());

            //Reset the ticking bool
            ticking = false;

            Answered = true;
        }
    }

    //Set up a new game
    void PrepareNewGame()
    {
        Resources.UnloadUnusedAssets();

        //Set timescale to 1
        Time.timeScale = 1;

        //Disable the loading animation
        if (Loading)
            Loading.Stop();
        if (Loading)
            Loading.gameObject.SetActive(false);

        //Reset the lives counter
        AvailableLives = LivesCount;

        //Update the number of lives available
        LivesText.text = "Lives: " + AvailableLives.ToString();

        //Re-shuffles the question list
        ShuffleQuestions();

        VideoAdPanel.SetActive(false);

#if USE_UNITYADS
        VideoCounter = AdLimit;
#endif

        //Reset the score counters
        score = 0;

        //Reset the current question count
        CurrentQuestion = 0;

        //Set the score counter text to nil
        ScoreText.text = Prefix + "0";

        //Reset buttons color to white
        for (int t = 0; t < AnswerA.Length; t++)
        {
            AnswerA[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswerB[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswerC[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            AnswerD[t].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
        }

        //Get current highscore
        highscore = PlayerPrefs.GetFloat(hScorePref, 0);

        //Disable menu Canvas
        if (MenuCanvas)
            MenuCanvas.SetActive(false);

        //Disable menu Canvas
        if (CategoryCanvas)
            CategoryCanvas.SetActive(false);

        //Disable gameover Canvas
        if (GameOverCanvas)
            GameOverCanvas.SetActive(false);

        //Disable pause Canvas
        if (PauseCanvas)
            PauseCanvas.SetActive(false);

        //Enable Game Canvas
        GameCanvas.SetActive(true);

        if (ScreenshotCanvas)
            ScreenshotCanvas.SetActive(true);

        //Set gameOver bool to false
        GameOver = false;

        //Set Paused bool to false
        Paused = false;

        //Reset Timer
        TimeLeft = TimerAmount;

        //Reset timer text
        TimerText.text = TimeLeft.ToString("0") + "'";

        //Show timers, score text and life display
        TimerText.gameObject.SetActive(true);
        //ScoreText.gameObject.SetActive(true);
        //LifeFill.transform.parent.gameObject.SetActive(true);

        //Hide the quit panel
        QuitPanel.SetActive(false);

        //Hide the answer buttons
        if (LongBarAnswers)
            LongBarAnswers.SetActive(false);
        if (ShortBarAnswers)
            ShortBarAnswers.SetActive(false);

        LongBarA.localScale = new Vector3(0, 0, 0);
        LongBarB.localScale = new Vector3(0, 0, 0);
        LongBarC.localScale = new Vector3(0, 0, 0);
        LongBarD.localScale = new Vector3(0, 0, 0);

        ShortBarA.localScale = new Vector3(0, 0, 0);
        ShortBarB.localScale = new Vector3(0, 0, 0);
        ShortBarC.localScale = new Vector3(0, 0, 0);
        ShortBarD.localScale = new Vector3(0, 0, 0);

        //Displays the first question on the list
        StartCoroutine(NextQuestion());

        if (OnlineMode)
        {
            ImageLinks.Clear();
            FaultyLinks.Clear();

            for (int a = 0; a < Questions.Count; a++)
                if (Questions[a].Image.Length > 1)
                    ImageLinks.Add(Questions[a].Image);

            StartCoroutine(CacheImages());
        }
    }

    //Call this to pause the game
    public void PauseGame()
    {
        if (GameOver)
            return;

        if (ReviewPanel.activeInHierarchy)
        {
            HideReview();
            return;
        }

        if (!EnablePausing)
        {
            QuitPanel.SetActive(true);
            return;
        }

        //Set "Paused" bool to true
        Paused = true;

        //Show Pause Canvas
        PauseCanvas.SetActive(true);
    }

    //Call this to un-pause the game
    public void ResumeGame()
    {
        if (EnablePausing && !Answered)
            Timer = StartCoroutine(UpdateTimer());

        //Set "Paused" bool to false
        Paused = false;

        //Hide Pause Canvas
        PauseCanvas.SetActive(false);
    }

    //Close the Quit Panel
    public void CloseQuitPanel()
    {
        QuitPanel.SetActive(false);
    }

    //Call this to restart the game
    public void RestartGame()
    {
        //Stop the Timer
        StopCoroutine(Timer);

        if (ticking)
            SoundFX.Stop();

        //Reset the ticking bool
        ticking = false;

        //Set unanswered bool to true to stop the timer
        Answered = true;

        //Set unanswered bool to true to stop the timer
        GameOver = true;

        //prepare a new game with shuffled questions
        PrepareNewGame();
    }

    public void ForfeitGame()
    {
#if USE_ADMOB
        AdmobManager.instance.ShowInterstitial();
#endif

        MainMenu();
    }

    //Call this to quit the game and return to main menu
    public void MainMenu()
    {
        //Set the "Paused" bool to false
        Paused = false;

        //Disable gameover Canvas
        if (GameCanvas)
            GameCanvas.SetActive(false);

        //Disable category Canvas
        if (CategoryCanvas)
            CategoryCanvas.SetActive(false);

        //Disable gameover Canvas
        if (GameOverCanvas)
            GameOverCanvas.SetActive(false);

        //Disable pause Canvas
        if (PauseCanvas)
            PauseCanvas.SetActive(false);

        //Enable menu Canvas
        if (MenuCanvas)
            MenuCanvas.SetActive(true);

        if (ScreenshotCanvas)
            ScreenshotCanvas.SetActive(false);

        //Set GameOver to true
        GameOver = true;

        //Set menu bool to true
        isMenu = true;

        //Set playing bool to false
        Playing = false;
    }

    //Call this to redirect the user to a link (mailto:name@domain.com for emails)
    public void URL(string link)
    {
        Application.OpenURL(link);
    }

#if UNITY_EDITOR
    [ContextMenu("Open Cache Folder")]
    public void OpenCacheFolder()
    {
        URL(Application.temporaryCachePath);
    }
#endif
#endif
}