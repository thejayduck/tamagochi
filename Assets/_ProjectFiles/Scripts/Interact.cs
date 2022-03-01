using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public UnityEvent Events;

    private void OnMouseDown()
    {
        Events.Invoke();
    }
}