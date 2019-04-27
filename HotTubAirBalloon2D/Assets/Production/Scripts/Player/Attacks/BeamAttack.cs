using System.Collections;
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

    public ParticleSystem particleSystem;

    void Start()
    {
        // Get color effect settings
        if (effectType.Equals(BurstAttack.EffectType.FIRE)) {
            beamColorSettings = EffectManager.Instance.fireBeamColorSettings;
        }
        else if (effectType.Equals(BurstAttack.EffectType.ICE)) {
            beamColorSettings = EffectManager.Instance.iceBeamColorSettings;
        }

        // Capture initial transform values

        // Set curve values to 0
        //beamInterpolationValue = 0;
    }

    void Update()
    {
        RotateTowardsCursor();
    }

    /// <summary>
    /// Fires beam towards cursor position
    /// </summary>
    public void FireBeam()
    {
        
    }

    /// <summary>
    /// Rotates beam towards cursor
    /// </summary>
    void RotateTowardsCursor()
    {
        // Get angle to target (in degrees)
        Vector3 vectorToCursor = cursor.position - transform.position;
        float angle = Mathf.Atan2(vectorToCursor.y, vectorToCursor.x) * Mathf.Rad2Deg;

        // Calculate rotation towards cursor
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smooth rotation towards cursor
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed);
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