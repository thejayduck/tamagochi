using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string SceneName;
    public AudioMixer Mixer;

    public void ChangeMasterVolume(float value) => 
        Mixer.SetFloat("volMaster", Mathf.Log(value) * 20);
    public void ChangeSfxVolume(float value) =>
        Mixer.SetFloat("volSFX", Mathf.Log(value) * 20);
    public void ChangeMusicVolume(float value) =>
        Mixer.SetFloat("volBGM", Mathf.Log(value) * 20);

    public void Play(){
        SceneManager.LoadScene(SceneName); 
    }

    public void Quit(){
        Application.Quit();
    }
}