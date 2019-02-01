using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstTrigger : AbstractProjectile
{
    public float lifetime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player")
        {
            Debug.Log("Collided with vulnerable object " + obj.name);
            if(collision.tag == "Enemy" && collision.GetComponent<SalamanderBehavior>() != null)
                collision.GetComponent<SalamanderBehavior>().takeDamage(getTemp());
            if(collision.tag == "Player")
            {
                collision.GetComponent<BalloonController>().changeTemp(getTemp());
                // Debug.Log("TempChange: " + getTemp());
                // Debug.Log("new Temp: " + collision.GetComponent<BalloonController>().temperature);
            }
            Destroy(gameObject);

            // Execute code here
        }
    }
}
