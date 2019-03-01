using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    public Transform target;

    public float speed;

    Transform init;

    // Update is called once per frame
    void Update()
    {
        // Get angle to target (in degrees)
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        // Debug.Log("Angle to target: " + angle);

        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        Debug.LogWarning(q.eulerAngles);
        Debug.Log(transform.rotation.eulerAngles);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * speed);
        // transform.Rotate(Vector3.forward, angle - 90, Space.Self);
        transform.localScale = new Vector3(1, vectorToTarget.magnitude, 1);
    }
}
