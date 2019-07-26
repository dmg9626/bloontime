using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleBehavior : AbstractProjectile
{
    public GameObject player;
    public float attackRange;

    public ParticleSystem particles;

    public float speedFactor;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(takeTheShot());
    }

    IEnumerator takeTheShot() 
    {
        bool notFall = true;
        while(notFall){
            float playerDist = player.transform.position.x - transform.position.x;
            if(Mathf.Abs(playerDist) < attackRange)
            {
                StartCoroutine(fall());
                notFall = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    protected IEnumerator fall() 
    {
        particles.Clear();
        var main = particles.main;
        main.startSpeed = 6f;
        particles.Emit(1);
        while(true){
            
            transform.position = new Vector2(transform.position.x, transform.position.y - speedFactor);

            yield return new WaitForFixedUpdate();
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (LayerMask.LayerToName(obj.layer) == "Vulnerable" || LayerMask.LayerToName(obj.layer) == "Player")
        {
            Debug.Log("Collided with vulnerable object " + obj.name);
            if(obj.tag == "Player")
            {
                BalloonController.Instance.changeTemp(getTemp());
                BalloonController.Instance.changeComfort(getComfort());
            }
            Destroy(gameObject);

            // Execute code here
        }
    }
}
