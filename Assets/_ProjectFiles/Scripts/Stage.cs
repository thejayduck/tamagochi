using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stage
{
    public string Name;

    [Header("Stats")]
    public AnimationCurve AffectionCurve;
    public AnimationCurve HungerCurve;
    public AnimationCurve CleanlinessCurve;

    [Header("Event")]
    public UnityEvent OnGrowth;
}