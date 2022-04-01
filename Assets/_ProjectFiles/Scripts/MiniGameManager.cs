using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public int AccumulatedCoins;

    public void Play()
    {

    }

    public void Quit(string target)
    {
        SceneManager.LoadScene(target);
    }
}
