using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        if (Input.GetKey(KeyCode.A))
            pos.x -= .1f;
        if (Input.GetKey(KeyCode.D))
            pos.x += .1f;
        if (Input.GetKey(KeyCode.W))
            pos.y += .1f;
        if (Input.GetKey(KeyCode.S))
            pos.y -= .1f;

        transform.position = pos;
    }
}
