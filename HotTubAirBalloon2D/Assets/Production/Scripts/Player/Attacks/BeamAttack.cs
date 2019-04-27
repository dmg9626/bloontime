﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    public Transform cursor;

    [Header("Effects")]
    public BurstAttack.EffectType effectType;

    public float damagePerSecond;

    [Header("Movement Settings")]
    /// <summary>
    /// Max speed for beam to rotate towards cursor
    /// </summary>
    [Range(0f, 1.5f)]
    public float rotationSpeed;

    /// <summary>
    /// Duration released beam travels before disappearing
    /// </summary>
    public float releaseLifetime;

    [Header("Animation Settings")]
    /// <summary>
    /// Rate at which beam color iterates over gradient
    /// </summary>
    public float animationSpeed;

    /// <summary>
    /// Rate of animation while hitting enemy/obstacle
    /// </summary>
    public float hitAnimationSpeed;

    /// <summary>
    /// Interpolation value used for beam growth in FireBeam()
    /// </summary>
    private float beamInterpolationValue;

    /// <summary>
    /// Value used to animate beam color over gradient
    /// </summary>
    private float colorAnimationValue;

    /// <summary>
    /// Settings used for animating beam
    /// </summary>
    EffectManager.BeamColorSettings beamColorSettings;

    /// <summary>
    /// Beam's particle system
    /// </summary>
    public ParticleSystem particleSystem;


    ParticleSystem.EmissionModule emission;

    void Start()
    {
        // Get color effect settings
        if (effectType.Equals(BurstAttack.EffectType.FIRE)) {
            beamColorSettings = EffectManager.Instance.fireBeamColorSettings;
        }
        else if (effectType.Equals(BurstAttack.EffectType.ICE)) {
            beamColorSettings = EffectManager.Instance.iceBeamColorSettings;
        }

        // Disable particle emission
        emission = particleSystem.emission;
        emission.enabled = false;

        // Set curve values to 0
        //beamInterpolationValue = 0;
    }

    /// <summary>
    /// Called on update - fires beam if true, stops firing if false
    /// </summary>
    public void FireBeam(bool fire)
    {
        if(fire) {
            // Initialize beam before firing
            if (!emission.enabled) {
                InitBeam();
                emission.enabled = true;
            }

            // Smooth rotation towards target
            transform.rotation = Quaternion.RotateTowards(transform.rotation, GetRotationTowards(cursor.position), Time.fixedDeltaTime * rotationSpeed * 360f);
        }

        // Stop emitting particles
        else {
            emission.enabled = false;
        }
    }

    /// <summary>
    /// Initializes beam before firing particles
    /// </summary>
    void InitBeam()
    {
        // Set beam rotation towards cursor
        transform.rotation = GetRotationTowards(cursor.position);
    }

    /// <summary>
    /// Rotates beam towards target at given speed (float between 0 and 1)
    /// </summary>
    Quaternion GetRotationTowards(Vector3 target)
    {
        // Get angle to target (in degrees)
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        // Calculate rotation towards target
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        return q;
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    GameObject obj = collision.gameObject;
    //    if (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player")
    //    {
    //        // Damage enemy
    //        if (collision.tag == "Enemy" && collision.GetComponent<AbstractObstacle>() != null)
    //        {
    //            Debug.Log(name + " collided with " + obj.name);
    //            collision.GetComponent<AbstractObstacle>().takeDamage(effectType, damagePerSecond * Time.fixedDeltaTime);
    //        }

    //        // Destroy projectile
    //        if (collision.tag == "Projectile" && collision.GetComponent<AbstractProjectile>() != null)
    //        {
    //            collision.GetComponent<AbstractProjectile>().takeDamage(effectType, damagePerSecond * Time.fixedDeltaTime);
    //        }
    //    }
    //}
}