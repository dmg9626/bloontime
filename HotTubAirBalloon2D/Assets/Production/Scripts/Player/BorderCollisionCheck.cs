using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollisionCheck : MonoBehaviour
{
    public BalloonController player;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BalloonController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Environment")
        {
            player.bottomCollision = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Environment")
        {
            player.bottomCollision = false;
        }
    }
}
