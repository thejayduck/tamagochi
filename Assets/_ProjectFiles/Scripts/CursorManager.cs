using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D Idle; 
    public Texture2D Pointer; 

    public void ChangeCursor (bool click){
        Cursor.SetCursor(click ? Pointer : Idle, Vector2.zero, CursorMode.Auto);
    }
}
