using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button button1, button2;
    public Text button1Text, button2Text;

    public PowerUpManager powerUpManager;

    public GM gm;


    // Start is called before the first frame update
    void Start()
    {
        setButtons();
    }

    public void setButtons()
    {

        int rand1 = Random.Range(0, 3);

        int rand2 = Random.Range(0, 3);

        while (rand1 == rand2)
        {
            rand2 = Random.Range(0, 3);
        }

        PowerUpManager.PowerUp p1 = new PowerUpManager.PowerUp((PowerUpManager.PowerUpName)rand1);
        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(delegate { choosePowerUp(p1.name); });
        button1Text.text = p1.displayName;

        PowerUpManager.PowerUp p2 = new PowerUpManager.PowerUp((PowerUpManager.PowerUpName)rand2);
        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(delegate { choosePowerUp(p2.name); });
        button2Text.text = p2.displayName;

    }

    public void choosePowerUp(PowerUpManager.PowerUpName p)
    {
        powerUpManager.addPowerUp(p);
        gm.NextLevel();
    }
}
