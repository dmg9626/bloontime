using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{

    public float tempChange, comfortChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getTemp()
    {
        return tempChange;
    }

    public float getComfort()
    {
        return comfortChange;
    }

    public void setTemp(float newTemp)
    {
        tempChange = newTemp;
    }

    public void setComfort(float newComfort)
    {
        comfortChange = newComfort;
    }
}
