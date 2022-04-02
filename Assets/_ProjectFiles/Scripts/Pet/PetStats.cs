using UnityEngine;
using UnityEngine.Events;

#region ENUMS
public enum StagesEnum
{
    Puppy,
    Adolescent,
    Adult
}
public enum StatEnum
{
    Affection,
    Hunger,
    Cleanliness
}
#endregion

public class PetStats : SingletonBehaviour<PetStats>
{
    UIManager uiManager;

    public Transform ShibaObject;

    public int Money = 100;

    [Header("Experience")]
    private int maxLevel;
    [HideInInspector] public int previousExperience = 0;
    [HideInInspector] public int nextExperience = 0;

    [ReadOnly] public int CurrentLevel;
    [ReadOnly] public StagesEnum Stage;

    public float TotalExperience;
    public AnimationCurve ExperienceCurve;
    public UnityEvent OnLevelUp;

    [Header("State")]
    public SpriteStateManager HungerStateManager;
    public SpriteStateManager CleanlinessStateManager;
    public SpriteStateManager AffectionStateManager;
    public string HungerState = "Hungry";
    public float HungerThreshold = 0.5f;
    public string CleanlinessState = "Dirty";
    public float CleanlinessThreshold = 0.5f;
    public string AffectionState = "Sad";
    public float AffectionThreshold = 0.3f;

    [Header("Stages")]
    [ReadOnly] public Stage CurrentStage;

    public Stage[] Stages;

    [Header("Health")]
    public bool IsDead;

    [Range(0.0f, 1.0f)]
    public float OverallHealth;
    public Stat Affection;
    public Stat Hunger;
    public Stat Cleanliness;

    public UnityEvent OnDeathEvent;

    private void Start()
    {
        uiManager = UIManager.Instance;
        maxLevel = (int)ExperienceCurve[ExperienceCurve.length - 1].time;
        SwitchStage(CurrentLevel);

        var minigameManager = GameObject.Find("MiniGameManager");
        if (minigameManager != null)
        {
            var comp = minigameManager.GetComponent<MiniGameManager>();
            Money += comp.AccumulatedCoins;
            Destroy(minigameManager);
        }
        uiManager.UpdateMoney();
    }

    private void Update()
    {
        var _cleanliness = CurrentStage.Level.CleanlinessCurve.Evaluate(Cleanliness.Value);
        var _hunger = CurrentStage.Level.HungerCurve.Evaluate(Hunger.Value);
        var _affection = CurrentStage.Level.AffectionCurve.Evaluate(Affection.Value);

        Cleanliness.Value -= _cleanliness * Time.deltaTime;
        Hunger.Value -= _hunger * Time.deltaTime;
        Affection.Value -= _affection * Time.deltaTime;

        CalculateOverall();
        CalculateLevel();
        uiManager.UpdateProgressBars(previousExperience, nextExperience, TotalExperience, CurrentLevel + 1);

        if (Hunger.Value <= HungerThreshold)
            HungerStateManager.ChangeState(HungerState);

        if (Cleanliness.Value <= CleanlinessThreshold)
            CleanlinessStateManager.ChangeState(CleanlinessState);

        if (Affection.Value <= AffectionThreshold)
            AffectionStateManager.ChangeState(AffectionState);
    }

    private void SwitchStage(int target)
    {
        switch (CurrentLevel)
        {
            case 0:
                Stage = StagesEnum.Puppy;
                CurrentStage = Stages[0];
                break;
            case 1:
                Stage = StagesEnum.Adolescent;
                CurrentStage = Stages[1];
                uiManager.WardrobeToggle.interactable = true;
                break;
            case 2:
                Stage = StagesEnum.Adult;
                CurrentStage = Stages[2];
                uiManager.WardrobeToggle.interactable = true;
                break;
        }
        ShibaObject.localScale = Vector3.one * CurrentStage.Scale;
    }

    public void CalculateLevel()
    {
        previousExperience = (int)ExperienceCurve.Evaluate(CurrentLevel);
        nextExperience = (int)ExperienceCurve.Evaluate(CurrentLevel + 1);

        if ((TotalExperience - previousExperience) < 0)
            CurrentLevel--;

        if (CurrentLevel == maxLevel)
            return;

        if ((nextExperience - TotalExperience) <= 0)
        {
            CurrentStage.OnGrowth.Invoke();
            OnLevelUp.Invoke();
            CurrentLevel++;
        }

        SwitchStage(CurrentLevel);

        CurrentLevel = Mathf.Clamp(CurrentLevel, 0, 2);
        TotalExperience = Mathf.Clamp(TotalExperience, 0, 500);
    }

    public void CalculateOverall()
    {
        OverallHealth = (Mathf.Pow(Cleanliness.Value, 1.2f) + Mathf.Pow(Hunger.Value, 1.2f) + Affection.Value) / 3;
        if (OverallHealth <= 0 && !IsDead)
        {
            IsDead = true;
            OnDeathEvent.Invoke();
        }
    }

    public void IncrementStat(StatEnum stat, float increment)
    {
        switch (stat)
        {
            case StatEnum.Affection:
                Affection.Value += increment;
                if (Affection.Value < 0.9f)
                    TotalExperience += Affection.IncrementCurve.Evaluate(increment);
                break;
            case StatEnum.Hunger:
                Hunger.Value += increment;
                if (Hunger.Value < 0.9f)
                    TotalExperience += Hunger.IncrementCurve.Evaluate(increment);
                break;
            case StatEnum.Cleanliness:
                Cleanliness.Value += increment / 20f;
                if (Cleanliness.Value < 0.9f)
                    TotalExperience += Cleanliness.IncrementCurve.Evaluate(increment);
                break;
        }
    }

    public void AddMoney(int value)
    {
        Money += value;
    }
}
