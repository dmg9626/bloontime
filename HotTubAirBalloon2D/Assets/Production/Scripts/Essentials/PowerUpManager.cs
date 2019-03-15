using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            switch(n){
                case PowerUpName.WATERJETS:
                    displayName = "Water Jets";
                    break;
                case PowerUpName.TEMPREG:
                    displayName = "Temp Reg";
                    break;
                case PowerUpName.SNOCONE:
                    displayName = "Sno Cone";
                    break;
                case PowerUpName.HOTCOCOA:
                    displayName = "Hot Cocoa";
                    break;
                default:
                    displayName = "No Name";
                    break;
            }
        }
        public PowerUpName name;
        public string displayName;
        public bool isApplied;
    }

    public GameObject Player;
    public BalloonController BCtrl;

    public Button button1, button2;
    public Text button1Text, button2Text;
    public List<PowerUp> activePowerUps;

    public GM gm;

    // Start is called before the first frame update
    void Start()
    {
        //Player = gameObject;
        //Player = GameObject.FindGameObjectWithTag("Player");
        //BCtrl = Player.GetComponent<BalloonController>();
        Debug.Log(BCtrl.temperature);
        activePowerUps = new List<PowerUp>();
        setButtons();
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
    public void addPowerUp(int p)
    {
        PowerUp n = new PowerUp((PowerUpName)p);
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

    public void setButtons()
    {

        int rand1 = Random.Range(0,3);

        int rand2 = Random.Range(0,3);

        while(rand1==rand2){
            rand2 = Random.Range(0,3);
        }
        

        PowerUp p1 = new PowerUp((PowerUpName)rand1);
        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(delegate{this.choosePowerUp(p1.name);});
        button1Text.text = p1.displayName;

        PowerUp p2 = new PowerUp((PowerUpName)rand2);
        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(delegate{this.choosePowerUp(p2.name);});
        button2Text.text = p2.displayName;

    }

    public void choosePowerUp(PowerUpName p){

        addPowerUp(p);

        gm.NextLevel();

    }


}
