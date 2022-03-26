using UnityEngine;

public class Pet : MonoBehaviour
{
    private PetStats stats;

    [Header("State")]
    public ParticleSystem HeartParticle;
    public SpriteStateManager StateManager;

    public float Increment = 0.05f;
    public string TargetState = "Happy";

    private void Start()
    {
        stats = PetStats.Instance;
    }

    public void Caress()
    {
        stats.IncrementStat(StatEnum.Affection, Increment);
        HeartParticle.Play();

        StateManager.ChangeState(TargetState);
    }
}