using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CamScreenshot : MonoBehaviour
{
	internal static CamScreenshot instance;
	private Camera thisCamera;

	private void Awake()
	{
		thisCamera = GetComponent<Camera>();

		if(instance == null)
			instance = this;
	}

	public void CaptureScreenshot(string SSName)
	{
		StartCoroutine(Capture(SSName));
	}

	// Use this for initialization
	IEnumerator Capture(string ScreenshotName)
	{
		yield return new WaitForEndOfFrame();
		RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
		rt.useMipMap = false;
		rt.antiAliasing = 1;

		RenderTexture.active = rt;
		thisCamera.targetTexture = rt;
       
		Texture2D shot = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
           
		thisCamera.Render();
           
		shot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
		shot.Apply();
           
		thisCamera.targetTexture = null;
		RenderTexture.active = null;
		Destroy(rt);
         
		// Encode texture into PNG
		byte[] bytes = shot.EncodeToPNG();
 
		// save in memory
		string filename = ScreenShotName(ScreenshotName);

		if (!System.IO.Directory.Exists(Application.temporaryCachePath + "/Screenshots"))
			System.IO.Directory.CreateDirectory(Application.temporaryCachePath + "/Screenshots");
        
		System.IO.File.WriteAllBytes(filename, bytes);
	}
	
	public string ScreenShotName(string Name) {
		return string.Format("{0}/Screenshots/" + Name + ".png", 
			Application.temporaryCachePath);
	}
}