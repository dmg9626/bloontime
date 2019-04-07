using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    public Transform cursor;

    public BurstAttack.EffectType effectType;

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

    SpriteRenderer spriteRenderer;
    
    /// <summary>
    /// Default transform.scale values
    /// </summary>
    Vector3 initalScale;

    Rigidbody2D rb;

    void Start()
    {
        // Get color effect settings
        if (effectType.Equals(BurstAttack.EffectType.FIRE)) {
            beamColorSettings = EffectManager.Instance.fireBeamColorSettings;
        }
        else if (effectType.Equals(BurstAttack.EffectType.ICE)) {
            beamColorSettings = EffectManager.Instance.iceBeamColorSettings;
        }

        // Get sprite renderer and set initial color
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Capture initial scale values
        initalScale = transform.localScale;

        // Set curve values to 0
        beamInterpolationValue = 0;
        colorAnimationValue = 0;
    }

    void Update()
    {
        RotateTowardsCursor();

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    FireBeam();
        //}
        //else
        //{
        //    // Shrink beam back to inital scale
        //    transform.localScale = initalScale;

        //    // Set curve values back to 0
        //    beamInterpolationValue = 0;
        //    colorAnimationValue = 0;
        //}
    }

    public void SetActive(bool active)
    {
        // Hide beam and disable collision
        spriteRenderer.enabled = active;
        rb.simulated = active;

        if(!active)
        {
            // Set curve values to 0
            beamInterpolationValue = 0;
            colorAnimationValue = 0;

            // Reset scale
            transform.localScale = initalScale;
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
    public void FireBeam()
    {
        // Calculate distance to cursor
        Vector3 vectorToCursor = cursor.position - transform.position;
        float distanceToCursor = vectorToCursor.magnitude;

        // Get time value to interpolate over beam growth curve
        beamInterpolationValue = Mathf.Clamp01(beamInterpolationValue + (Time.fixedDeltaTime * expansionSpeed));

        // Scale beam length towards distanceToCursor
        float beamLength = Mathfx.Sinerp(1, distanceToCursor, beamInterpolationValue);
        transform.localScale = new Vector3(initalScale.x, beamLength, initalScale.z);

        // Flash beam color
        AnimateBeamColor(animationSpeed);
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

    /// <summary>
    /// Animate beam color over gradient at rate
    /// </summary>
    /// <param name="rate">Speed of color animation</param>
    void AnimateBeamColor(float rate)
    {
        // Calculate gradient position
        colorAnimationValue += Time.fixedDeltaTime * rate;
        float pingPong = Mathf.PingPong(colorAnimationValue, 1);

        // Evaluate gradient at pingPong and update beam
        Color color = beamColorSettings.hitEffectGradient.Evaluate(pingPong);
        spriteRenderer.color = color;
    }
}