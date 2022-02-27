using UnityEngine;

public class Wash : MonoBehaviour
{
    public GameObject Soap;
    public ParticleSystem Foam;
    public PetStats Stats;

    private Vector2 prevMouse;

    public void StartWashing()
    {
        Soap.SetActive(true);
        Foam.Pause();
        prevMouse = Input.mousePosition;
        UpdatePositions();
        Foam.Play();
    }

    public void StopWashing()
    {
        Soap.SetActive(false);
    }

    void Update()
    {
        if (Soap.activeInHierarchy)
        {
            UpdatePositions();
        }
    }

    void UpdatePositions()
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
}
