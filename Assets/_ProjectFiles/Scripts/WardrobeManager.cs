using UnityEngine;

public class WardrobeManager : MonoBehaviour
{
    public Wardrobe Hats;
    public Wardrobe Glasses;
    public Wardrobe Dress;
    public Wardrobe Accessories;

    public void Next(int target) {
        var wardrobe = GetWardrobe(target);
        wardrobe.Index = Repeat(wardrobe.Index + 1, wardrobe.Options.Length);
        wardrobe.Target.sprite = wardrobe.Options[wardrobe.Index];
    }

    public void Prev(int target) {
        var wardrobe = GetWardrobe(target);
        wardrobe.Index = Repeat(wardrobe.Index - 1, wardrobe.Options.Length);
        wardrobe.Target.sprite = wardrobe.Options[wardrobe.Index];
    }

    public Wardrobe GetWardrobe(int target) {
        switch (target) {
            case 0:
                return Hats;
            case 1:
                return Glasses;
            case 2:
                return Dress;
            case 3:
                return Accessories;
            default:
                throw new System.ArgumentException();
        }
    }

    int Repeat(int v, int max) => (v % max + max) % max;
}

[System.Serializable]
public class Wardrobe {

    public int Index;

    public SpriteRenderer Target;
    public Sprite[] Options;
} 