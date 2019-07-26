using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinBehaviour : AbstractProjectile
{
    public GameObject player;
    public float attackRange;
    public float speedFactor;

    public Vector2 movementTarget;
    public float movementTargetOffset;
    public float verticalSinOffset;
    public float maxRaycastDistance;
    public float WaveFrequency;

    public bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = BalloonController.Instance.gameObject;
        movementTarget = transform.position;
        StartCoroutine(waitForPlayer());
    }

    IEnumerator waitForPlayer() 
    {
        float currentTime = 0;
        float interY = 0;
        Vector3 movementTarget3 = new Vector3(movementTarget.x, movementTarget.y, 0);
        while(true){
            
            if(Mathf.Abs(transform.position.x - movementTarget.x) <= .2f){
                UpdateMovementTarget();
                movementTarget3 = new Vector3(movementTarget.x, movementTarget.y, 0);

            }

            transform.right = new Vector3(Mathf.Lerp(transform.right.x, movementTarget3.x - transform.position.x, .3f), Mathf.Lerp(transform.right.y, movementTarget3.y - transform.position.y, .3f), 0);

            // transform.position += new Vector3(transform.right.x * Time.deltaTime * speedFactor, (transform.right.y/Mathf.Abs(movementTargetOffset)) + (verticalSinOffset * Mathf.Sin(currentTime*WaveFrequency)), 0);
            transform.position += new Vector3(transform.right.x * Time.deltaTime * speedFactor, (transform.right.y) + (verticalSinOffset * Mathf.Sin(currentTime*WaveFrequency)), 0);
            
            currentTime += Time.deltaTime;

            float playerDist = Vector2.Distance(player.transform.position, transform.position);
            
            if(Mathf.Abs(playerDist) < attackRange)
            {
                StartCoroutine(diveBomb());
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
        
    }

    void UpdateMovementTarget()
    {

        Debug.Log("UpdatingMovementTarget");

        // Get layer index
        int layerIndex = LayerMask.NameToLayer("Environment");

        // Bit shift layerIndex times to get a layermask, so we only check for collision with environment
        int layerMask = 1 << layerIndex;

        float topBound, bottomBound;

        // Check for top of level bounds
        RaycastHit2D raycastUp = Physics2D.Raycast(new Vector2(transform.position.x+movementTargetOffset, transform.position.y), Vector3.up, maxRaycastDistance, layerMask);

        // Check for bottom of level bounds
        RaycastHit2D raycastDown = Physics2D.Raycast(new Vector2(transform.position.x+movementTargetOffset, transform.position.y), Vector3.down, maxRaycastDistance, layerMask);
        
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
        movementTarget = new Vector2(transform.position.x+movementTargetOffset, midBound);
    }

    IEnumerator diveBomb(){
        transform.right = player.transform.position - transform.position;
        transform.right = new Vector3(transform.right.x, transform.right.y, 0);

        while(true){
            //transform.right = player.transform.position - transform.position;
            transform.position += transform.right * Time.deltaTime * speedFactor;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if ((!collided) && (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player"))
        {
            collided = true;
            Debug.Log("Collided with vulnerable object " + obj.name);
            if(obj.tag == "Player")
            {
                // update temperature/confort meters
                BalloonController.Instance.changeTemp(getTemp() - BalloonController.Instance.getIceResist());
                BalloonController.Instance.changeComfort(getComfort());
            }
            Destroy(gameObject);

            // Execute code here
        }
    }
    
}
