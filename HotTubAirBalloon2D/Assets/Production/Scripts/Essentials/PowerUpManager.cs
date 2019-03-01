using System.Collections;
using System.Collections.Generic;
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

    public class PowerUp
    {
        public PowerUp(PowerUpName n){
            name = n;
            isApplied = false;
        }
        public PowerUpName name;
        public bool isApplied;
    }

    public GameObject Player;
    public BalloonController BCtrl;
    public List<PowerUp> activePowerUps;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        BCtrl = Player.GetComponent<BalloonController>();
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
        PowerUp n = new PowerUp(p);
        if(!activePowerUps.Contains(n))
            activePowerUps.Add(n);
        checkPowerUps();
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
