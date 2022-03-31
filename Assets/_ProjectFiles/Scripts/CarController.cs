using UnityEngine;

public class CarController : MonoBehaviour
{
    float val = 0;
    Rigidbody2D rb;

    public float Speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        val = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.up * val * Speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.name)
        {
            case "Coin":
                // TODO you lost all money.
                break;
            case "Coin Doge":
                // TODO
                break;
            case "Amongus":
                // TODO you died and lost all money.
                break;
            case "Splat":
                // TODO you died.
                break;
        }
    }
}
