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

    public bool isPaused;

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
        comfortSlider.value = BCtrl.comfort;

        if(BCtrl.comfort <= 0){
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
    }
    
    public void UnFreeze(){
        Time.timeScale = 1.0f;
        // Player1.SetActive(true);
        // Player2.SetActive(true);
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

    public void NextLevel(){
        procGen.NextLevel();
        victoryMenu.SetActive(false);
        isEnd = false;
        UnFreeze();
    }

    public void Quit(){
        Application.Quit();
    }


}
