using UnityEngine;

public class CarController : MonoBehaviour
{
    public float Speed;
    float val = 0;

    [ReadOnly]
    public float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        val = 0;
        if (Input.GetKey(KeyCode.D))
            val += 1;
        if (Input.GetKey(KeyCode.A))
            val -= 1;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * val * Speed);
        //currentSpeed += (Speed * val - currentSpeed) / 2 * Time.deltaTime;
        //transform.Translate(Vector3.right * currentSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.name)
        {
            case "Coin":
                // TODO you lost all money
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
