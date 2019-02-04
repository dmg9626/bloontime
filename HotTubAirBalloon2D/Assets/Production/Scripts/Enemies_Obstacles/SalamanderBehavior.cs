using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderBehavior : MonoBehaviour
{
    public GameObject player;
    public float attackRange;

    public GameObject fireball;
    public GameObject Cannon;

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
        while(true){
            float playerDist = Vector2.Distance(player.transform.position, transform.position);
            if(Mathf.Abs(playerDist) < attackRange)
            {
                shootFireball();
                Cannon.SetActive(true);
                Cannon.transform.right = player.transform.position - Cannon.transform.position;
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForFixedUpdate();
        }
        
    }

    void shootFireball(){
        GameObject fb = Instantiate(fireball, transform.position, transform.rotation);
        fb.transform.right = player.transform.position - fb.transform.position;
        Destroy (fb, 1.5f);
    }

    public void takeDamage(float temp)
    {
        Destroy(this.gameObject);
    }
}
