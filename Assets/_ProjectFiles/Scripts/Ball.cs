using UnityEngine;

public class Ball : PickableBase
{
    private PetStats stats;
    
    private Rigidbody2D rb;

    public AnimationCurve Curve;
    public AudioClip BounceClip;

    private new void Start()
    {
        base.Start();

        stats = PetStats.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    private new void OnMouseDown()
    {
        base.OnMouseDown();
    }

    private new void OnMouseDrag()
    {
        base.OnMouseDrag();

        rb.velocity = Vector2.zero;
    }

    private new void OnMouseUp() 
    {
        base.OnMouseUp();

        velocity = delta.normalized / Time.deltaTime;
        rb.AddForce(velocity * delta.magnitude, ForceMode2D.Impulse);
    }

    void Update()
    {
        var screenPosition = Camera.main.WorldToScreenPoint(Target.transform.position);
        var ballRadius = Mathf.Abs(Camera.main.WorldToScreenPoint(Vector3.one * 0.8f).x - Camera.main.WorldToScreenPoint(Vector3.zero).x);
        if (
            (screenPosition.y > Screen.height - ballRadius) 
            || (screenPosition.y < ballRadius) 
            || (screenPosition.x > Screen.width - ballRadius) 
            || (screenPosition.x < ballRadius)
        )
        {
            var velocity = rb.velocity;
            if (screenPosition.x > Screen.width - ballRadius || screenPosition.x < ballRadius)
            {
                velocity.x *= -0.85f;
                velocity.y *= 0.85f;
                rb.AddTorque(velocity.y * 5f);
                screenPosition.x = screenPosition.x < ballRadius ? ballRadius : Screen.width - ballRadius;
            }

            if (screenPosition.y > Screen.height - ballRadius || screenPosition.y < ballRadius)
            {
                velocity.x *= 0.85f;
                velocity.y *= -0.85f;
                rb.AddTorque(-velocity.x * 5f);
                screenPosition.y = screenPosition.y < ballRadius ? ballRadius : Screen.height - ballRadius;
            }


            if(rb.velocity.magnitude >= 2.5f)
            {
                source.PlayOneShot(BounceClip);
            }

            var newWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            rb.velocity = velocity;
            Target.transform.position = new Vector2(newWorldPosition.x, newWorldPosition.y);
        }
        print(rb.velocity.magnitude);
        if (rb.velocity.magnitude > 10f)
        {
            stats.IncrementStat(StatEnum.Affection, Curve.Evaluate(rb.velocity.magnitude));
        }
    }
}
