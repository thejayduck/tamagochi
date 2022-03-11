using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stage
{
    public string Name;

    [Header("Stats")]
    public Stat Affection;
    public Stat Hunger;
    public Stat Cleanliness;

    [Header("Event")]
    public UnityEvent OnGrowth;
}