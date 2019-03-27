using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DialUpdate : MonoBehaviour
{
    RectTransform needle;

    public float minAngle;

    public float maxAngle;

    public enum Type { TEMPERATURE, COMFORT };
    public Type dialType;

    void Start()
    {
        switch(dialType) {
            case Type.TEMPERATURE:
                BalloonController.Instance.onTempChanged += UpdateDial;
                break;

            case Type.COMFORT:
                BalloonController.Instance.onComfortChanged += UpdateDial;
                break;
        }

        needle = GetComponent<RectTransform>();

        // Set needle to center of dial
        float angle = (minAngle + maxAngle) / 2;
        needle.rotation = Quaternion.Euler(0, 0, angle);
    }

    void UpdateDial()
    {
        // Get temperature scaled between 0 and 1
        float scaledTemp = BalloonController.Instance.GetScaledTemp();

        // Lerp angle between min/max angle values using scaled temp
        float angle = Mathf.Lerp(minAngle, maxAngle, scaledTemp);

        // Set needle rotation on dial
        needle.rotation = Quaternion.Euler(0, 0, angle);
    }
}
