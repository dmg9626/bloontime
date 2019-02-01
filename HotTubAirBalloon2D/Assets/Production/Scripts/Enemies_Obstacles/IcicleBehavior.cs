using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleBehavior : MonoBehaviour
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
        while(true){
            Debug.Log("looking");
            float playerDist = player.transform.position.x - transform.position.x;
            if(Mathf.Abs(playerDist) < attackRange)
            {
                StartCoroutine("fall");
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        
    }

    IEnumerator fall() 
    {
        while(true){
            
            transform.position = new Vector2(transform.position.x, transform.position.y - speedFactor);

            yield return null;
        }
    }

}
