using UnityEngine;
using UnityEngine.Events;

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

public class PetStats : SingletonBehaviour<PetStats>
{
    [Header("Experience")]
    [ReadOnly] public StagesEnum Stage;

    private int maxLevel;
    public int TotalExperience;
    public int CurrentLevel;
    public int RemainingExperience;
    public AnimationCurve ExperienceCurve;
    public UnityEvent OnLevelUp;

    [Header("Stages")]
    [ReadOnly] public Stage CurrentStage;
    public Stage[] Stages;

    [Header("Health")]
    [Range(0.0f, 1.0f)]
    public float OverallHealth;
    [ReadOnly] public StatesEnum CurrentState;
    public AnimationCurve HealthCurve; // TODO implement

    private void Start()
    {
        maxLevel = (int)ExperienceCurve[ExperienceCurve.length - 1].time;
        Initialize();
    }

    private void Initialize() // TODO load data from save
    {
        SwitchStage(CurrentLevel); 
    }

    private void SwitchStage(int target)
    {
        switch (CurrentLevel){
            case 0:
                Stage = StagesEnum.Puppy;
                CurrentStage = Stages[0];
                break;
            case 1:
                Stage = StagesEnum.Adolescent;
                CurrentStage = Stages[1];
                break;
            case 2:
                Stage = StagesEnum.Adult;
                CurrentStage = Stages[2];
                break;
        }
    }

    public void CalculateLevel() 
    {

        int nextExperience = 0;
        int currentExperience = 0;

        currentExperience = (int)ExperienceCurve.Evaluate(CurrentLevel);
        nextExperience = (int)ExperienceCurve.Evaluate(CurrentLevel + 1);

        if((TotalExperience - currentExperience) < 0)
            CurrentLevel--;

        if(CurrentLevel == maxLevel)
            return;

        if((nextExperience - TotalExperience) <= 0) {
            CurrentLevel++;
            OnLevelUp.Invoke();
        }

        SwitchStage(CurrentLevel);

        CurrentLevel = Mathf.Clamp(CurrentLevel, 0, 2);
        TotalExperience = Mathf.Clamp(TotalExperience, 0, 500);
        RemainingExperience = (nextExperience - TotalExperience);
    }

    public void CalculateOverall()
    {
        OverallHealth = (Mathf.Pow(CurrentStage.Cleanliness.Value, 1.2f) + Mathf.Pow(CurrentStage.Hunger.Value, 1.2f) + CurrentStage.Affection.Value) / 3;
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

    public void SaveState()
    {
        // TODO save state
    }

    public void LoadState()
    {
        // TODO load state
    }

    private void Update()
    {
        var _cleanliness = CurrentStage.Cleanliness.ValueCurve.Evaluate(CurrentStage.Cleanliness.Value);
        var _hunger = CurrentStage.Hunger.ValueCurve.Evaluate(CurrentStage.Hunger.Value);
        var _affection = CurrentStage.Affection.ValueCurve.Evaluate(CurrentStage.Affection.Value);

        CurrentStage.Cleanliness.Value -= _cleanliness * Time.deltaTime;
        CurrentStage.Hunger.Value -= _hunger * Time.deltaTime;
        CurrentStage.Affection.Value -= _affection * Time.deltaTime;

        CalculateOverall();
        CalculateLevel();
    }

    public void Pet()
    {
        CurrentStage.Affection.Value += 0.1f;
    }
}
