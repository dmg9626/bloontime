﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{

    public float tempChange, comfortChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void setTemp(ClickBurst.EffectType effectType)
    {
        if (effectType.Equals(ClickBurst.EffectType.FIRE))
        {
            setTemp(1);
        }
        else if (effectType.Equals(ClickBurst.EffectType.ICE))
        {
            setTemp(-1);
        }
    }

    public void setComfort(float newComfort)
    {
        comfortChange = newComfort;
    }
}