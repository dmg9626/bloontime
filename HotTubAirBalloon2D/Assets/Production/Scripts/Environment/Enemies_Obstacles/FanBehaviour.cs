using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaviour : AbstractObstacle
{

    public SpriteRenderer iceSprite;
    public ParticleSystem windSystem;
    public bool isFrozen;
    public float windStrength;
    public float minWindStrength;
    public float maxWindStrength;


    // Start is called before the first frame update
    void Start()
    {
        if(isFrozen){
            Freeze();
        }else{
            Thaw();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Freeze(){
        windSystem.Stop();
        iceSprite.enabled = true;
        typeVunerable = BurstAttack.EffectType.FIRE;
        isFrozen = true;
    }

    private void Thaw(){
        windSystem.Play();
        iceSprite.enabled = false;
        typeVunerable = BurstAttack.EffectType.ICE;
        isFrozen = false;
    }

    public override void takeDamage(BurstAttack.EffectType effectType, float damage)
    {
        if (effectType.Equals(typeVunerable))
        {
            // Reduce health
            currentHealth -= damage;

            // Destroy if below 0
            if(currentHealth <= 0) {
                if(isFrozen){
                    Thaw();
                }else{
                    Freeze();
                }
            }
        }else{
            currentHealth += damage;
        }
    }

    public void randomize(ProcGenLevel.EnemyPositionType placement){

        if(placement == ProcGenLevel.EnemyPositionType.TOP){
            transform.Rotate(0, 0, Random.Range(-90, 90));
        }else if(placement == ProcGenLevel.EnemyPositionType.BOTTOM){
            transform.Rotate(0, 0, Random.Range(90, 270));
        }else{
            Debug.Log("Should be top or bottom...");
        }

    }

    public void randomize(){

        

        if(Random.Range(0,2)>0){
            Thaw();
        }else{
            Freeze();
        }

        windStrength = Random.Range(minWindStrength, maxWindStrength);
        gameObject.GetComponent<WindHitboxBehaviour>().windStrength = windStrength;

    }
}
