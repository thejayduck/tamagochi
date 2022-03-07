using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector2? _delta;
    bool hitLastFrame = false;
    bool touchedLeft = false;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    public PetStats Stats;

    void Update()
    {
        var point = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var delta = point - (Vector2)transform.position;
        var hit = delta.magnitude <= 0.5f;
        if (Input.GetMouseButton(0))
        {
            if (hit || hitLastFrame)
            {
                rb.velocity = Vector2.zero;
                transform.position = point;
                _delta = delta;
            }
        }
        else if (_delta != null)
        {
            var velocity = _delta.Value / Time.deltaTime;
            rb.velocity = velocity.normalized * Mathf.Min(velocity.magnitude, 20.0f);
            _delta = null;
        }
        hitLastFrame = hit;

        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        var ballRadiusInScreenCoordinates = Mathf.Abs(Camera.main.WorldToScreenPoint(Vector3.one * 0.5f).x - Camera.main.WorldToScreenPoint(Vector3.zero).x);
        if ((screenPosition.y > Screen.height - ballRadiusInScreenCoordinates) || (screenPosition.y < ballRadiusInScreenCoordinates) || (screenPosition.x > Screen.width - ballRadiusInScreenCoordinates) || (screenPosition.x < ballRadiusInScreenCoordinates))
        {
            var velocity = rb.velocity;
            if (screenPosition.x > Screen.width - ballRadiusInScreenCoordinates)
            {
                velocity.x *= -0.85f;
                velocity.y *= 0.85f;
                rb.AddTorque(velocity.y * 5f);
                screenPosition.x = Screen.width - ballRadiusInScreenCoordinates;
                if (touchedLeft)
                {
                    Stats.Affection.Value += 0.1f;
                }
                touchedLeft = false;
            }
            else if (screenPosition.x < ballRadiusInScreenCoordinates)
            {
                velocity.x *= -0.85f;
                velocity.y *= 0.85f;
                rb.AddTorque(velocity.y * 5f);
                screenPosition.x = ballRadiusInScreenCoordinates;
                touchedLeft = true;
            }

            if (screenPosition.y > Screen.height - ballRadiusInScreenCoordinates)
            {
                velocity.x *= 0.85f;
                velocity.y *= -0.85f;
                rb.AddTorque(-velocity.x * 5f);
                screenPosition.y = Screen.height - ballRadiusInScreenCoordinates;
            }
            else if (screenPosition.y < ballRadiusInScreenCoordinates)
            {
                velocity.x *= 0.85f;
                velocity.y *= -0.85f;
                rb.AddTorque(-velocity.x * 5f);
                screenPosition.y = ballRadiusInScreenCoordinates;
            }

            var newWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            rb.velocity = velocity;
            transform.position = new Vector2(newWorldPosition.x, newWorldPosition.y);
        }
    }

    void ApplyFoce()
    {
        // TODO implement
    }
}
