using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PowerUp
{
    public enum PowerUpType
    {
        WATERJETS,
        TEMPREG,
        SNOCONE,
        HOTCOCOA
    };
    
    public PowerUpType type;

    public string displayName;
    public string description;

    public bool isApplied;

    public Effects effects;

    public Sprite icon;

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

    public PowerUpCustom customPowerUp;

    /// <summary>
    /// Can be overriden by derived class
    /// </summary>
    public virtual void Activate()
    {
        // Fallback check to ensure not applied
        if(!isApplied) {
            BalloonController balloonController = BalloonController.Instance;

            Debug.Log("Applying powerup " + type);            
            
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

            // Call custom powerup script if provided
            if(customPowerUp != null) {
                customPowerUp.Activate();
            }

            // Flag as applied so we don't activate it again
            isApplied = true;
        }
    }
}
