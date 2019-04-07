using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Slider comfortSlider;
    public Text endText;
    public bool isEnd = false;
    public GameObject Player1, Player2, endMenu, pauseMenu, victoryMenu, powerUpMenu;

    public bool isPaused;
    public bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        if(Time.timeScale == 0.0f){
            Freeze();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update slider values
        comfortSlider.value = BalloonController.Instance.comfort;

        if(BalloonController.Instance.comfort <= 0){
            GameOver();
        }
    }

    void Update(){
        if(Input.GetKeyDown("escape"))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if(!isEnd){
            if(!isPaused){
                Freeze();
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            else{
                UnFreeze();
                pauseMenu.SetActive(false);
                isPaused = false;
            }
        }
            
    }

    public void Freeze(){
        Time.timeScale = 0.0f;
        //Player1.SetActive(false);
        //Player2.SetActive(false);
        isFrozen = true;
    }
    
    public void UnFreeze(){
        Time.timeScale = 1.0f;
        // Player1.SetActive(true);
        // Player2.SetActive(true);
        isFrozen = false;
    }
    
    public void Victory(){
        victoryMenu.SetActive(true);
        isEnd = true;
        Freeze();
    }

    public void GameOver(){
        endMenu.SetActive(true);
        isEnd = true;
        Freeze();
    }

    public void Retry(){
        UnFreeze();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        UnFreeze();
    }

    public void NextLevel(){
        ProcGenLevel.Instance.NextLevel();
        victoryMenu.SetActive(false);
        powerUpMenu.SetActive(false);
        isEnd = false;
        UnFreeze();
    }

    public void toPowerUp(){
        victoryMenu.SetActive(false);
        powerUpMenu.SetActive(true);
    }

    public void Quit(){
        Application.Quit();
    }


}
