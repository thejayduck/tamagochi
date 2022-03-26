using UnityEngine;

[CreateAssetMenu(fileName = "New Wardrobe Item", menuName = "Custom/Wardrobe Item")]
public class WardrobeItem : ScriptableObject
{
    public string Name = "New Item";

    public Sprite Sprite;
    public bool Locked = true;
    public int Price;
}