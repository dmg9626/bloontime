using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollisionCheck : MonoBehaviour
{
    
    public enum SIDE { TOP, BOTTOM, LEFT, RIGHT };

    public BalloonController player;
    public SIDE colliderSide;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").GetComponent<BalloonController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Environment")
        {
            switch(colliderSide){
                case SIDE.BOTTOM: 
                    player.bottomCollision = true;
                    break;
                case SIDE.TOP:
                    player.topCollision = true;
                    break;
                default:
                    break;
            }
            
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Environment")
        {
            switch(colliderSide){
                case SIDE.BOTTOM: 
                    player.bottomCollision = false;
                    break;
                case SIDE.TOP:
                    player.topCollision = false;
                    break;
                default:
                    break;
            }
        }
    }
}
