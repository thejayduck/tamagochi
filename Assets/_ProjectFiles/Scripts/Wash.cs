using UnityEngine;

public class Wash : MonoBehaviour
{
    private PetStats Stats;
    
    public SpriteRenderer Target;
    public GameObject Soap;
    public ParticleSystem Foam;

    private Vector2 prevMouse;

    private void Start()
    {
        Stats = PetStats.Instance;
    }

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
