using System;
using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public MiniGameManager manager;
    public Animator explosionAnimator;
    public AudioSource explosionSFX;


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

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);

        switch (other.name)
        {
            case "Coin":
                manager.AccumulatedCoins += 1;
                break;
            case "CoinDoge":
                manager.AccumulatedCoins += 5;
                break;
            case "Amongus":
                manager.AccumulatedCoins = -999;
                // TODO Explode
                break;
            case "Splat":
                if (manager.AccumulatedCoins >= 10)
                {
                    manager.AccumulatedCoins -= 10;
                }
                explosionAnimator.gameObject.SetActive(true);
                explosionAnimator.SetTrigger("Explode");
                explosionSFX.Play();
                yield return new WaitUntil(() => explosionAnimator.GetCurrentAnimatorStateInfo(0).IsName("Explosion"));
                yield return new WaitUntil(() => !explosionAnimator.GetCurrentAnimatorStateInfo(0).IsName("Explosion") && !explosionSFX.isPlaying);
                explosionAnimator.gameObject.SetActive(false);
                break;
            default:
                throw new Exception();
        }
    }
}
