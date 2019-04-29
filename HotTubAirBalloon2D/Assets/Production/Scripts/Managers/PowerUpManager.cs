using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    /// <summary>
    /// Powerup UI
    /// </summary>
    public List<Image> powerUpSlots;
    public Sprite inactiveIcon;

    void Start()
    {
        Debug.Log(BalloonController.Instance.temperature);

        powerUpSlots.ForEach (p => {

        });

        // Activate any powerups set on start
        checkPowerUps();
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
    public void checkPowerUps(){
        activePowerUps.Where(p => !p.isApplied).ToList()
        .ForEach(powerUp => {
            powerUp.Activate();
        });
        string dlog = "";
        for(int i = 0; i < activePowerUps.Count; i++)
        {
            powerUpSlots[i].sprite = activePowerUps[i].icon;
            dlog += activePowerUps[i].type.ToString() + ", ";
        }
        Debug.Log(dlog);
    }

}
