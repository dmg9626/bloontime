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

    public bool centralizedAnalog;

    public bool pauseTemp = false;

    private Rewired.Player player { get { return GetRewiredPlayer(0); } }
    private Vector2 fireMovement, iceMovement;

    void Start(){
        // cursor.position = (playerNum.Equals(PlayerNumber.ONE) ? new Vector3(1,0,0) : new Vector3(-1,0,0));
    }

    // Update is called once per frame
    void Update()
    {

        if((player.GetButtonDown("Pause")) && pauseTemp){
            GM.Instance.PauseGame();
        }

        if(!GM.Instance.isFrozen)
        {
            UpdateCursor(playerNum);
            fireMovement.x = player.GetAxis("Fire_MoveCursorX");
            fireMovement.y = player.GetAxis("Fire_MoveCursorY");
            iceMovement.x = player.GetAxis("Ice_MoveCursorX");
            iceMovement.y = player.GetAxis("Ice_MoveCursorY");
        }
    }


    public static Rewired.Player GetRewiredPlayer(int gamePlayerId) {
        return ReInput.players.GetPlayer(0);
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

            if(centralizedAnalog){
                pos.y = fireMovement.y * 5 + transform.parent.position.y;
                pos.x = fireMovement.x * 5 + transform.parent.position.x;
            }
            else{
                pos.y += fireMovement.y * moveSpeed;
                pos.x += fireMovement.x * moveSpeed;
            }

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

            if(centralizedAnalog){
                pos.y = iceMovement.y * 5 + transform.parent.position.y;
                pos.x = iceMovement.x * 5 + transform.parent.position.x;
            }
            else{
                pos.y += iceMovement.y * moveSpeed;
                pos.x += iceMovement.x * moveSpeed;
            }
        }

        // Get cursor position in screen space (where 0,0 = min corner, 1,1 = max corner)
        Vector2 screenPos = Camera.main.WorldToViewportPoint(pos);

        // Clamp position to screen bounds
        screenPos = new Vector2(Mathf.Clamp01(screenPos.x), Mathf.Clamp01(screenPos.y));

        // Convert properly-clamped screen position back to world space
        cursor.position = (Vector2)Camera.main.ViewportToWorldPoint(screenPos);
    }
}
