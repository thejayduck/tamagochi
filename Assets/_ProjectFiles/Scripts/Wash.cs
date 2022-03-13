using UnityEngine;

public class Wash : PickableBase
{
    private PetStats stats;
    private bool isClose;
    
    [Header("Others")]
    public Transform DogTarget;
    public ParticleSystem Foam;
    public AnimationCurve Curve;

    private new void Start()
    {
        base.Start();
        stats = PetStats.Instance;
    }

    private new void OnMouseDown()
    {
        base.OnMouseDown();

        Foam.Pause();
        Foam.Play();
    }

    private new void OnMouseDrag()
    {
        base.OnMouseDrag();

        // if (Vector2.Distance(Target.transform.position, DogTarget.position) < 3f)
        // {
        //     if (!isClose)
        //         Foam.Play();

        //     isClose = true;
        // }
        // else {
        //     print("stopped");
        //     Foam.Stop();
        //     isClose = false;
        // }

        var mouseDelta = delta.magnitude * Time.deltaTime;
        stats.IncrementStat(StatEnum.Cleanliness, mouseDelta);

        Foam.transform.position = curPosition;
    }

    private new void OnMouseUp()
    {
        base.OnMouseUp();

        Target.transform.localPosition = new Vector2(0.0f, 0.0f); 
        Foam.transform.localPosition = new Vector2(0.0f, 0.0f);
    }
}