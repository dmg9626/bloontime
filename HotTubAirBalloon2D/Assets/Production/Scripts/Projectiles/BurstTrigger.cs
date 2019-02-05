using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstTrigger : AbstractProjectile
{
    public float lifetime;

    public ClickBurst.EffectType effectType;

    private void OnEnable()
    {
        // Disable in a second so it doesn't sit there waiting for balloon to collide
        StartCoroutine(SetInactiveDelay(1));
    }

    IEnumerator SetInactiveDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player")
        {
            Debug.Log("Collided with vulnerable object " + obj.name);
            if(collision.tag == "Enemy" && collision.GetComponent<AbstractObstacle>() != null)
                collision.GetComponent<AbstractObstacle>().takeDamage(effectType);
            if(collision.tag == "Projectile" && collision.GetComponent<AbstractProjectile>() != null)
                collision.GetComponent<AbstractProjectile>().takeDamage(effectType);
            if(collision.tag == "Player")
            {
                collision.GetComponent<BalloonController>().changeTemp(effectType);
                // Debug.Log("TempChange: " + getTemp());
                // Debug.Log("new Temp: " + collision.GetComponent<BalloonController>().temperature);
            }
            Destroy(gameObject);

            // Execute code here
        }
    }
}
