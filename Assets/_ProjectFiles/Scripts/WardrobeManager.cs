using UnityEngine;

public class WardrobeManager : MonoBehaviour
{
    public Wardrobe Hats;
    public Wardrobe Glasses;
    public Wardrobe Dress;

    public void Wear (int target) {
        switch (target) {
            case 0:
                ApplyClothing(Hats);
                break;
            case 1:
                ApplyClothing(Glasses);
                break;
            case 2:
                ApplyClothing(Dress);
                break;
        }
    }
    
    void ApplyClothing(Wardrobe target) {
        target.Index += 1;
        target.Index = target.Index > target.Options.Length - 1 ? 0 : target.Index;
        target.Target.sprite = target.Options[target.Index];
    }
}

[System.Serializable]
public class Wardrobe {

    public int Index;

    public SpriteRenderer Target;
    public Sprite[] Options;
} 