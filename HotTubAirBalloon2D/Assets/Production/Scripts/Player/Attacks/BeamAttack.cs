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
    /// Last gameobject hit by particle - saved to avoid redundant GetComponent() calls
    /// </summary>
    AbstractObstacle previousObstacleHit;

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

            // Get reference to AbstractObstacle component (if exists)
            AbstractObstacle enemy;

            // Check if we already have a reference to this enemy
            if(previousObstacleHit != null && other.Equals(previousObstacleHit.gameObject)) {
                enemy = previousObstacleHit;
                Debug.Log("Collided with previous enemy " + other.name);
            }
            // Otherwise get component on gameobject
            else {
                enemy = other.GetComponent<AbstractObstacle>();
                Debug.LogWarning("Called GetComponent<> on " + other.name);
            }
            
            // Apply damage if collided with enemy
            if(enemy != null) {
                enemy.takeDamage(effectType, particleDamage);

                // Save reference to enemy for subsequent particle collisions (saves a GetComponent<> call)
                previousObstacleHit = enemy;
            }
        }
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