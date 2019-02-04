using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public BalloonController BCtrl;
    public Slider tempSlider, comfortSlider;
    public Text tempNum, maxTemp, minTemp, comfortNum, maxComfort, minComfort;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempSlider.value = BCtrl.temperature;
        tempNum.text = BCtrl.temperature + "";
        maxTemp.text = tempSlider.maxValue + "";
        minTemp.text = tempSlider.minValue + "";

        comfortSlider.value = BCtrl.comfort;
        comfortNum.text = BCtrl.comfort + "";
        maxComfort.text = comfortSlider.maxValue + "";
        minComfort.text = comfortSlider.minValue + "";

        if(BCtrl.comfort <= 0){
            GameOver();
        }
    }
    
    public void Victory(){
        Debug.Log("We Win");
    }

    public void GameOver(){
        Debug.Log("You Lost Nerd");
    }


}
