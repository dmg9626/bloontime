using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleBehavior : AbstractProjectile
{
    public GameObject player;
    public float attackRange;

    public float speedFactor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("takeTheShot");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator takeTheShot() 
    {
        bool notFall = true;
        while(notFall){
            float playerDist = player.transform.position.x - transform.position.x;
            if(Mathf.Abs(playerDist) < attackRange)
            {
                StartCoroutine("fall");
                notFall = false;
            }
            yield return new WaitForFixedUpdate();
        }
        
    }

    IEnumerator fall() 
    {
        while(true){
            
            transform.position = new Vector2(transform.position.x, transform.position.y - speedFactor);

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player")
        {
            Debug.Log("Collided with vulnerable object " + obj.name);
            if(collision.tag == "Player" && collision.GetComponent<BalloonController>() != null)
            {
                collision.GetComponent<BalloonController>().changeTemp(getTemp());
                collision.GetComponent<BalloonController>().changeComfort(getComfort());
            }
            Destroy(gameObject);

            // Execute code here
        }
    }

    public void takeDamage(float temp)
    {
        Destroy(this.gameObject);
    }

}
