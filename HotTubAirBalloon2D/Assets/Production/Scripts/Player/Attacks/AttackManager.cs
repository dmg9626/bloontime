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
        beam.FireBeam(activate);
    }

    public void HandleBurstAttack()
    {
        burstAttack.Burst(burstAttack.transform.position);
    }
}
