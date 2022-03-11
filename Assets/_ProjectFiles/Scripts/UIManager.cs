using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBehaviour<UIManager>
{
    private PetStats stats;

    [Header("Sliders")]
    public Slider AffectionSlider;
    public Slider HungerSlider;
    public Slider CleanlinessSlider;

    public Slider ExperienceSlider;
    public TMP_Text CurExperienceText;
    public TMP_Text NextExperienceText;

    private void Start()
    {
        stats = PetStats.Instance;
    }

    public void UpdateProgressBars(int previousExperience, int nextExperience, int totalExperience)
    {
        AffectionSlider.value = stats.Affection.Value;
        HungerSlider.value = stats.Hunger.Value;
        CleanlinessSlider.value = stats.Cleanliness.Value;

        // Experience Slider
        var requiredLevelExp = nextExperience - previousExperience;
        var currentLevelExp = totalExperience - previousExperience;
        
        ExperienceSlider.value = (float)currentLevelExp / requiredLevelExp;
        CurExperienceText.SetText(currentLevelExp.ToString());
        NextExperienceText.SetText((nextExperience - totalExperience).ToString());

    }
}