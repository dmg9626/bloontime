using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderBehavior : MonoBehaviour
{
    public GameObject player;
    public float attackRange;

    public GameObject fireball;

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
            Debug.Log("looking");
            float playerDist = Vector2.Distance(player.transform.position, transform.position);
            if(Mathf.Abs(playerDist) < attackRange)
            {
                shootFireball();
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        
    }

    void shootFireball(){
        GameObject fb = Instantiate(fireball, transform.position, transform.rotation);
        fb.transform.LookAt(player.transform.position);
        Destroy (fb, 1.5f);
    }
}
