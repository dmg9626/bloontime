using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindHitboxBehaviour : MonoBehaviour
{
    public float windStrength;
    public FanBehaviour myFan;
    public Transform fanTransform;

    void Start()
    {
        windStrength = myFan.windStrength;
    }

    private void OnCollisionStay2D(Collision2D obj)
    {
        if(!myFan.isFrozen){
            if(LayerMask.LayerToName(obj.gameObject.layer) == "Player")
            {
                float deltaDistance = Mathf.Abs(Vector2.Distance(BalloonController.Instance.transform.position, fanTransform.position));
                BalloonController.Instance.changeHorizontalMomentum((0 - transform.up.x)*windStrength*(1/deltaDistance)*Time.fixedDeltaTime);
                BalloonController.Instance.changeVerticalMomentum((0 - transform.up.y)*windStrength*(1/deltaDistance)*Time.fixedDeltaTime);
            }
        }
    }

}
