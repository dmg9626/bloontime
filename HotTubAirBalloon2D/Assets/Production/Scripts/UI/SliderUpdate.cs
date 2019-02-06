using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SliderUpdate - Sets sliderBG color based on slider value and corresponding gradient key. Calls SetSliderColor() each time slider value changes.
// Ex. if slider has min 0 and max 10, a slider value of 5 would set sliderBG color to the middle color on the gradient (where key = .5)
// If slider has min -10 and max 10, a value of 10 would set the color to the rightmost color on gradient (where key = 1)
public class SliderUpdate : MonoBehaviour
{
    public Gradient gradient;

    public Slider slider;

    public Image sliderBG;

    // Start is called before the first frame update
    void Start()
    {
        // Set color at start
        sliderBG.color = gradient.Evaluate(GetSliderValue01(slider.value));

        // Call SetSliderColor() with slider value each time value changes
        slider.onValueChanged.AddListener(SetSliderColor);
    }

    /// <summary>
    /// Takes raw slider value and returns position between 0 and 1 (pretend 0 is min bound and 1 is max bound)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    float GetSliderValue01(float value)
    {
        float value01 = value / (slider.maxValue + slider.minValue);
        Debug.Log(name + " | value / (max / min) = " + value01);
        return value01;
    }

    /// <summary>
    /// Called each time slider value changes
    /// Sets sliderBG color to the color found at gradient key matching slider value within its bounds (terrible explanation sorry)
    /// </summary>
    /// <param name="value"></param>
    public void SetSliderColor(float value)
    {
        sliderBG.color = gradient.Evaluate(GetSliderValue01(slider.value));
    }
}
