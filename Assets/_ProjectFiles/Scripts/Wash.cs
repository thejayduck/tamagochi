using UnityEngine;

public class Wash : MonoBehaviour
{
    private PetStats Stats;
    private Vector2 prevMouse;
    
    public SpriteRenderer Target;
    public GameObject Soap;
    public ParticleSystem Foam;

    private void Start()
    {
        Stats = PetStats.Instance;
    }

    private void OnMouseDown()
    {
        Foam.Pause();
        prevMouse = Input.mousePosition;
        Foam.Play();
    }

    private void OnMouseDrag()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        var mouseDelta = ((Vector2)mousePos - prevMouse).magnitude * Time.deltaTime;
        Stats.Cleanliness.Value += mouseDelta / 50f;

        Soap.transform.position = worldPos;
        Foam.transform.position = worldPos;
        prevMouse = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        Soap.transform.localPosition = new Vector2(0.0f, 0.0f); 
    }
}