using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    public Transform cursor;

    /// <summary>
    /// Used to rotate beam direction
    /// </summary>
    public Transform pivot;

    [Header("Effects")]
    public BurstAttack.EffectType effectType;

    public float damagePerSecond;

    public float powerToDPS;

    [Header("Movement Settings")]
    /// <summary>
    /// Max speed for beam to rotate towards cursor
    /// </summary>
    [Range(0f, 1.5f)]
    public float rotationSpeed;

    /// <summary>
    /// Controls beam length via particle lifetime
    /// </summary>
    public float beamLength;

    /// <summary>
    /// Damage done by a single particle
    /// </summary>
    private float particleDamage;

    /// <summary>
    /// Settings used for animating beam
    /// </summary>
    EffectManager.BeamColorSettings beamColorSettings;

    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule emission;

    /// <summary>
    /// Last AbstractObstacle hit by particle - saved to avoid redundant GetComponent() calls
    /// </summary>
    AbstractObstacle previousObstacleHit;

    /// <summary>
    /// Last AbstractProjectile hit by particle - saved to avoid redundant GetComponent() calls
    /// </summary>
    AbstractProjectile previousProjectileHit;

    void Start()
    {
        // Get color effect settings
        if (effectType.Equals(BurstAttack.EffectType.FIRE)) {
            beamColorSettings = EffectManager.Instance.fireBeamColorSettings;
        }
        else if (effectType.Equals(BurstAttack.EffectType.ICE)) {
            beamColorSettings = EffectManager.Instance.iceBeamColorSettings;
        }

        particleSystem = GetComponent<ParticleSystem>();

        // Disable particle emission
        emission = particleSystem.emission;
        emission.enabled = false;

        // Set particle system params
        main = particleSystem.main;
        main.startLifetime = beamLength;

        // Calculate individual particle damage (based on particles emitted per second)
        particleDamage = (1 / emission.rateOverTime.constant) * damagePerSecond;
        Debug.Log(effectType + " particle damage: " + particleDamage);
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
            pivot.rotation = Quaternion.RotateTowards(pivot.rotation, GetRotationTowards(cursor.position), Time.fixedDeltaTime * rotationSpeed * 360f);
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
        pivot.rotation = GetRotationTowards(cursor.position);
    }

    /// <summary>
    /// Rotates beam towards target at given speed (float between 0 and 1)
    /// </summary>
    Quaternion GetRotationTowards(Vector3 target)
    {
        // Get angle to target (in degrees)
        Vector3 vectorToTarget = target - pivot.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        // Calculate rotation towards target
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        return q;
    }

    /// <summary>
    /// OnParticleCollision is called when a particle hits a collider.
    /// </summary>
    /// <param name="other">The GameObject hit by the particle.</param>
    protected void OnParticleCollision(GameObject other)
    {
        // Ignore everything outside Vulnerable layer
        if(other.layer.Equals(LayerMask.NameToLayer("Vulnerable"))) {
            
            // Handles collision with AbstractObstacle
            HandleObstacleCollision(other);

            // Handles collision with AbstractProjectile
            HandleProjectileCollision(other);
        }
    }

    void HandleObstacleCollision(GameObject obj)
    {
        // Get reference to AbstractObstacle component (if exists)
        AbstractObstacle obstacle;

        // Check if we already have a reference to this enemy
        if(previousObstacleHit != null && obj.Equals(previousObstacleHit.gameObject)) {
            obstacle = previousObstacleHit;
            // Debug.Log(effectType + " beam with previous obstacle " + obj.name);
        }
        // Otherwise get component on gameobject
        else {
            obstacle = obj.GetComponent<AbstractObstacle>();
            // Debug.LogWarning(effectType + " beam called GetComponent<> on " + obj.name);
        }
        
        // Apply damage if collided with enemy
        if(obstacle != null) {
            obstacle.takeDamage(effectType, particleDamage);

            // Save reference to obstacle for subsequent particle collisions (saves a GetComponent<> call)
            previousObstacleHit = obstacle;
        }
    }

    void HandleProjectileCollision(GameObject obj)
    {
        // Get reference to AbstractProjectile component (if exists)
        AbstractProjectile projectile;

        // Check if we already have a reference to this enemy
        if(previousObstacleHit != null && obj.Equals(previousObstacleHit.gameObject)) {
            projectile = previousProjectileHit;
            Debug.Log(effectType + " beam hit previous projectile " + obj.name);
        }
        // Otherwise get component on gameobject
        else {
            projectile = obj.GetComponent<AbstractProjectile>();
            Debug.LogWarning(effectType + " beam called GetComponent<> on " + obj.name);
        }
        
        // Apply damage if collided with enemy
        if(projectile != null) {
            projectile.takeDamage(effectType, particleDamage);
            Debug.Log("Dealing " + particleDamage + " to " + projectile.name);

            // Save reference to projectile for subsequent particle collisions (saves a GetComponent<> call)
            previousProjectileHit = projectile;
        }
    }

    public void updateBeamLength(float beamTime){
        beamLength = beamTime;
        main.startLifetime = beamLength;

    }

    public void updateBeamDamage(float newPowerLevel){

        damagePerSecond = newPowerLevel * powerToDPS;

        // Calculate individual particle damage (based on particles emitted per second)
        particleDamage = (1 / emission.rateOverTime.constant) * damagePerSecond;
        Debug.Log(effectType + " particle damage: " + particleDamage);

    }
}