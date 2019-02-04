using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : AbstractProjectile
{

    public float speedFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * Time.deltaTime * speedFactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("something is happening");
        GameObject obj = collision.gameObject;
        if (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player")
        {
            Debug.Log("Collided with vulnerable object " + obj.name);
            if(collision.tag == "Player")
            {
                collision.GetComponent<BalloonController>().changeTemp(getTemp());
                collision.GetComponent<BalloonController>().changeComfort(getComfort());
            }
            Destroy(gameObject);

            // Execute code here
        }
    }

    public void takeDamage(float temp)
    {
        Destroy(this.gameObject);
    }
}
