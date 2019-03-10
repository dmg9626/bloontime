using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    /// <summary>
    /// Position of cursor
    /// </summary>
    public Transform cursor;

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

    /// <summary>
    /// Max beam expansion speed
    /// </summary>
    // public float expansionSpeed;

    BoxCollider2D col;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        beamInterpolationValue = 0;
    }

    void Update()
    {
        RotateTowardsCursor();

        if (Input.GetKey(KeyCode.Space)) {
            FireBeam();
        }
        else {
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }

    // Update is called once per frame
    void FireBeam()
    {
        // Calculate distance to cursor
        Vector3 vectorToCursor = cursor.position - transform.position;
        float distanceToCursor = vectorToCursor.magnitude;

        // Get time value to interpolate over beam growth curve
        beamInterpolationValue = Mathf.Clamp01(beamInterpolationValue + (Time.deltaTime * expansionSpeed));

        // Scale beam length towards distanceToCursor
        float beamLength = Mathfx.Sinerp(1, distanceToCursor, beamInterpolationValue);
        transform.localScale = new Vector3(1, beamLength, 1);
    }
}
