using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public enum Scene {
        MAIN_MENU,
        GAME
    }

    public Scene sceneToOpen;

    public Dictionary<Scene, string> sceneDict = new Dictionary<Scene, string>() {
        {Scene.MAIN_MENU, "MainMenu"},
        {Scene.GAME, "ProcGenTest"}
    };

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Load);
    }
    
    public void Load()
    {
        SceneManager.LoadScene(sceneDict[sceneToOpen]);
    }
}
