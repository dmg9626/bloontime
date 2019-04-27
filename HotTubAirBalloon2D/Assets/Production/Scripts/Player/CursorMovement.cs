using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CursorMovement : MonoBehaviour
{
    public GameObject balloon;

    public float moveSpeed;

    public enum PlayerNumber { ONE, TWO }

    public PlayerNumber playerNum;

    public Transform cursor;

    public bool pauseTemp = false;

    public float centralizedRadius;

    ///<summary>
    ///Updates cursor position from x and y
    ///CALLED IN INPUTMANAGER.CS
    ///</summary>
    public void UpdateCursor(float x, float y, bool centralized)
    {
        Vector2 cursorPos = cursor.position;

        if(centralized)
        {
            cursorPos.y = y * centralizedRadius + transform.parent.position.y;
            cursorPos.x = x * centralizedRadius + transform.parent.position.x;
        }
        else
        {
            cursorPos.y += y * moveSpeed;
            cursorPos.x += x * moveSpeed;
        }
        Vector2 screenPos = Camera.main.WorldToViewportPoint(cursorPos);

        // Clamp position to screen bounds
        screenPos = new Vector2(Mathf.Clamp01(screenPos.x), Mathf.Clamp01(screenPos.y));

        // Convert properly-clamped screen position back to world space
        cursor.position = (Vector2)Camera.main.ViewportToWorldPoint(screenPos);
    }
}
