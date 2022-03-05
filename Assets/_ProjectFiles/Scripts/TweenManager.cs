using UnityEngine;
using UnityEngine.Events;

public enum AnimTypes {
    Move,
    Scale,
    ScaleX,
    ScaleY,
    Fade
}

public class TweenManager : MonoBehaviour
{

    private LTDescr _tweenObject;

    public GameObject Target;

    [Header("Properties")]
    public AnimTypes AnimationType;

    [Space]
    public float Duration = 0.5f;
    public float Delay;

    [Space]
    public bool PlayOnStart;
    public bool PlayOnEnable;
    public bool Loop;
    public bool PingPong;
    public bool Offset;

    [Space]
    public Vector3 From; // Offset
    public Vector3 To;

    [Space]
    public LeanTweenType EaseType;

    [Header("Events")]
    public UnityEvent OnStart;
    public UnityEvent OnUpdate;
    public UnityEvent OnComplete;

    private void OnEnable()
    {
        if (PlayOnEnable)
            InitTween();
    }

    private void Start()
    {
        if (PlayOnStart)
            InitTween();
    }

    public void InitTween()
    {
        if (Target == null)
            Target = this.gameObject;

        switch (AnimationType) {
            case AnimTypes.Move:
                //TODO Implement
                break;
            case AnimTypes.Scale:
                Scale();
                break;
            case AnimTypes.ScaleX:
                //TODO Implement
                break;
            case AnimTypes.ScaleY:
                //TODO Implement
                break;
            case AnimTypes.Fade:
                Fade();
                break;
        }

        // Set Other
        _tweenObject.setDelay(Delay);
        _tweenObject.setEase(EaseType);
        if (Loop)
            _tweenObject.loopCount = int.MaxValue;
        if (PingPong)
            _tweenObject.setLoopPingPong();

        // Set Events
        _tweenObject.setOnStart(() => {
            OnStart.Invoke();
        })
        .setOnUpdate((float value) => {
            OnUpdate.Invoke();
        })
        .setOnComplete(() => {
            OnComplete.Invoke();
        });
    }

    private void Fade()
    {
        CanvasGroup _group = Target.gameObject.GetComponent<CanvasGroup>();

        if (Offset)
            _group.alpha = From.x;

        _tweenObject = LeanTween.alphaCanvas(_group, To.x, Duration);
    }

    private void Scale()
    {
        if (Offset)
            Target.gameObject.GetComponent<RectTransform>().localScale = From;

        _tweenObject = LeanTween.scale(Target, To, Duration);
    }
}