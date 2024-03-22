using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationQuit : MonoBehaviour
{
    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Fail-safe escape for Main Menus.
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(currentScene)
            {
                case "Main Menu":
                    Application.Quit();
                    break;
                case "Settings":
                    SceneManager.LoadScene("Main Menu");
                    break;
                default:
                    Debug.Log($"You cannot quit from the scene {currentScene}!");
                    break;
            }
            
        }
    }
}
