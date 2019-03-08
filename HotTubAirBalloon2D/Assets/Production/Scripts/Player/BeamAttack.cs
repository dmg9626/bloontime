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
    /// Max beam expansion speed
    /// </summary>
    // public float expansionSpeed;

    BoxCollider2D col;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) {
            FireBeam();
        }
        else {
             transform.localScale = Vector3.one;
             RotateTowardsCursor();
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
        RotateTowardsCursor();
        
        // Calculate vector towards cursor
        Vector3 vectorToCursor = cursor.position - transform.position;

        // Distance to cursor
        float distanceToCursor = vectorToCursor.magnitude;

        // Get beam length (scaled between 0 and 1)
        float beamLengthScaled = transform.localScale.y / distanceToCursor;

        // Scale beam length towards distanceToCursor
        float beamLength = Mathf.Lerp(1, distanceToCursor, beamLengthScaled);
        transform.localScale = new Vector3(1, beamLength, 1);

        // Debug.Log("Beam length: " + beamLength);
        // Debug.Log("Beam length scaled: " + beamLengthScaled);
        // Debug.Log("Distance: " + distanceToCursor);
    }
}
