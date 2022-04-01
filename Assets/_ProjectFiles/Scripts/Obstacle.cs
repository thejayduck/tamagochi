using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float Speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}
