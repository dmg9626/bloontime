using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public enum PowerUp
    {
        COMFORTREG,
        TEMPREG,
        SNOCONE,
        HOTCOCOA
    };

    public GameObject Player;
    public BalloonController BCtrl;
    public List<PowerUp> activePowerUps;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        BCtrl = Player.GetComponent<BalloonController>();
        activePowerUps = new List<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Power Up"))
            addPowerUp(PowerUp.COMFORTREG);
    }

    public void addPowerUp(PowerUp p)
    {
        if(!activePowerUps.Contains(p))
            activePowerUps.Add(p);
    }

    public void checkPowerUps()
    {

    }
}
