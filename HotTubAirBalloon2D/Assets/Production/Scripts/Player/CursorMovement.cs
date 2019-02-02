using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{

    public float moveSpeed;

    public enum PlayerNumber { ONE, TWO }

    public PlayerNumber playerNum;

    public Transform cursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNum.Equals(PlayerNumber.ONE))
        {
            PlayerOneUpdate();
        }
        else if (playerNum.Equals(PlayerNumber.TWO))
        {
            PlayerTwoUpdate();
        }
    }

    void PlayerOneUpdate()
    {
        Vector2 pos = cursor.position;
        if (Input.GetKey(KeyCode.A))
            pos.x -= moveSpeed;
        if (Input.GetKey(KeyCode.D))
            pos.x += moveSpeed;
        if (Input.GetKey(KeyCode.W))
            pos.y += moveSpeed;
        if (Input.GetKey(KeyCode.S))
            pos.y -= moveSpeed;

        cursor.position = pos;
    }

    void PlayerTwoUpdate()
    {
        Vector2 pos = cursor.position;
        if (Input.GetKey(KeyCode.LeftArrow))
            pos.x -= moveSpeed;
        if (Input.GetKey(KeyCode.RightArrow))
            pos.x += moveSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
            pos.y += moveSpeed;
        if (Input.GetKey(KeyCode.DownArrow))
            pos.y -= moveSpeed;

        cursor.position = pos;
    }
}
