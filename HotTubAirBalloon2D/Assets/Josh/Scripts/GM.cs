using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public BalloonController BCtrl;
    public Slider tempSlider;
    public Text tempNum, maxTemp, minTemp;

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
    }
}
