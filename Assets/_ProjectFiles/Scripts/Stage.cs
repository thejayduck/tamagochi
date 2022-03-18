using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stage
{
    public string Name;
    public StageObject Level;

    public Transform MouthTransform;
    public SpriteRenderer EyesSprite;
    public SpriteRenderer MouthSprite;

    [Header("Event")]
    public UnityEvent OnGrowth;
}