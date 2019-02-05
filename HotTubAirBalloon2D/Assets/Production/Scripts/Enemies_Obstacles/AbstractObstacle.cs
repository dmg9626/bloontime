using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractObstacle : MonoBehaviour
{
    public ClickBurst.EffectType typeVunerable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(ClickBurst.EffectType effectType)
    {
        if (effectType.Equals(typeVunerable))
        {
            Destroy(this.gameObject);
        }
    }

}
