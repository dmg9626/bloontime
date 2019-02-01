using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonController : MonoBehaviour
{
    public GameObject BalloonChar;
    public float temperature, tempMultiplier;//, balloonSpeed;

    [SerializeField]
    private float balloonVerticalSpeed, balloonHorizontalSpeed;
    [SerializeField]
    private Vector3 charPos;

    // Start is called before the first frame update
    void Start()
    {
        charPos = BalloonChar.transform.position;
        balloonHorizontalSpeed = .005f;
    }

    // Update is called once per frame
    void Update()
    {
        balloonVerticalSpeed = temperature / tempMultiplier;
        charPos.y += balloonVerticalSpeed;

        charPos.x += balloonHorizontalSpeed;

        BalloonChar.transform.position = charPos;
    }
}
