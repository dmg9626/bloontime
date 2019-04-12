using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCustom : MonoBehaviour
{

    public virtual void Activate()
    {
        // Derived class should overrie this method with powerup code
        throw new NotImplementedException();
    }
}
