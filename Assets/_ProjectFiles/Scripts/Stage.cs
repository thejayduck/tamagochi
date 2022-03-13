using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stage
{
    public string Name;
    public StageObject Level;

    [Header("Event")]
    public UnityEvent OnGrowth;
}