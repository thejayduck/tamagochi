using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float Speed = 8.0f;

    [Header("Audio")]
    public AudioClip Clip;

    void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}
