using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstTrigger : AbstractProjectile
{
    public float lifetime;

    public ClickBurst.EffectType effectType;

    public float damage;

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

            // Damage enemy
            if(collision.tag == "Enemy" && collision.GetComponent<AbstractObstacle>() != null) {
                collision.GetComponent<AbstractObstacle>().takeDamage(effectType, damage);
            }

            // Destroy projectile
            if(collision.tag == "Projectile" && collision.GetComponent<AbstractProjectile>() != null) {
                collision.GetComponent<AbstractProjectile>().takeDamage(effectType);
            }

            // Increase balloon temperature
            if(collision.tag == "Player" && collision.GetComponent<BalloonController>() != null)
            {
                collision.GetComponent<BalloonController>().changeTemp(effectType);
            }
            
            Destroy(gameObject);
        }
    }
}
