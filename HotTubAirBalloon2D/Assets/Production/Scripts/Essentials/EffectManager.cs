using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Beam effect settings
    
    [System.Serializable]
    public class BeamColorSettings
    {
        public Gradient hitEffectGradient;
    }

    public BeamColorSettings fireBeamColorSettings;

    public BeamColorSettings iceBeamColorSettings;


    // Burst effect settings
    public Gradient fireBurstGradient;

    public Gradient iceBurstGradient;
}
