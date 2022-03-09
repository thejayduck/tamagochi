using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    private int _currentRoom;

    public Room[] Rooms;
    public UnityEvent OnTransition;
    public AudioSource Source;

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

        foreach (Room i in Rooms)
            i.Object.SetActive(false);

        Rooms[target].Object.SetActive(true);
        // Transition Audio
        float _baseVol = Source.volume;

        LeanTween.value(_baseVol, 0, 0.5f).setOnUpdate((float var) =>
        {
            Source.volume = var;

        }).setOnComplete(() =>
        {
            Source.clip = Rooms[target].Clip;
            Source.Play();
            LeanTween.value(0, 1, 0.5f).setOnUpdate((float var) => { Source.volume = var; });
        });

        Debug.LogWarning($"Loading Assets for Room {target}");

        yield return new WaitForSeconds(1.5f);

    }

}

[System.Serializable]
public class Room
{
    public string Name = "New Room";
    public GameObject Object;
    public AudioClip Clip;
}