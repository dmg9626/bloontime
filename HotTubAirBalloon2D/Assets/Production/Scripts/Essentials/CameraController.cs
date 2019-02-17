using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    private GameObject currentTarget;
    // public GameObject[] cameraTargets;
    private int targetIndex = 0;

    /// <summary>
    /// X-offset from camera position used when creating the next camera waypoint
    /// </summary>
    public Transform CameraTargetOffset;

    public GameObject cameraWaypoint;

    private float yIncrement;

    public float maxRaycastDistance;

    // Start is called before the first frame update
    void Start()
    {
        // Create initial waypoint
        currentTarget = CreateCameraWaypoint();
        UpdateYIncrement();
    }

    GameObject CreateCameraWaypoint()
    {
        // Get layer index
        int layerIndex = LayerMask.NameToLayer("Environment");

        // Bit shift layerIndex times to get a layermask, so we only check for collision with environment
        int layerMask = 1 << layerIndex;

        float topBound, bottomBound;

        // Check for top of level bounds
        RaycastHit2D raycastUp = Physics2D.Raycast(CameraTargetOffset.position, Vector3.up, maxRaycastDistance, layerMask);

        // Check for bottom of level bounds
        RaycastHit2D raycastDown = Physics2D.Raycast(CameraTargetOffset.position, Vector3.down, maxRaycastDistance, layerMask);
        
        if(raycastUp.collider == null || raycastDown.collider == null) {
            Debug.LogWarning("WARNING: raycast failed - no collision found");
            return null;
        }

        // TODO: consider using these to determine if player is visible on screen
        // float topDistance = (raycastUp.point - (Vector2)CameraTargetOffset.transform.position).magnitude;
        // float bottomDistance = (raycastDown.point - (Vector2)CameraTargetOffset.transform.position).magnitude;

        // Calculate top/bottom of level
        topBound = raycastUp.point.y;
        bottomBound = raycastDown.point.y;

        // Calculate middle of level
        float midBound = (topBound + bottomBound) / 2;
        
        return GameObject.Instantiate(cameraWaypoint, new Vector3(CameraTargetOffset.position.x, midBound, 0), transform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float playerPos = Player.position.x;

        // If player passed current target
        if(playerPos >= currentTarget.transform.position.x){

            // // Set current target to next
            // targetIndex++;
            // currentTarget = cameraTargets[targetIndex];
            currentTarget = CreateCameraWaypoint();

            UpdateYIncrement();
        }
    
        float newY = ((playerPos - transform.position.x) * yIncrement) + transform.position.y;

        transform.position = new Vector3(playerPos, newY, transform.position.z);
    }

    void UpdateYIncrement()
    {
        yIncrement = (currentTarget.transform.position.y - transform.position.y)/(currentTarget.transform.position.x - Player.position.x);
    }
}
