using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
// using Rewired.ReInput;

public class CursorMovement : MonoBehaviour
{

    public float moveSpeed;

    public enum PlayerNumber { ONE, TWO }

    public PlayerNumber playerNum;

    public Transform cursor;
    private Rewired.Player player { get { return GetRewiredPlayer(0); } }

    // Update is called once per frame
    void Update()
    {
        UpdateCursor(playerNum);
    }
    public static Rewired.Player GetRewiredPlayer(int gamePlayerId) {
        return ReInput.players.GetPlayer(0);
    }

    void UpdateCursor(PlayerNumber playerNumber)
    {
        Vector2 pos = cursor.position;

        // Handle player one inputs
        if(playerNumber.Equals(PlayerNumber.ONE)) {
            if (Input.GetKey(KeyCode.A) || player.GetAxis("Fire_MoveCursorX") < 0)
                pos.x -= moveSpeed;
            if (Input.GetKey(KeyCode.D) || player.GetAxis("Fire_MoveCursorX") > 0)
                pos.x += moveSpeed;
            if (Input.GetKey(KeyCode.W) || player.GetAxis("Fire_MoveCursorY") > 0)
                pos.y += moveSpeed;
            if (Input.GetKey(KeyCode.S) || player.GetAxis("Fire_MoveCursorY") < 0)
                pos.y -= moveSpeed;
        }
        // Handle player two inputs
        else if(playerNumber.Equals(PlayerNumber.TWO)) {
            if (Input.GetKey(KeyCode.LeftArrow) || player.GetAxis("Ice_MoveCursorX") < 0)
                pos.x -= moveSpeed;
            if (Input.GetKey(KeyCode.RightArrow) || player.GetAxis("Ice_MoveCursorX") > 0)
                pos.x += moveSpeed;
            if (Input.GetKey(KeyCode.UpArrow) || player.GetAxis("Ice_MoveCursorY") > 0)
                pos.y += moveSpeed;
            if (Input.GetKey(KeyCode.DownArrow) || player.GetAxis("Ice_MoveCursorY") < 0)
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
