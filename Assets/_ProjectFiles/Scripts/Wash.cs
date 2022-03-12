using UnityEngine;

public class Wash : PickableBase
{
    private PetStats Stats;
    public ParticleSystem Foam;
    public AnimationCurve Curve;

    private new void Start()
    {
        base.Start();
        Stats = PetStats.Instance;
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
        Stats.Cleanliness.Value += Curve.Evaluate(mouseDelta);

        Foam.transform.position = curPosition;
    }

    private new void OnMouseUp()
    {
        base.OnMouseUp();

        Target.transform.localPosition = new Vector2(0.0f, 0.0f); 
    }
}