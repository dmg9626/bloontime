using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  Hot Tub Air Balloon Prototype
 *  Josh Karmel (Copyright 2019)
 *  ~BalloonController.cs~
 */

public class BalloonController : MonoBehaviour
{
/**************************************************************************Public Fields */
    public GameObject BalloonChar, cursor;
    public ClickBurst clickBurst;
    public float temperature, tempMultiplier, cursorSpeed;//, balloonSpeed;
    public bool bottomCollision;

/****************************************************************************Private Fields */
    [SerializeField]
    private float balloonVerticalSpeed, balloonHorizontalSpeed;
    [SerializeField]
    private Vector2 charPos;

/***************************************************************************Mono Methods */
    // Start is called before the first frame update
    void Start()
    {
        charPos = BalloonChar.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BalloonMovement();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
    }

/*************************************************************************Methods*/
    //controls the vertical movement of the ballon using temperature and preset horizontal temp
    void BalloonMovement()
    {
        balloonVerticalSpeed = temperature / tempMultiplier;

        if(!bottomCollision || balloonVerticalSpeed > 0)
        {
            charPos.y += balloonVerticalSpeed;

            charPos.x += balloonHorizontalSpeed;
        }

        BalloonChar.transform.position = charPos;
    }

/***************************************************************************Public Methods */
    //takes in temp to change temperature
    public void changeTemp(float tempChange)
    {
        temperature += tempChange;
    }
}
