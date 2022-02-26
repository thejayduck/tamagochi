using UnityEngine;

enum State {
    Good,
    Decent,
    Bad,
    Dead
} 

public class PetStats : MonoBehaviour
{
    [Range (0.0f, 1.0f)]
    public float OverallHealth;

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
