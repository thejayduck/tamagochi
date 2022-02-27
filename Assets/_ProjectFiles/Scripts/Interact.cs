using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public UnityEvent Events;

    private void OnMouseDown()
    {
        Events.Invoke();
    }

    public void Caress(ParticleSystem particle){
        GameObject _particle = Instantiate(particle, this.transform.position, Quaternion.identity).gameObject;
        Destroy(_particle, 1.1f);
    }
}
