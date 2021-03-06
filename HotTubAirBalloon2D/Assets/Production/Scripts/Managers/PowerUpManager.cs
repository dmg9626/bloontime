﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpManager : Singleton<PowerUpManager>
{
    /// <summary>
    /// Settings for each powerup stored here
    /// </summary>
    public List<PowerUp> powerUps;

    /// <summary>
    /// Powerups currently applied to balloon
    /// </summary>
    [SerializeField]
    private List<PowerUp> activePowerUps;

    void Start()
    {
        Debug.Log(BalloonController.Instance.temperature);

        // Activate any powerups set on start
        checkPowerUps();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            addPowerUp(PowerUp.PowerUpType.WATERJETS);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            addPowerUp(PowerUp.PowerUpType.TEMPREG);
        if(Input.GetKeyDown(KeyCode.Alpha3))
            addPowerUp(PowerUp.PowerUpType.HOTCOCOA);
        if(Input.GetKeyDown(KeyCode.Alpha4))
            addPowerUp(PowerUp.PowerUpType.SNOCONE);
    }

    public void addPowerUp(PowerUp.PowerUpType p)
    {
        // Add to active powerup list if not already there
        PowerUp n = powerUps.First(pow => pow.type.Equals(p));
        if(!activePowerUps.Contains(n))
            activePowerUps.Add(n);

        // Apply powerups
        checkPowerUps();
    }


    public List<PowerUp> getPowerUps()
    {
        return activePowerUps;
    }

    /// <summary>
    /// Checks list of active powerups and applies new ones
    /// </summary>
    void checkPowerUps(){
        activePowerUps.Where(p => !p.isApplied).ToList()
        .ForEach(powerUp => powerUp.Activate());
    }

}
