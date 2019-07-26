using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : AbstractProjectile
{

    public float speedFactor;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * Time.deltaTime * speedFactor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player")
        {
            Debug.Log("Collided with vulnerable object " + obj.name);
            if(obj.tag == "Player")
            {
                // update temperature/confort meters
                BalloonController.Instance.changeTemp(getTemp() - BalloonController.Instance.getIceResist());
                BalloonController.Instance.changeComfort(getComfort());
            }
            Destroy(gameObject);

            // Execute code here
        }
    }

}
