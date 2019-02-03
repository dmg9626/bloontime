﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBurst : MonoBehaviour
{
    ParticleSystem psystem;

    public int particleCount;

    public GameObject burstTriggerPrefab;

    public BurstTrigger burstTrigger;

    public Gradient fireGradient;

    public Gradient iceGradient;

    public enum EffectType { FIRE, ICE };

    public EffectType effectType;

    // Start is called before the first frame update
    void Start()
    {
        psystem = GetComponent<ParticleSystem>();
        if(effectType.Equals(EffectType.FIRE))
        {
            SetColorOverLifetime(fireGradient);
        }
        else if (effectType.Equals(EffectType.ICE))
        {
            SetColorOverLifetime(iceGradient);
        }

    }

public void Burst(Vector2 position)
    {
        psystem.transform.position = position;
        psystem.Emit(particleCount);

        if(burstTrigger == null) {
            burstTrigger = Instantiate(burstTriggerPrefab, position, transform.rotation).GetComponent<BurstTrigger>();
            burstTrigger.effectType = effectType;
        }
        else {
            burstTrigger.transform.position = position;
            burstTrigger.gameObject.SetActive(true);
        }
        
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Q))
        //{
        //    SetColorOverLifetime(fireGradient);
        //    effectType = EffectType.FIRE;
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    SetColorOverLifetime(iceGradient);
        //    effectType = EffectType.ICE;
        //}
    }

    void SetColorOverLifetime(Gradient gradient)
    {
        var colorModule = psystem.colorOverLifetime;
        colorModule.color = gradient;
    }
}
