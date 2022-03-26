using UnityEngine;

public class Wash : PickableBase
{
    private PetStats stats;
    private bool isClose;

    [Header("Others")]
    public Transform DogTarget;
    public ParticleSystem Foam;
    public AnimationCurve Curve;

    public float DistanceThreshold = 3f;

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

        var mouseDelta = delta.magnitude * Time.deltaTime;
        if (Vector2.Distance(Target.transform.position, DogTarget.position) < DistanceThreshold)
        {
            stats.IncrementStat(StatEnum.Cleanliness, Curve.Evaluate(mouseDelta));
            StateManager.ChangeState(TargetState);
        }

        Foam.transform.position = curPosition;
    }

    private new void OnMouseUp()
    {
        base.OnMouseUp();

        Target.transform.localPosition = new Vector2(0.0f, 0.0f);
        Foam.transform.localPosition = new Vector2(0.0f, 0.0f);
    }
}