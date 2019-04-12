using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// sample implementation of a powerup with custom scripting
public class TestPowerUp : PowerUpCustom
{
    override
    public void Activate()
    {
        // Powerup code goes here
        Debug.Log("TestPowerUp.Activate()");
    }
}
