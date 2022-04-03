using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class ScreenshotManager : MonoBehaviour
{
    private static string SCREENSHOT_LOC => Application.dataPath + "/Screenshots/Screenshot";

    public UnityEvent OnStartScreenshot;
    public UnityEvent OnEndScreenshot;

    private void Awake()
    {
        if (!Directory.Exists(SCREENSHOT_LOC))
        {
            Directory.CreateDirectory(Directory.GetParent(SCREENSHOT_LOC).ToString());
        }
    }

    public void GrabScreen()
    {
        StartCoroutine(StartScreenshot());
    }
    IEnumerator StartScreenshot()
    {
        OnStartScreenshot.Invoke();
        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot(SCREENSHOT_LOC + GetCh() + GetCh() + GetCh() + "_" + Screen.width + "x" + Screen.height + ".png", 1);
        yield return new WaitForEndOfFrame();
        OnEndScreenshot.Invoke();
    }


    public static char GetCh()
    {
        return (char)UnityEngine.Random.Range('A', 'Z');
    }
}
