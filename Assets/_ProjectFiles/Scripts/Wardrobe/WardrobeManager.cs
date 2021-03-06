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

    [Header("Visuals")]
    public Color32 AvailableColor;
    public Color32 UnavailableColor;
    public ParticleSystem PurchaseParticle;

    [Header("Audio")]
    public AudioSource Source;
    public AudioClip PurchaseSFX;
    public AudioClip FailSFX;

    private void Awake()
    {
        wardrobes = new List<Wardrobe> { Hats, Glasses, Dress, Accessories };

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

        if (stats.Money >= item.Price)
        {
            Debug.Log($"Purchased: {item.name}");

            stats.Money -= item.Price;
            wardrobe.PurchasedItems.Add(item.Name);
            Set(wardrobe, wardrobe.PreviewIndex);
            Source.PlayOneShot(PurchaseSFX);

            PurchaseParticle.Play();

            uiManager.UpdateMoney();
        }
        else
            Source.PlayOneShot(FailSFX);
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

        var locked = !wardrobe.PurchasedItems.Contains(currentItem.Name) && currentItem.Price != 0;

        uiElement
            .SetActive(locked);
        uiElement.transform
            .GetChild(0)
            .GetComponent<TMP_Text>()
            .text = $"{currentItem.Name}\nBuy [${currentItem.Price}]";

        wardrobe.Target.color = locked ? UnavailableColor : AvailableColor;

        if (!locked)
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
    public List<string> PurchasedItems = new List<string>();

    [ReadOnly] public int Index;
    [ReadOnly] public int PreviewIndex;

    public SpriteRenderer Target;
    public WardrobeItem[] Items;
}