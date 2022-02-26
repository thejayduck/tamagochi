using UnityEngine;

[System.Serializable]
public class Stat{

    [Range (0.0f, 1.0f)]
    public float Value = 1;
    public float IncreaseRate;
    public float DecreaseRate;
}