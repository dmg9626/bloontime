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
    public GameObject BalloonChar;
    public float 
        temperature, 
        tempMultiplier, 
        comfort,
        comfortRegain,
        comfortTemp,
        maxComfort,
        cursorSpeed,
        collisionComfortLoss,
        tempSmoothTime;
        //, balloonSpeed;
    public bool bottomCollision, topCollision;

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
        StartCoroutine("regainComfort");
        StartCoroutine("regainTemperature");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BalloonMovement();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        changeComfort(collisionComfortLoss);
    }

/*************************************************************************Methods*/
    //controls the vertical movement of the ballon using temperature and preset horizontal temp
    void BalloonMovement()
    {
        balloonVerticalSpeed = temperature / tempMultiplier;

        if(!bottomCollision || balloonVerticalSpeed > 0)
        {
            if((!topCollision && balloonVerticalSpeed > 0) || (balloonVerticalSpeed < 0))
                charPos.y += balloonVerticalSpeed;

            charPos.x += balloonHorizontalSpeed;
        }

        BalloonChar.transform.position = charPos;
    }

    IEnumerator regainComfort(){
        while(true){
            if((temperature < comfortTemp) && (temperature > (0-comfortTemp) && (comfort < maxComfort))){
                comfort += comfortRegain;
            }  
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator regainTemperature(){
        float velocity = 0.0f;
        while(true){
            temperature = Mathf.SmoothDamp(temperature, 0f, ref velocity, tempSmoothTime);
            yield return new WaitForSeconds(0.1f);
        }
    }

/***************************************************************************Public Methods */
    //takes in temp to change temperature
    public void changeTemp(float tempChange)
    {
        temperature += tempChange;
    }

    public void changeTemp(ClickBurst.EffectType effectType)
    {
        if(effectType.Equals(ClickBurst.EffectType.FIRE)) {
            changeTemp(1);
        }
        else if(effectType.Equals(ClickBurst.EffectType.ICE)) {
            changeTemp(-1);
        }
    }

    public void changeComfort(float comfortChange)
    {
        comfort += comfortChange;
    }

    public void moveBalloon(Vector2 newPos){
        charPos = newPos;
    }
}
