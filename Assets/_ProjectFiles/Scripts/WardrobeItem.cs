using UnityEngine;

[CreateAssetMenu(fileName = "New Wardrobe Item", menuName = "Custom/Wardrobe Item")]
public class WardrobeItem : ScriptableObject
{
    public Sprite Sprite;
    public bool Locked = true;
    public int Price;
}