using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public BalloonController BCtrl;
    public Slider tempSlider, comfortSlider;
    public Text tempNum, maxTemp, minTemp, comfortNum, maxComfort, minComfort, endText;
    public bool isEnd = false;
    public GameObject Cursor1, Cursor2, endMenu, pauseMenu;

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
        tempNum.text = BCtrl.temperature + "";
        maxTemp.text = tempSlider.maxValue + "";
        minTemp.text = tempSlider.minValue + "";

        comfortSlider.value = BCtrl.comfort;
        comfortNum.text = BCtrl.comfort + "";
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
            Cursor1.SetActive(false);
            Cursor2.SetActive(false);
            if(isEnd){
                endMenu.SetActive(true);
            }else{
                pauseMenu.SetActive(true);
            }
        }else{
            Time.timeScale = 1.0f;
            Cursor1.SetActive(true);
            Cursor2.SetActive(true);
            endMenu.SetActive(false);
            pauseMenu.SetActive(false);
        }   
    }
    
    public void Victory(){
        Debug.Log("We Win");
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
