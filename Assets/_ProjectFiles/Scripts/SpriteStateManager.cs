using System.Collections.Generic;
using UnityEngine;

public class SpriteStateManager : MonoBehaviour
{
    public SpriteRenderer TargetRenderer;
    public List<SpriteState> Sprites = new List<SpriteState>();
    [ReadOnly] public string CurrentState;
    public string DefaultState = "Idle";

    [Header("Time")]
    public float Cooldown = 3f;
    [ReadOnly] public float Timer = 0f;

    private void Start()
    {
        if (TargetRenderer == null)
            TargetRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Timer > 0) Timer -= Time.deltaTime;
        else
        {
            Timer = 0;

            if (CurrentState != DefaultState)
            {
                ChangeState(DefaultState, false);
            }
        }
    }

    public void ChangeState(string target, bool resetTimer = true)
    {
        print(target);

        var res = Sprites.Find(x => x.Name == target);
        CurrentState = res.Name;

        TargetRenderer.sprite = res.TargetSprite;

        if (resetTimer) Timer = Cooldown;
    }


}

[System.Serializable]
public class SpriteState
{
    public string Name = "New Sprite";
    public Sprite TargetSprite;

}