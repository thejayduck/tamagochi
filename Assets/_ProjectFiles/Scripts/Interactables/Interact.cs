using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    private PetStats stats;

    [Header("Highlight")]
    public Color32 DefaultColor = new Color32(255, 255, 255, 255);
    public Color32 HoverColor = new Color32(154, 154, 154, 255);
    public SpriteRenderer TargetSprite;

    public UnityEvent Events;

    private void Start()
    {
        stats = PetStats.Instance;
    }

    private void OnMouseEnter()
    {
        if (TargetSprite)
            TargetSprite.color = HoverColor;
    }
    private void OnMouseExit()
    {
        if (TargetSprite)
            TargetSprite.color = DefaultColor;
    }

    private void OnMouseDown()
    {
        Events.Invoke();
    }

    public void StartEvent(Animator animator)
    {
        animator.SetTrigger("Start");
    }

    // public IEnumerator CrashOnFinish()
    // {
    //     AudioSource source = GameObject.Find("Amongus Source").GetComponent<AudioSource>();

    //     yield return new WaitUntil(() => source.isPlaying);

    //     print("Amongus!");

    //     // #if !UNITY_EDITOR
    //     //             Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
    //     // #endif
    // }
}