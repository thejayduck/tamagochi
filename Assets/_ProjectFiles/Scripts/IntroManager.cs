using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer Player;
    public string MainMenuScene = "MainMenuScene";

    private void Start()
    {
        StartCoroutine(PlayVideo());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.Stop();
        }
    }

    IEnumerator PlayVideo()
    {
        yield return new WaitUntil(() => !Player.isPlaying);

        SceneManager.LoadScene(MainMenuScene);
    }
}
