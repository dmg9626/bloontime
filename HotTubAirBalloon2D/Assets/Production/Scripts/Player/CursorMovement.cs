using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{

    public float moveSpeed;

    public enum PlayerNumber { ONE, TWO }

    public PlayerNumber playerNum;

    public Transform cursor;

    // Update is called once per frame
    void Update()
    {
        UpdateCursor(playerNum);
    }

    void UpdateCursor(PlayerNumber playerNumber)
    {
        Vector2 pos = cursor.position;

        // Handle player one inputs
        if(playerNumber.Equals(PlayerNumber.ONE)) {
            if (Input.GetKey(KeyCode.A))
                pos.x -= moveSpeed;
            if (Input.GetKey(KeyCode.D))
                pos.x += moveSpeed;
            if (Input.GetKey(KeyCode.W))
                pos.y += moveSpeed;
            if (Input.GetKey(KeyCode.S))
                pos.y -= moveSpeed;
        }
        // Handle player two inputs
        else if(playerNumber.Equals(PlayerNumber.TWO)) {
            if (Input.GetKey(KeyCode.LeftArrow))
                pos.x -= moveSpeed;
            if (Input.GetKey(KeyCode.RightArrow))
                pos.x += moveSpeed;
            if (Input.GetKey(KeyCode.UpArrow))
                pos.y += moveSpeed;
            if (Input.GetKey(KeyCode.DownArrow))
                pos.y -= moveSpeed;
        }

        // Get cursor position in screen space (where 0,0 = min corner, 1,1 = max corner)
        Vector2 screenPos = Camera.main.WorldToViewportPoint(pos);

        // Clamp position to screen bounds
        screenPos = new Vector2(Mathf.Clamp01(screenPos.x), Mathf.Clamp01(screenPos.y));

        // Convert properly-clamped screen position back to world space
        cursor.position = (Vector2)Camera.main.ViewportToWorldPoint(screenPos);
    }
}
