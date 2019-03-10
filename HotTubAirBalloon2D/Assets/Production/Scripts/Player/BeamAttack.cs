using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    public Transform cursor;

    public ClickBurst.EffectType effectType;

    public float damagePerSecond;

    /// <summary>
    /// Max speed for beam to rotate towards cursor
    /// </summary>
    public float rotationSpeed;

    /// <summary>
    /// Rate at which beam expands towards cursor
    /// </summary>
    public float expansionSpeed;

    /// <summary>
    /// Interpolation value used for beam growth in FireBeam()
    /// </summary>
    private float beamInterpolationValue;

    BoxCollider2D col;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        beamInterpolationValue = 0;
    }

    void Update()
    {
        RotateTowardsCursor();

        if (Input.GetKey(KeyCode.Space))
        {
            FireBeam();
        }
        else
        {
            transform.localScale = Vector3.one;
            beamInterpolationValue = 0;
        }
    }

    void RotateTowardsCursor()
    {
        // Get angle to target (in degrees)
        Vector3 vectorToCursor = cursor.position - transform.position;
        float angle = Mathf.Atan2(vectorToCursor.y, vectorToCursor.x) * Mathf.Rad2Deg;

        // Calculate rotation towards cursor
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        // Smooth rotation towards cursor
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed);
    }

    // Update is called once per frame
    void FireBeam()
    {
        // Calculate distance to cursor
        Vector3 vectorToCursor = cursor.position - transform.position;
        float distanceToCursor = vectorToCursor.magnitude;

        // Get time value to interpolate over beam growth curve
        beamInterpolationValue = Mathf.Clamp01(beamInterpolationValue + (Time.fixedDeltaTime * expansionSpeed));

        // Scale beam length towards distanceToCursor
        float beamLength = Mathfx.Sinerp(1, distanceToCursor, beamInterpolationValue);
        transform.localScale = new Vector3(1, beamLength, 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player")
        {
            // Damage enemy
            if (collision.tag == "Enemy" && collision.GetComponent<AbstractObstacle>() != null)
            {
                Debug.Log(name + " collided with " + obj.name);
                collision.GetComponent<AbstractObstacle>().takeDamage(effectType, damagePerSecond * Time.fixedDeltaTime);
            }

            // Destroy projectile
            if (collision.tag == "Projectile" && collision.GetComponent<AbstractProjectile>() != null)
            {
                collision.GetComponent<AbstractProjectile>().takeDamage(effectType);
            }
        }
    }
}