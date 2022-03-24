using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingletonBehaviour<UIManager>
{
    private PetStats stats;
    private TweenManager quickActionsTween;

    public Toggle WardrobeToggle;
    public TMP_Text MoneyText;

    [Header("Sliders")]
    public Slider AffectionSlider;
    public Slider HungerSlider;
    public Slider CleanlinessSlider;

    public Slider ExperienceSlider;
    public TMP_Text CurExperienceText;
    public TMP_Text NextExperienceText;

    public GameObject QuickActions;

    [Header("Wardrobe")]
    public GameObject[] PurchaseButtons;

    [Header("Other")]
    public string SceneName;
    public AudioMixer Mixer;
    
    private void Start()
    {
        stats = PetStats.Instance;
        quickActionsTween = QuickActions.GetComponent<TweenManager>();
    }

    public void ToggleQuickActions()
    {
        if (QuickActions.activeInHierarchy)
            quickActionsTween.InitTween();
        else
            QuickActions.SetActive(true);
    }

    public void UpdateProgressBars(int previousExperience, int nextExperience, float totalExperience, int nextLevel)
    {
        AffectionSlider.value = stats.Affection.Value;
        HungerSlider.value = stats.Hunger.Value;
        CleanlinessSlider.value = stats.Cleanliness.Value;

        // Experience Slider
        var requiredLevelExp = nextExperience - previousExperience;
        var currentLevelExp = (int)totalExperience - previousExperience;
        
        ExperienceSlider.value = (float)currentLevelExp / requiredLevelExp;
        CurExperienceText.SetText(currentLevelExp.ToString());
        // NextExperienceText.SetText((nextExperience - (int)totalExperience).ToString());
        NextExperienceText.SetText($"LVL{nextLevel}");
    }

    public void UpdateMoney(int money)
    {
        MoneyText.text = $"${money}";
    }

    public void ChangeMasterVolume(float value) => 
        Mixer.SetFloat("volMaster", Mathf.Log(value) * 20);
    public void ChangeSfxVolume(float value) =>
        Mixer.SetFloat("volSFX", Mathf.Log(value) * 20);
    public void ChangeMusicVolume(float value) =>
        Mixer.SetFloat("volBGM", Mathf.Log(value) * 20);

    public void LoadScene(string target){
        SceneManager.LoadScene(target); 
    }

    public void Quit(){
        Application.Quit();
    }
}