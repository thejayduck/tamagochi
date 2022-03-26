using UnityEngine;
using UnityEngine.Events;

#region ENUMS
public enum StatesEnum
{
    Happy,
    Okay,
    Bad,
    Awful,
    Dead
}
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
    public string HungerState = "Hungry";
    public float HungerThreshold = 0.5f;
    public string CleanlinessState = "Dirty";
    public float CleanlinessThreshold = 0.5f;

    [Header("Stages")]
    [ReadOnly] public Stage CurrentStage;

    public Stage[] Stages;

    [Header("Health")]
    [ReadOnly] public StatesEnum CurrentState;

    [Range(0.0f, 1.0f)]
    public float OverallHealth;
    public AnimationCurve HealthCurve; // TODO implement
    public Stat Affection;
    public Stat Hunger;
    public Stat Cleanliness;

    private void Start()
    {
        uiManager = UIManager.Instance;
        maxLevel = (int)ExperienceCurve[ExperienceCurve.length - 1].time;
        Initialize();
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
    }

    public void Initialize() // TODO load data from save
    {
        SwitchStage(CurrentLevel);
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
        if (OverallHealth > 0.85f)
        {
            CurrentState = StatesEnum.Happy;
        }
        else if (OverallHealth > 0.55f)
        {
            CurrentState = StatesEnum.Okay;
        }
        else if (OverallHealth > 0.35f)
        {
            CurrentState = StatesEnum.Bad;
        }
        else if (OverallHealth > 0f)
        {
            CurrentState = StatesEnum.Awful;
        }
        else
        {
            CurrentState = StatesEnum.Dead;
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
}
