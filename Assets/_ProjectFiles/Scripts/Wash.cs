using UnityEngine;

public class Wash : MonoBehaviour
{
    public GameObject Soap;
    public ParticleSystem Foam;

    private Vector3 mousePos;
    private Vector3 worldPos;

    public void StartWashing()
    {
        Soap.SetActive(true);
        Foam.Pause();
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
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Soap.transform.position = worldPos;
        Foam.transform.position = worldPos;
    }
}
