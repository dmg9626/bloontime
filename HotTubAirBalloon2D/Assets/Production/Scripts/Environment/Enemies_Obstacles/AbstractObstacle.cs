using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObstacle : MonoBehaviour
{
    public BurstAttack.EffectType typeVunerable;

    public float maxHealth;

    public float currentHealth;

    public void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
    }

    public virtual void takeDamage(BurstAttack.EffectType effectType, float damage)
    {
        if (effectType.Equals(typeVunerable))
        {
            // Reduce health
            Debug.Log(name + " | Reducing health " + currentHealth + " by " + damage);
            currentHealth -= damage;

            // Destroy if below 0
            if(currentHealth <= 0) {
                Debug.Log(name + " | health below 0, destroying object");
                Destroy(gameObject);
            }
        }
    }

}
