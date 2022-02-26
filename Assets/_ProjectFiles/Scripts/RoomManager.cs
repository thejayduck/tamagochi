using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int RoomIndex;

    public Room[] Rooms;
}

[System.Serializable]
public class Room {
    public string Name = "New Room";
    public Sprite Sprite;
    public AudioClip Clip;
} 