using UnityEngine;

public class WardrobeManager : SingletonBehaviour<WardrobeManager>
{
    public Wardrobe Hats;
    public Wardrobe Glasses;
    public Wardrobe Dress;
    public Wardrobe Accessories;

    public void Initialize(SaveObject save) // TODO load data from save
    {
        Hats.Index = save.HatIndex;
        Glasses.Index = save.GlassesIndex;
        Dress.Index = save.DressIndex;
        Accessories.Index = save.AccessoryIndex;
    }

    public void Next(int target)
    {
        var wardrobe = GetWardrobe(target);
        Set(wardrobe, wardrobe.Index + 1);
    }

    public void Prev(int target)
    {
        var wardrobe = GetWardrobe(target);
        Set(wardrobe, wardrobe.Index - 1);
    }

    public void Set(Wardrobe wardrobe, int value)
    {
        wardrobe.Index = Repeat(value, wardrobe.Options.Length);
        wardrobe.Target.sprite = wardrobe.Options[wardrobe.Index];
    }

    public Wardrobe GetWardrobe(int target)
    => target switch
    {
        0 => Hats,
        1 => Glasses,
        2 => Dress,
        3 => Accessories,
        _ => throw new System.ArgumentException(),
    };

    int Repeat(int v, int max) => (v % max + max) % max;
}

[System.Serializable]
public class Wardrobe
{

    public int Index;

    public SpriteRenderer Target;
    public Sprite[] Options;
}