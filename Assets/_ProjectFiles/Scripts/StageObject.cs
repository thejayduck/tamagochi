using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Custom/New Stage")]
public class StageObject : ScriptableObject
{
    [Header("Stats")]
    public AnimationCurve AffectionCurve;
    public AnimationCurve HungerCurve;
    public AnimationCurve CleanlinessCurve;   
}
