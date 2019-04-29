using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    public float fallSpeed;
    private float speed = 0;
    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - speed);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        speed = 0;
        transform.SetParent(null);
    }

    public void fall()
    {
        speed = fallSpeed;
    }
}
