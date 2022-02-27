using UnityEngine;

public class Wash : MonoBehaviour
{
    public GameObject Prefab;

    private Transform target;
    private Vector3 mousePos;
    private Vector3 worldPos;

    public void StartWashing() {
        target = Instantiate(Prefab, Vector3.zero, Quaternion.identity).transform;
    }

    public void StopWashing() {
        Destroy(target.gameObject);
        target = null;
    }

    void Update()
    {
        if (target != null) {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            target.position = worldPos;
        }
    }
}
