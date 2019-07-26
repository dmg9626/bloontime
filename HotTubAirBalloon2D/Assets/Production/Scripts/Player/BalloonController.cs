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

public class BalloonController : Singleton<BalloonController>
{
/**************************************************************************Public Fields */

    public float temperature;
    public float comfort;
    
    [Header("Temperature/Comfort Settings")]
    public float minTemperature;
    public float maxTemperature;
    public float tempMultiplier;
    
    public float
        minComfort,
        maxComfort,
        defaultMaxComfort;
    
    public float defaultComfortRegen;
    public float comfortRegen;
    public float comfortTemp;
    public float collisionBounceForce;
    private float collisionBounceForceStaticTemp;

    public float invulnerabilityTime;
    private float currentInvulnerabilityTime;
    private bool isInvulnerable = false;

    [Header("Passenger Values")]
    public int maxPass;
    public int currentPass;
    public int passLost;
    public Text passNumText;
    public GameObject passPrefab;
    public List<GameObject> passList;
    
    [Header("Fire/Ice Settings")]
    public float firePower;
    public float icePower;
    public float selfFirePowerMultiplier;
    public float selfIcePowerMultiplier;

    public float fireResist;
    public float iceResist;

    [Space(10)]
    public float collisionComfortLoss;
    public float tempSmoothTime;
    public float horizontalMomentumSmoothTime;
    public float verticalMomentumSmoothTime;

    public float verticalSpeedMax;
    public float horizontalSpeedMax;

    public bool bottomCollision;
    public bool topCollision;

    public delegate void OnTempChanged();
    public OnTempChanged onTempChanged;

    public delegate void OnComfortChanged();
    public OnComfortChanged onComfortChanged;

/****************************************************************************Private Fields */

    [Header("Movement")]
    [SerializeField]
    private float balloonVerticalSpeed;
    [SerializeField]
    private float balloonHorizontalSpeed;
    [SerializeField]
    private float balloonVerticalMomentum;
    [SerializeField]
    private float balloonHorizontalMomentum;

    private float horizontalDampVelocity = 0.0f;
    private float verticalDampVelocity = 0.0f;
    
    private Vector2 charPos;

    public SpriteRenderer playerSpriteRender;

/***************************************************************************Mono Methods */
    // Start is called before the first frame update
    void Start()
    {
        charPos = transform.position;
        StartCoroutine("regainComfort");
        StartCoroutine("regainTemperature");

        maxComfort = defaultMaxComfort;
        comfortRegen = defaultComfortRegen;

        // Initialize temperature/comfort to neutral values
        temperature = (minTemperature + maxTemperature) / 2;
        comfort = (minComfort + maxComfort) / 2;

        currentPass = maxPass;
        passNumText.text = "" + currentPass;

        collisionBounceForceStaticTemp = collisionBounceForce;

        for(int i = 0; i < currentPass; i++)
        {
            Vector2 pos = new Vector2(transform.position.x + .5f + Random.Range(-0.5f,0.5f), transform.position.y - .5f);
            GameObject p = (GameObject)Instantiate(passPrefab,pos,transform.rotation,transform);
            passList.Add(p);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BalloonMovement();
    }

/*************************************************************************Methods*/
    //controls the vertical movement of the ballon using temperature and preset horizontal temp
    void BalloonMovement()
    {
        balloonVerticalSpeed = Mathf.Clamp((temperature / tempMultiplier) + balloonVerticalMomentum, 0f-verticalSpeedMax, verticalSpeedMax);
        balloonVerticalMomentum = Mathf.SmoothDamp(balloonVerticalMomentum, 0f, ref verticalDampVelocity, verticalMomentumSmoothTime);

        collisionBounceForce = collisionBounceForceStaticTemp * Mathf.Lerp(0.5f, 1f, balloonVerticalSpeed/verticalSpeedMax);

        if(!bottomCollision || balloonVerticalSpeed > 0)
        {
            if((!topCollision && balloonVerticalSpeed > 0) || (balloonVerticalSpeed < 0))
                charPos.y += balloonVerticalSpeed;

            charPos.x += balloonHorizontalMomentum;

            balloonHorizontalMomentum = Mathf.SmoothDamp(balloonHorizontalMomentum, balloonHorizontalSpeed, ref horizontalDampVelocity, horizontalMomentumSmoothTime);
        }else{
            balloonHorizontalMomentum = 0;
        }

        transform.position = charPos;
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

    public void changeTemp(BurstAttack.EffectType effectType)
    {
        if(effectType.Equals(BurstAttack.EffectType.FIRE)) {
            changeTemp(firePower*selfFirePowerMultiplier);
        }
        else if(effectType.Equals(BurstAttack.EffectType.ICE)) {
            changeTemp(-icePower*selfIcePowerMultiplier);
        }
    }

    public void changeComfort(float comfortChange)
    {
        // Update comfort
        float newComfort = comfort + comfortChange;

        // Check if lost passengers
        if(newComfort <= 0 && currentPass > 0 && !isInvulnerable)
        {
            currentInvulnerabilityTime = invulnerabilityTime;
            isInvulnerable = true;
            addCurrentPassengers(-passLost);
            StartCoroutine(blinkInvulnerability());
        }

        //Make sure comfort doesn't go below 0 (done after passenger check so we know if we lost any)
        comfort = Mathf.Clamp(newComfort, minComfort, maxComfort);

        // Fire event
        onComfortChanged?.Invoke();
    }

    public void changeVerticalMomentum(float y){
        balloonVerticalMomentum += y;
    }

    public void changeHorizontalMomentum(float x){
        balloonHorizontalMomentum += x;
    }

    public void moveBalloon(Vector2 newPos){
        charPos = newPos;
        transform.position = charPos;
    }

    #region getters_setters

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

    public void addCurrentPassengers(int p)
    {
        if(currentPass > 0){
            currentPass += p;
            if(p < 0) passList[currentPass].GetComponent<PassengerManager>().fall();
        }
        passNumText.text = currentPass+"";
        if(currentPass <= 0)
            GameController.Instance.GameOver();
    }

    #endregion

    public void resetValues(){
        temperature = (minTemperature + maxTemperature) / 2;
        comfort = (minComfort + maxComfort) / 2;
        balloonHorizontalMomentum = 0;
        balloonVerticalMomentum = 0;
        for(int i = currentPass; i < maxPass; i++)
        {
            Vector2 pos = new Vector2(transform.position.x + Random.Range(-0.5f,0.5f), transform.position.y);
            GameObject p = (GameObject)Instantiate(passPrefab,pos,transform.rotation,transform);
        }
        currentPass = maxPass;
        passNumText.text = "" + currentPass;
        
    }

    IEnumerator blinkInvulnerability(){
        Color playerColor = playerSpriteRender.color;
        Color blinkColor = playerColor;
        blinkColor.a = 0.5f;

        while(currentInvulnerabilityTime > 0f){
            if((Mathf.Floor(currentInvulnerabilityTime*10f) % 3) == 0){
                playerSpriteRender.color = blinkColor;
            }else{
                playerSpriteRender.color = playerColor;
            }
            currentInvulnerabilityTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        playerSpriteRender.color = playerColor;
        isInvulnerable = false;
        currentInvulnerabilityTime = -1f;
    }
}
