using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public BalloonController BCtrl;
    public Slider comfortSlider;
    public Text endText;
    public bool isEnd = false;
    public GameObject Player1, Player2, endMenu, pauseMenu, victoryMenu;
    public ProcGenLevel procGen;

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
        // Update slider values
        comfortSlider.value = BCtrl.comfort;

        if(BCtrl.comfort <= 0){
            GameOver();
        }
    }

    void Update(){

        if((Input.GetKeyDown("escape")) && (!isEnd))     
        {
            if(Time.timeScale == 1.0f){
                if(!isEnd){
                    Pause();
                    pauseMenu.SetActive(true);
                }
            }else{
                UnPause();
                pauseMenu.SetActive(false);
            }
                
        }

    }

    public void Pause(){
        Time.timeScale = 0.0f;
        Player1.SetActive(false);
        Player2.SetActive(false);
    }
    
    public void UnPause(){
        Time.timeScale = 1.0f;
        Player1.SetActive(true);
        Player2.SetActive(true);
    }
    
    public void Victory(){
        victoryMenu.SetActive(true);
        isEnd = true;
        Pause();
    }

    public void GameOver(){
        endMenu.SetActive(true);
        isEnd = true;
        Pause();
    }

    public void Retry(){
        UnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        UnPause();
    }

    public void NextLevel(){
        procGen.NextLevel();
        victoryMenu.SetActive(false);
        isEnd = false;
        UnPause();
    }

    public void Quit(){
        Application.Quit();
    }


}
