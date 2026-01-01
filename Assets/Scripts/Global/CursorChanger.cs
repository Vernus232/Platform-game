using UnityEngine;
using System.Collections;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;


    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}