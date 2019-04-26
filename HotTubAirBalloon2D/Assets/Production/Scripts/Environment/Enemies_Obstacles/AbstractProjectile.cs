using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{

    public float tempChange, comfortChange;
    public BurstAttack.EffectType typeVunerable;
    public float maxHealth;
    private float currentHealth;

    public float getTemp()
    {
        return tempChange;
    }

    public float getComfort()
    {
        return comfortChange;
    }

    void setTemp(float newTemp)
    {
        tempChange = newTemp;
    }

    public void setTemp(BurstAttack.EffectType effectType)
    {
        if (effectType.Equals(BurstAttack.EffectType.FIRE))
        {
            setTemp(1);
        }
        else if (effectType.Equals(BurstAttack.EffectType.ICE))
        {
            setTemp(-1);
        }
    }

    public void setComfort(float newComfort)
    {
        comfortChange = newComfort;
    }

    public void takeDamage(BurstAttack.EffectType effectType, float damage)
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
