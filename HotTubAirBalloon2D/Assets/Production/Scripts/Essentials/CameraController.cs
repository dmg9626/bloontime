using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    private GameObject currentTarget;

    /// <summary>
    /// X-offset from camera position used when creating the next camera waypoint
    /// </summary>
    public Transform CameraTargetOffset;

    private float yIncrement;

    public float maxRaycastDistance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return null;

        currentTarget = new GameObject("Camera Waypoint");

        resetCameraToPlayer();
    }

    void UpdateCameraWaypoint()
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

        // Calculate middle of level
        float midBound = (topBound + bottomBound) / 2;

        Debug.Log("Generated new camera waypoint");
        currentTarget.transform.position = new Vector3(CameraTargetOffset.position.x, midBound, 0);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 playerPos = Player.position;

        // If player passed current target
        if(currentTarget != null && playerPos.x >= currentTarget.transform.position.x){
            // Set current target to next
            UpdateCameraWaypoint();
            
            UpdateYIncrement();
        }
    
        float newY = ((playerPos.x - transform.position.x) * yIncrement) + transform.position.y;

        transform.position = new Vector3(playerPos.x, newY, transform.position.z);
    }

    void UpdateYIncrement()
    {
        if((currentTarget.transform.position.x - Player.position.x) == 0){
            yIncrement = 0;
        }else{
            yIncrement = (currentTarget.transform.position.y - transform.position.y)/(currentTarget.transform.position.x - Player.position.x);
        }
    }

    public void resetCameraToPlayer(){

        transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        currentTarget.transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        yIncrement = 0f;
        
    }
}
