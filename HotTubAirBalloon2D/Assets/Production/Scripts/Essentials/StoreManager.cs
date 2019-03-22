using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : Singleton<StoreManager>
{
    public Button button1, button2;
    public Text button1Text, button2Text;
    public Text description1, description2;

    public PowerUpManager powerUpManager;

    public GM gm;


    // Start is called before the first frame update
    void Start()
    {
        setButtons();
    }

    public void setButtons()
    {
        // TODO: remove hardcoded range (0,3), replace with actual number of powerups
        // TODO: make this a method on PowerUpManager
        int rand1 = Random.Range(0, 3);
        int rand2 = Random.Range(0, 3);

        while (rand1 == rand2)
        {
            rand2 = Random.Range(0, 3);
        }

        PowerUpManager.PowerUp p1 = powerUpManager.powerUps[rand1];
        PowerUpManager.PowerUp p2 = powerUpManager.powerUps[rand2];

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(delegate { choosePowerUp(p1.name); });
        button1Text.text = p1.displayName;
        description1.text = p1.description;

        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(delegate { choosePowerUp(p2.name); });
        button1Text.text = p1.displayName;
        description2.text = p2.description;
    }

    public void choosePowerUp(PowerUpManager.PowerUpName p)
    {
        powerUpManager.addPowerUp(p);
        gm.NextLevel();
    }
}
