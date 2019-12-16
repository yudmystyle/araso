#if (!UNITY_ANDROID && !UNITY_IOS)
#pragma warning disable 0414
#endif

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NativeShareHandler : MonoBehaviour
{
	#if USE_DOTWEEN
	#if (UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR)

	public string ShareText = "Get QuizApp Trivia Template on @UnityAssetStore - http://u3d.as/oQJ";
	public string subject = "Share with";
	private string destination;

	private static string ImageName = "OriginalImg";

	public void ShareQuestion ()
	{
		#if UNITY_EDITOR
		if (Application.isEditor) {
			EditorUtility.DisplayDialog ("Sharing is not supported on the Editor", "Kindly deploy the project to an iOS or Android device to test this feature.", "Ok");
			return;
		}
		#endif

		destination = CamScreenshot.instance.ScreenShotName (ImageName);
		
		StartCoroutine (Share ());
	}

	private IEnumerator Share ()
	{
		yield return new WaitForEndOfFrame ();
		#if UNITY_ANDROID
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");

		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");

		intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject> ("setType", "image/png");

		intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), ShareText);
		intentObject.Call<AndroidJavaObject> ("addFlags", intentObject.GetStatic<int> ("FLAG_GRANT_READ_URI_PERMISSION"));

		AndroidJavaClass fileProviderClass = new AndroidJavaClass ("android.support.v4.content.FileProvider");
		AndroidJavaObject unityContext = currentActivity.Call<AndroidJavaObject> ("getApplicationContext");
		//AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

		string packageName = unityContext.Call<string> ("getPackageName");
		string authority = packageName + ".provider";

		AndroidJavaObject fileObject = new AndroidJavaObject ("java.io.File", destination);

		if (fileObject.Call<bool> ("exists")) {
			AndroidJavaObject uriObj = fileProviderClass.CallStatic<AndroidJavaObject> ("getUriForFile", unityContext, authority, fileObject);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), uriObj);
		}

		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject> ("createChooser", intentObject, subject);
		currentActivity.Call ("startActivity", jChooser);
		#elif UNITY_IOS
	CallSocialShareAdvanced(ShareText, subject, "", destination);
		#else
	Debug.Log("No sharing set up for this platform.");
		#endif
	}

	#if UNITY_IOS
	public struct ConfigStruct
	{
	public string title;
	public string message;
	}

	[DllImport("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);

	public struct SocialSharingStruct
	{
	public string text;
	public string url;
	public string image;
	public string subject;
	}

	[DllImport("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);

	public static void CallSocialShare(string title, string message)
	{
	ConfigStruct conf = new ConfigStruct();
	conf.title = title;
	conf.message = message;
	showAlertMessage(ref conf);
	}


	public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
	{
	SocialSharingStruct conf = new SocialSharingStruct();
	conf.text = defaultTxt;
	conf.url = url;
	conf.image = img;
	conf.subject = subject;

	showSocialSharing(ref conf);
	}
	#endif
	#endif
	#endif
}