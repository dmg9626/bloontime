﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public BalloonController BCtrl;
    public Slider tempSlider, comfortSlider;
    public Text tempNum, maxTemp, minTemp, comfortNum, maxComfort, minComfort, endText;
    public bool isEnd = false;
    public GameObject Player1, Player2, endMenu, pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(Time.timeScale == 0.0f){
            Pause();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempSlider.value = BCtrl.temperature;
        tempNum.text = BCtrl.temperature.ToString("F2");
        maxTemp.text = tempSlider.maxValue + "";
        minTemp.text = tempSlider.minValue + "";

        comfortSlider.value = BCtrl.comfort;
        comfortNum.text = BCtrl.comfort.ToString("F2");
        maxComfort.text = comfortSlider.maxValue + "";
        minComfort.text = comfortSlider.minValue + "";

        if(BCtrl.comfort <= 0){
            GameOver();
        }
    }

    void Update(){

        if((Input.GetKeyDown("escape")) && (!isEnd))     
        {
            Pause();     
        }

    }

    public void Pause(){
        if (Time.timeScale == 1.0f) {           
            Time.timeScale = 0.0f;
            Player1.SetActive(false);
            Player2.SetActive(false);
            if(isEnd){
                endMenu.SetActive(true);
            }else{
                pauseMenu.SetActive(true);
            }
        }else{
            Time.timeScale = 1.0f;
            Player1.SetActive(true);
            Player2.SetActive(true);
            endMenu.SetActive(false);
            pauseMenu.SetActive(false);
        }   
    }
    
    public void Victory(){
        isEnd = true;
        endText.text = "You Win";
        Pause();
    }

    public void GameOver(){
        endText.text ="Game Over";
        isEnd = true;
        Pause();
    }

    public void Retry(){
        Application.LoadLevel(Application.loadedLevel);

    }

    public void Quit(){
        Application.Quit();
        
    }


}
