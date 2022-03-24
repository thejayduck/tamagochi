using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    private PetStats stats;

    public UnityEvent Events;

    private void Start()
    {
        stats = PetStats.Instance;
    }

    private void OnMouseDown()
    {
        Events.Invoke();
    }

    public void StartCrash()
    {
        StartCoroutine(CrashOnFinish());
    }

    public IEnumerator CrashOnFinish()
    {
        AudioSource source = GameObject.Find("Amongus Source").GetComponent<AudioSource>();
        
        yield return new WaitUntil(() => source.isPlaying);

        print("Amongus!");

// #if !UNITY_EDITOR
//             Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
// #endif
    }
}