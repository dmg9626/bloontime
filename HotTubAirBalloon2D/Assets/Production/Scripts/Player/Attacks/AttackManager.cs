using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;

public class AttackManager : MonoBehaviour
{
    public BeamAttack beam;
    public Transform cursor;
    public BurstAttack burstAttack;

    public void HandleBeam(bool activate)
    {
        if(activate)
        {
            beam.SetActive(true);

            Vector2 cursorPos = cursor.position;
            beam.FireBeam();
        }
        else beam.ReleaseBeam();
    }

    public void HandleBurstAttack()
    {
        burstAttack.Burst(cursor.position);
    }
}
