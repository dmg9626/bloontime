using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonController : MonoBehaviour
{
    public GameObject BalloonChar;
    public float temperature, tempMultiplier;//, balloonSpeed;

    [SerializeField]
    private float balloonSpeed;

    private Vector3 charPos;

    // Start is called before the first frame update
    void Start()
    {
        charPos = BalloonChar.transform.position;
        balloonSpeed = temperature / tempMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        charPos.y += balloonSpeed;
        BalloonChar.transform.position = new Vector3(charPos.x, charPos.y, charPos.z);
    }
}
