using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    private int _currentRoom;

    public SpriteRenderer Target;
    public UnityEvent OnTransition;
    public Room[] Rooms;

    public void SwitchRoom(int target)
    {
        if (_currentRoom != target)
            StartCoroutine(Transition(target));
    }

    IEnumerator Transition(int target)
    {
        _currentRoom = target;

        OnTransition.Invoke();
        
        yield return new WaitForSeconds(0.5f);
        
        Debug.LogWarning($"Loading Assets for Room {target}");
        
        yield return new WaitForSeconds(1.5f);

    }

}

[System.Serializable]
public class Room {
    public string Name = "New Room";
    public Sprite Sprite;
    public GameObject Object;
    public AudioClip Clip;
} 