using UnityEngine;
using UnityEngine.Events;

public class Treat : PickableBase
{
    PetStats stats;

    [Header("Others")]
    public Transform DogTarget;
    public float DistanceThreshold = 3f;
    public UnityEvent OnEat;

    private new void Start()
    {
        base.Start();
        stats = PetStats.Instance;
    }

    private new void OnMouseDown()
    {
        base.OnMouseDown();
    }

    private new void OnMouseDrag()
    {
        base.OnMouseDrag();

        if (Vector2.Distance(Target.transform.position, DogTarget.position) < DistanceThreshold)
            StateManager.ChangeState(TargetState);
    }

    private new void OnMouseUp()
    {
        base.OnMouseUp();

        if (Vector2.Distance(Target.transform.position, DogTarget.position) < DistanceThreshold)
        {
            stats.IncrementStat(StatEnum.Hunger, 0.1f);
            OnEat.Invoke();
        }


        Target.gameObject.transform.localPosition = Vector2.zero;
    }
}
