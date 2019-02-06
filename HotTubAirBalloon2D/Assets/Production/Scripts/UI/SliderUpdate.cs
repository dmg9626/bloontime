using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdate : MonoBehaviour
{
    public Gradient gradient;

    public Slider slider;

    public Image sliderBG;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Slider value start: " + GetSliderValue01(slider.value));
        sliderBG.color = gradient.Evaluate(GetSliderValue01(slider.value));

        slider.onValueChanged.AddListener(SetSliderColor);
    }

    float GetSliderValue01(float value)
    {
        float value01 = value / Mathf.Abs(slider.maxValue - slider.minValue);
        return value01;
    }

    public void SetSliderColor(float value)
    {
        sliderBG.color = gradient.Evaluate(GetSliderValue01(slider.value));
    }
}
