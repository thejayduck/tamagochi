using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WardrobeManager : SingletonBehaviour<WardrobeManager>
{
    private UIManager uiManager;
    private PetStats stats; 

    [ReadOnly] public List<Wardrobe> wardrobes;

    public Wardrobe Hats;
    public Wardrobe Glasses;
    public Wardrobe Dress;
    public Wardrobe Accessories;

    private void Awake()
    {
        wardrobes = new List<Wardrobe>{Hats, Glasses, Dress, Accessories}; 

        uiManager = UIManager.Instance; 
        stats = PetStats.Instance;
    }

    public void Initialize()
    {
        Hats.PreviewIndex = Hats.Index;
        Glasses.PreviewIndex = Glasses.Index;
        Dress.PreviewIndex = Dress.Index;
        Accessories.PreviewIndex = Accessories.Index;
    }

    public void Next(int target)
    {
        var wardrobe = wardrobes[target];
        Set(wardrobe, wardrobe.PreviewIndex + 1);
    }

    public void Prev(int target)
    {
        var wardrobe = wardrobes[target];
        Set(wardrobe, wardrobe.PreviewIndex - 1);
    }

    public void Set(Wardrobe wardrobe, int value)
    {
        wardrobe.PreviewIndex = Repeat(value, wardrobe.Items.Length);
        TryApplyOrPreview(wardrobe);
    }

    public void Purchase(int target)
    {
        var wardrobe = wardrobes[target];
        var item = wardrobes[target].Items[wardrobes[target].PreviewIndex];

        if(stats.Money >= item.Price)
        {
            Debug.Log($"Purchased: {item.name}");
            stats.Money -= item.Price;
            item.Locked = false;
            Set(wardrobe, wardrobe.PreviewIndex);
        }
    }

    public void Apply()
    {
        TryApplyOrPreview(Hats);
        TryApplyOrPreview(Glasses);
        TryApplyOrPreview(Dress);
        TryApplyOrPreview(Accessories);
    }

    public void TryApplyOrPreview(Wardrobe wardrobe)
    {
        var currentItem = wardrobe.Items[wardrobe.PreviewIndex];
        var uiElement = uiManager.PurchaseButtons[wardrobes.IndexOf(wardrobe)];

        uiElement
            .SetActive(currentItem.Locked);
        uiElement.transform
            .GetChild(0)
            .GetComponent<TMP_Text>()
            .text = $"Buy - {currentItem.Price}";

        if (!currentItem.Locked)
        {
            wardrobe.Index = wardrobe.PreviewIndex;
        }

        wardrobe.Target.sprite = currentItem.Sprite;
    }

    int Repeat(int v, int max) => (v % max + max) % max;
}

[System.Serializable]
public class Wardrobe
{
    public int Index;
    public int PreviewIndex;

    public SpriteRenderer Target;
    public WardrobeItem[] Items;
}