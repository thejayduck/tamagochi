using UnityEngine;
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Vector2 offset;
    private Vector2 curScreenPoint;
    private Vector2 curPosition;
    private Vector2 velocity;
    private Vector2 delta;
    private bool dragging = false;

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
    }

    private void OnMouseDrag()
    {
        curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        curPosition = (Vector2)Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        delta = curPosition - (Vector2)transform.position;
        transform.position = curPosition;
        rb.velocity = Vector2.zero;

        dragging = true;
    }

    private void OnMouseUp() 
    {
        velocity = delta.normalized / Time.deltaTime;
        rb.AddForce(velocity * delta.magnitude, ForceMode2D.Impulse);

        dragging = false;
    }

    void Update()
    {
        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
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

            var newWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            rb.velocity = velocity;
            transform.position = new Vector2(newWorldPosition.x, newWorldPosition.y);
        }
    }
}
