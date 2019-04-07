using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractObstacle : MonoBehaviour
{
    public ClickBurst.EffectType typeVunerable;

    public float maxHealth;

    private float currentHealth;

    public void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
    }

    public void takeDamage(ClickBurst.EffectType effectType, float damage)
    {
        if (effectType.Equals(typeVunerable))
        {
            // Reduce health
            Debug.LogWarningFormat(name + " | Reducing health ({0}) by ({1})", currentHealth, damage);
            currentHealth -= damage;

            // Destroy if below 0
            if(currentHealth <= 0) {
                Debug.LogWarning(name + " | health below 0, destroying object");
                Destroy(gameObject);
            }
        }
    }

}
