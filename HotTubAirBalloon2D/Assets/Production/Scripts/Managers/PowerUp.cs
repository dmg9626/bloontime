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
    /// All values applied as percent changes.
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
}
