using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private GameObject currentTarget;
    public GameObject[] cameraTargets;
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
        currentTarget = cameraTargets[0];
        UpdateYIncrement();
        Debug.Log(yIncrement);
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);

    }

    void CreateCameraWaypoint()
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
            return;
        }

        // float topDistance = (raycastUp.point - (Vector2)CameraTargetOffset.transform.position).magnitude;
        // float bottomDistance = (raycastDown.point - (Vector2)CameraTargetOffset.transform.position).magnitude;

        // Calculate top/bottom of level
        topBound = raycastUp.point.y;
        bottomBound = raycastDown.point.y;
        Debug.LogWarningFormat("Upwards raycast collided with {0} at y = {1}", raycastUp.collider.gameObject.name, topBound);
        Debug.LogWarningFormat("Downwards raycast collided with {0} at y = {1}", raycastDown.collider.gameObject.name, bottomBound);

        // Calculate middle of level
        float midBound = (topBound + bottomBound) / 2;

        GameObject.Instantiate(cameraWaypoint, new Vector3(CameraTargetOffset.position.x, midBound, 0), transform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 playerPos = Player.transform.position;

        // If player passed current target
        if(playerPos.x >= currentTarget.transform.position.x){

            // Set current target to next
            targetIndex++;
            currentTarget = cameraTargets[targetIndex];

            UpdateYIncrement();
        }
    
        float newY = ((playerPos.x - transform.position.x) * yIncrement) + transform.position.y;

        transform.position = new Vector3(playerPos.x, newY, transform.position.z);

        if(Input.GetKeyDown(KeyCode.Space)) {
            CreateCameraWaypoint();
        }
    }

    void UpdateYIncrement()
    {
        yIncrement = (currentTarget.transform.position.y - transform.position.y)/(currentTarget.transform.position.x - Player.transform.position.x);
        Debug.LogWarning("Y Increment: " + yIncrement);
    }
}
