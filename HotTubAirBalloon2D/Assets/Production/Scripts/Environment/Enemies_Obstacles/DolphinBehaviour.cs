using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinBehaviour : AbstractProjectile
{
    public GameObject player;
    public float attackRange;
    public float speedFactor;

    public bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        player = BalloonController.Instance.gameObject;
        StartCoroutine(waitForPlayer());
    }

    IEnumerator waitForPlayer() 
    {
        while(true){
            float playerDist = Vector2.Distance(player.transform.position, transform.position);
            if(Mathf.Abs(playerDist) < attackRange)
            {
                StartCoroutine(diveBomb());
                StopCoroutine(waitForPlayer());
            }
            yield return new WaitForFixedUpdate();
        }
        
    }

    IEnumerator diveBomb(){
        transform.right = player.transform.position - transform.position;
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
