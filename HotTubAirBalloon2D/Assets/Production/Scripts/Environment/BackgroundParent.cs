using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParent : Singleton<BackgroundParent>
{

    public ScrollingParallaxBackground[] children;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetBackgroundToCamera(){
       
        //transform.position = Camera.main.transform.position;
         
        foreach (ScrollingParallaxBackground child in children)
        {
            child.resetBackgroundToCamera();
        }
        
    }
}
