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

    public float Scale = 1.0f;

    [Header("Event")]
    public UnityEvent OnGrowth;
}