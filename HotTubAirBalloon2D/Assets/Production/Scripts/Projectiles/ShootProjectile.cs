using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public Projectile projectile;

    public float maxSpeed;

    public float lifetime = 5;


    public void Shoot(Vector2 mousePos)
    {
        // Get direction of click
        Vector2 direction = mousePos - (Vector2)transform.position;

        // Clamp to max speed
        Vector2 trajectory = Vector3.Normalize(direction) * maxSpeed;

        Debug.Log("Shooting projectile towards " + trajectory + " at speed " + trajectory.magnitude);

        // Spawn projectile and set trajectory
        Projectile proj = GameObject.Instantiate(projectile, transform.position, transform.rotation);
        proj.SetDirection(trajectory);

        // Destroy when lifetime elapses
        GameObject.Destroy(proj.gameObject, lifetime);
    }
}
