using UnityEngine;

public abstract class PickableBase : MonoBehaviour
{
    [HideInInspector] public Vector2 offset;
    [HideInInspector] public Vector2 curScreenPoint;
    [HideInInspector] public Vector2 curPosition;
    [HideInInspector] public Vector2 velocity;
    [HideInInspector] public Vector2 delta;

    public AudioSource source;
    public GameObject Target;
    public float DragThreshold;

    [Header("Audio")]
    public AudioClip PickClip;
    public AudioClip DropClip;
    public AudioClip DragClip;

    public void Start()
    {
        source = GetComponent<AudioSource>();   

        if (Target == null)
            Target = this.gameObject;
    }

    public void OnMouseDown()
    {
        if (PickClip)
            source.PlayOneShot(PickClip);

        offset = Target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
    }

    public void OnMouseDrag()
    {
        curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        curPosition = (Vector2)Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        delta = curPosition - (Vector2)Target.transform.position;
        Target.transform.position = curPosition;

        if (DragThreshold > 0 && delta.magnitude >= DragThreshold && !source.isPlaying){
            source.PlayOneShot(DragClip);
        }
    }

    public void OnMouseUp()
    {
        if (DropClip)
            source.PlayOneShot(DropClip);
    }
}