using UnityEngine;

public enum States {
    Happy,
    Okay,
    Bad,
    Awful,
    Dead
} 

public class PetStats : MonoBehaviour
{
    [Range (0.0f, 1.0f)]
    public float OverallHealth;
    public States State;

    public Stat Cleanliness;
    public Stat Hunger;
    public Stat Affection;

    public void CalculateOverall(){
        // TODO calculate `overall health` and change the `pet state`
    }

    public void SaveState(){
        // TODO save state
    }

    public void LoadState(){
        // TODO load state
    }

}
