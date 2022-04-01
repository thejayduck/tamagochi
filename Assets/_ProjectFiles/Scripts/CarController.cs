using System;
using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float val = 0;
    Rigidbody2D rb;

    public MiniGameManager Manager;
    public Animator ExplosionAnimator;
    public AudioSource Source;
    public AudioClip Explosion;
    public AudioClip TireScreech;

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

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);

        switch (other.name)
        {
            case "Coin":
                Manager.AccumulatedCoins += 1;
                break;
            case "CoinDoge":
                Manager.AccumulatedCoins += 5;
                break;
            case "Amongus":
                Manager.AccumulatedCoins = -999;
                // TODO Explode
                break;
            case "Splat":

                if (Manager.AccumulatedCoins >= 10)
                {
                    Manager.AccumulatedCoins -= 10;
                }

                ExplosionAnimator.SetTrigger("Explode");

                Source.PlayOneShot(Explosion);
                yield return null;

                break;
            default:
                throw new Exception();
        }
    }
}
