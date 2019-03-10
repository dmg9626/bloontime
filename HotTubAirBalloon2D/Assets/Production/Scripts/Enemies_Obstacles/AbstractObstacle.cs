using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractObstacle : MonoBehaviour
{
    public ClickBurst.EffectType typeVunerable;

    public void takeDamage(ClickBurst.EffectType effectType, float damage)
    {
        if (effectType.Equals(typeVunerable))
        {
            Destroy(this.gameObject);
        }
    }

}
