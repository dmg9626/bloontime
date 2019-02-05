using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] cameraTargets;
    private int targetIndex;
    private GameObject currentTarget;
    private float yIncrement;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        targetIndex = 0;
        currentTarget = cameraTargets[0];
        yIncrement = (currentTarget.transform.position.y - transform.position.y)/(currentTarget.transform.position.x - Player.transform.position.x);
        Debug.Log(yIncrement);
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 playerPos = Player.transform.position;

        if(playerPos.x >= currentTarget.transform.position.x){
            targetIndex++;
            currentTarget = cameraTargets[targetIndex];
            yIncrement = (currentTarget.transform.position.y - transform.position.y)/(currentTarget.transform.position.x - Player.transform.position.x);
        }

        float newY = ((playerPos.x - transform.position.x)*yIncrement) + transform.position.y;


        transform.position = new Vector3(playerPos.x, newY, transform.position.z);
    }
}
