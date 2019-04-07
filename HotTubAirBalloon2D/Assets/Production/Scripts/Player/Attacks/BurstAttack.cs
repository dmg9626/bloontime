using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstAttack : MonoBehaviour
{
    ParticleSystem psystem;

    public int particleCount;

    public GameObject burstTriggerPrefab;

    public BurstTrigger burstTrigger;

    public enum EffectType { FIRE, ICE };

    public EffectType effectType;

    // Start is called before the first frame update
    void Start()
    {
        psystem = GetComponent<ParticleSystem>();
        if(effectType.Equals(EffectType.FIRE))
        {
            SetColorOverLifetime(EffectManager.Instance.fireBurstGradient);
        }
        else if (effectType.Equals(EffectType.ICE))
        {
            SetColorOverLifetime(EffectManager.Instance.iceBurstGradient);
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

    void SetColorOverLifetime(Gradient gradient)
    {
        var colorModule = psystem.colorOverLifetime;
        colorModule.color = gradient;
    }
}
