using UnityEngine;

public enum States
{
    Happy,
    Okay,
    Bad,
    Awful,
    Dead
}

public class PetStats : SingletonBehaviour<PetStats>
{
    [Range(0.0f, 1.0f)]
    public float OverallHealth;
    public States State;

    public Stat Cleanliness;
    public Stat Hunger;
    public Stat Affection;

    public void CalculateOverall()
    {
        OverallHealth = (Mathf.Pow(Cleanliness.Value, 1.2f) + Mathf.Pow(Hunger.Value, 1.2f) + Affection.Value) / 3;
        if (OverallHealth > 0.85f)
        {
            State = States.Happy;
        }
        else if (OverallHealth > 0.55f)
        {
            State = States.Okay;
        }
        else if (OverallHealth > 0.35f)
        {
            State = States.Bad;
        }
        else if (OverallHealth > 0f)
        {
            State = States.Awful;
        }
        else
        {
            State = States.Dead;
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
        var _cleanliness = Cleanliness.ValueCurve.Evaluate(Cleanliness.Value);
        var _hunger = Hunger.ValueCurve.Evaluate(Hunger.Value);
        var _affection = Affection.ValueCurve.Evaluate(Affection.Value);

        Cleanliness.Value -= _cleanliness * Time.deltaTime;
        Hunger.Value -= _hunger * Time.deltaTime;
        Affection.Value -= _affection * Time.deltaTime;

        CalculateOverall();
    }

    public void Pet()
    {
        Affection.Value += 0.1f;
    }
}
