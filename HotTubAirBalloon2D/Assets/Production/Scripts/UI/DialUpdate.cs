using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DialUpdate : MonoBehaviour
{
    public RectTransform needle;

    public BalloonController balloonController;

    public float minAngle;

    public float maxAngle;

    public enum Type { TEMPERATURE, COMFORT };
    public Type dialType;

    void Start()
    {
        switch(dialType) {
            case Type.TEMPERATURE:
                balloonController.onTempChanged += UpdateDial;
                break;

            case Type.COMFORT:
                balloonController.onComfortChanged += UpdateDial;
                break;
        }

        // Set needle to center of dial
        float angle = (minAngle + maxAngle) / 2;
        needle.rotation = Quaternion.Euler(0, 0, angle);
    }

    void UpdateDial()
    {
        // Get temperature scaled between 0 and 1
        float scaledTemp = balloonController.GetScaledTemp();

        // Lerp angle between min/max angle values using scaled temp
        float angle = Mathf.Lerp(minAngle, maxAngle, scaledTemp);

        // Set needle rotation on dial
        needle.rotation = Quaternion.Euler(0, 0, angle);
    }
}
