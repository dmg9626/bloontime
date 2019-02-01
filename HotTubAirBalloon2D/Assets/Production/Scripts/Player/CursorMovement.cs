using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{

    public float moveSpeed;

    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = cursor.transform.position;
        if (Input.GetKey(KeyCode.A))
            pos.x -= moveSpeed;
        if (Input.GetKey(KeyCode.D))
            pos.x += moveSpeed;
        if (Input.GetKey(KeyCode.W))
            pos.y += moveSpeed;
        if (Input.GetKey(KeyCode.S))
            pos.y -= moveSpeed;

        cursor.transform.position = pos;
    }
}
