using UnityEngine;
using UnityEngine.Events;

public class Fondle : MonoBehaviour
{
    public ParticleSystem particle;
    public UnityEvent Events;

    private void OnMouseDown()
    {
            print("yo");
            GameObject _particle = Instantiate(particle, this.transform.position, Quaternion.identity).gameObject;
            Destroy(_particle, 1.1f);
            Events.Invoke();
    }

}
