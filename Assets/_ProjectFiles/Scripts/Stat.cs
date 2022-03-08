using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stat
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float value = 1;

    public AnimationCurve ValueCurve;

    public float Value
    {
        get => value;
        set
        {
            this.value = value;
            this.value = Mathf.Clamp(this.value, 0.0f, 1.0f);
            OnChange?.Invoke();
        }
    }

    public UnityEvent OnChange;
}