using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

/*
 *  Hot Tub Air Balloon Prototype
 *  Josh Karmel (Copyright 2019)
 *  ~BalloonController.cs~
 */

public class BalloonController : MonoBehaviour
{
/**************************************************************************Public Fields */
    public GameObject BalloonChar;

    public float temperature,
        minTemperature,
        maxTemperature;

    public float
        comfort,
        minComfort,
        defaultMaxComfort,
        maxComfort,
        firePower,
        icePower,
        fireResist,
        iceResist;

    public float
        tempMultiplier,
        defaultComfortRegen,
        comfortRegen,
        comfortTemp;

    public float
        cursorSpeed,
        collisionComfortLoss,
        tempSmoothTime;
        //, balloonSpeed;

    public bool bottomCollision, topCollision;

    public delegate void OnTempChanged();
    public OnTempChanged onTempChanged;

    public delegate void OnComfortChanged();
    public OnComfortChanged onComfortChanged;

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

        maxComfort = defaultMaxComfort;
        comfortRegen = defaultComfortRegen;

        // Initialize temperature/comfort to neutral values
        temperature = (minTemperature + maxTemperature) / 2;
        comfort = (minComfort + maxComfort) / 2;
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
            if((temperature < comfortTemp) && (temperature > (-comfortTemp) 
                && (comfort < maxComfort))){
                comfort += comfortRegen;

                // Fire comfort change events
                onComfortChanged?.Invoke();
            }  
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator regainTemperature(){
        float velocity = 0.0f;
        while(true){
            temperature = Mathf.SmoothDamp(temperature, 0f, ref velocity, tempSmoothTime);

            // Fire temperature change events
            onTempChanged?.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
    }

/***************************************************************************Public Methods */
    //takes in temp to change temperature
    public void changeTemp(float tempChange)
    {
        // Update temperature
        temperature += tempChange;

        // Fire event
        onTempChanged?.Invoke();
    }

    public void changeTemp(ClickBurst.EffectType effectType)
    {
        if(effectType.Equals(ClickBurst.EffectType.FIRE)) {
            changeTemp(firePower);
        }
        else if(effectType.Equals(ClickBurst.EffectType.ICE)) {
            changeTemp(-icePower);
        }
    }

    public void changeComfort(float comfortChange)
    {
        // Update comfort
        comfort += comfortChange;

        // Fire event
        onComfortChanged?.Invoke();
    }

    public void moveBalloon(Vector2 newPos){
        charPos = newPos;
        BalloonChar.transform.position = charPos;
    }

    public void setFirePower(float f){
        firePower = f;
    }

    public void setIcePower(float i){
        icePower = i;
    }

    public void setMaxComfort(float c){
        maxComfort = c;
    }

    public void setComfortRegen(float r){
        comfortRegen = r;
    }

    public void setIceRes(float r){
        iceResist = r;
    }

    public void setFireRes(float r){
        fireResist = r;
    }
    
    public float getMaxComfort(){
        return maxComfort;
    }

    public float getComfortRegen(){
        return comfortRegen;
    }

    public float getIceResist(){
        return iceResist;
    }

    public float getFireResist(){
        return fireResist;
    }

    public float getDefaultRegen(){
        return defaultComfortRegen;
    }

    public float getDefaultMaxComfort(){
        return defaultMaxComfort;
    }

    public float GetScaledTemp()
    {
        return (temperature - minTemperature) / (maxTemperature - minTemperature);
    }

    public float GetScaledComfort()
    {
        return (comfort - minComfort) / (maxComfort - minComfort);
    }

    public void resetValues(){
        temperature = (minTemperature + maxTemperature) / 2;
        comfort = (minComfort + maxComfort) / 2;
    }
}
