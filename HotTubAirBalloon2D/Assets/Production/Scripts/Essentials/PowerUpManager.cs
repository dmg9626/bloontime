using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public enum PowerUpName
    {
        WATERJETS,
        TEMPREG,
        SNOCONE,
        HOTCOCOA
    };

    [System.Serializable]
    public class PowerUp
    {
        public PowerUpName name;

        public string displayName;
        public string description;

        public bool isApplied;
    }

    public BalloonController BCtrl;

    /// <summary>
    /// Base settings for powerups
    /// </summary>
    public List<PowerUp> powerUps;
    
    /// <summary>
    /// Powerups on balloon
    /// </summary>
    [SerializeField]
    private List<PowerUp> activePowerUps;

    public GM gm;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(BCtrl.temperature);
        activePowerUps = new List<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            addPowerUp(PowerUpName.WATERJETS);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            addPowerUp(PowerUpName.TEMPREG);
        if(Input.GetKeyDown(KeyCode.Alpha3))
            addPowerUp(PowerUpName.HOTCOCOA);
        if(Input.GetKeyDown(KeyCode.Alpha4))
            addPowerUp(PowerUpName.SNOCONE);
    }

    public void addPowerUp(PowerUpName p)
    {
        // Add to active powerup list if not already there
        PowerUp n = powerUps.First(pow => pow.name.Equals(p));
        if(!activePowerUps.Contains(n))
            activePowerUps.Add(n);

        // Apply powerups
        checkPowerUps();
    }
    public void addPowerUp(int p)
    {
        addPowerUp((PowerUpName)p);
    }


    public List<PowerUp> getPowerUps()
    {
        return activePowerUps;
    }

    void checkPowerUps(){
        activePowerUps.ForEach(e =>{
            if(!e.isApplied)
            {
                switch(e.name)
                {
                    case PowerUpName.WATERJETS:
                        float newMax = (float)(Mathf.RoundToInt(BCtrl.getDefaultMaxComfort() * 1.5f));
                        BCtrl.setMaxComfort(newMax);
                        float newRegen = BCtrl.getDefaultRegen() * 1.5f;
                        BCtrl.setComfortRegen(newRegen);
                        e.isApplied = true;
                        break;
                    case PowerUpName.TEMPREG:
                        BCtrl.setFireRes(BCtrl.getFireResist() + 1);
                        BCtrl.setIceRes(BCtrl.getIceResist() + 1);
                        e.isApplied = true;
                        break;
                    case PowerUpName.HOTCOCOA:
                        BCtrl.setFirePower(2);
                        e.isApplied = true;
                        break;
                    case PowerUpName.SNOCONE:
                        BCtrl.setIcePower(2);
                        e.isApplied = true;
                        break;
                    default: break;
                }
            }
        });
    }

    


}
