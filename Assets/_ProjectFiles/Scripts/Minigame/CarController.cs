using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float val = 0;
    Rigidbody2D rb;

    public MiniGameManager Manager;
    public Animator ExplosionAnimator;

    [Header("Audio")]
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

        if (Input.GetButtonDown("Cancel"))
        {
            Manager.OnDeath();
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.up * val * Speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);

        AudioClip clip = other.GetComponent<Obstacle>().Clip;
        Source.PlayOneShot(clip);

        switch (other.name)
        {
            case "Coin":
                Manager.AccumulatedCoins += 1;
                break;
            case "CoinDoge":
                Manager.AccumulatedCoins += 5;
                break;
            case "Amongus":
                Manager.AccumulatedCoins = 0;
                Manager.OnDeath();
                ExplosionAnimator.SetTrigger("Explode");
                Source.PlayOneShot(Explosion);
                break;
            case "Splat":
                if (Manager.AccumulatedCoins >= 10)
                {
                    Manager.AccumulatedCoins -= 10;
                }
                break;
            default:
                throw new Exception();
        }

        Manager.ScoreText.SetText($"Accumulated Coins: {Manager.AccumulatedCoins}");
    }
}
