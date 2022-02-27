using UnityEngine;

public class Wash : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 worldPos;

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = worldPos;
    }
}
