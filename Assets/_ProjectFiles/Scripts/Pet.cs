using UnityEngine;

public class Pet : MonoBehaviour
{
    private PetStats stats;

    public ParticleSystem HeartParticle;

    private void Start()
    {
        stats = PetStats.Instance;
    }

    public void Caress()
    {
        stats.IncrementStat(StatEnum.Affection, 0.05f);
        HeartParticle.Play();
    }
}