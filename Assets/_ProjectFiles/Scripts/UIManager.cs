using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBehaviour<UIManager>
{
    private PetStats stats;
    private TweenManager quickActionsTween;

    [Header("Sliders")]
    public Slider AffectionSlider;
    public Slider HungerSlider;
    public Slider CleanlinessSlider;

    public Slider ExperienceSlider;
    public TMP_Text CurExperienceText;
    public TMP_Text NextExperienceText;

    public GameObject QuickActions;

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

    public void UpdateProgressBars(int previousExperience, int nextExperience, float totalExperience, string stage)
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
        NextExperienceText.SetText(stage);

    }
}