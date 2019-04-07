using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUp
{
    public enum PowerUpName
    {
        WATERJETS,
        TEMPREG,
        SNOCONE,
        HOTCOCOA
    };
    
    public PowerUpName name;

    public string displayName;
    public string description;

    public bool isApplied;

    public Effects effects;

    /// <summary>
    /// Values used to apply powerup effects.
    /// All values should be applied as percent changes.
    /// </summary>
    [System.Serializable]
    public class Effects 
    {
        /// <summary>
        /// Change applied to fire power
        /// </summary>
        public float firePowerChange;

        /// <summary>
        /// Change applied to ice power
        /// </summary>
        public float icePowerChange;

        /// <summary>
        /// Change applied to fire resistance
        /// </summary>
        public float fireResistanceChange;

        /// <summary>
        /// Change applied to ice resistance
        /// </summary>
        public float iceResistanceChange;

        /// <summary>
        /// Change applied to comfort regeneration rate
        /// </summary>
        public float comfortRegenChange;

        /// <summary>
        /// New max comfort
        /// </summary>
        public float maxComfort;
    }


    public void Activate()
    {
        // Fallback check to ensure not applied
        if(!isApplied) {
            BalloonController balloonController = BalloonController.Instance;

            Debug.Log("Applying powerup " + name);            
            
            float defaultMaxComfort = balloonController.getDefaultMaxComfort();
            float defaultComfortRegen = balloonController.getDefaultRegen();
            
            // Calculate comfort values (scaled)
            float maxComfort = (float)(Mathf.RoundToInt((defaultMaxComfort * effects.maxComfort)) + defaultMaxComfort);
            float comfortRegen = (defaultComfortRegen * effects.comfortRegenChange) + defaultComfortRegen;
            
            // Calculate resistance values (additive)
            float fireRes = balloonController.getFireResist() + effects.fireResistanceChange;
            float iceRes = balloonController.getIceResist() + effects.iceResistanceChange;

            // Calculate power values (additive)
            float firePower = balloonController.firePower + effects.firePowerChange;
            float icePower = balloonController.icePower + effects.icePowerChange;
            
            // Set max comfort/regeneration rate
            balloonController.setMaxComfort(maxComfort);
            balloonController.setComfortRegen(comfortRegen);

            // Set fire/ice resistance
            balloonController.setFireRes(fireRes);
            balloonController.setIceRes(iceRes);

            // Set fire/ice power
            balloonController.setFirePower(firePower);
            balloonController.setIcePower(icePower);

            // Flag as applied so we don't activate it again
            isApplied = true;
        }
    }
}
