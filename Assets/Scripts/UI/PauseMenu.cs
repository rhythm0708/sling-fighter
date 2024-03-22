using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause Xbox")) 
        {
            if (isPaused)
            {
                ResumeGame();
                
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pause the game
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    // Resume game upon pressing esc in pause, or pressing resume
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Restart Game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Go into Settings
    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Quit to go back into main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // From Settings Scene go back
    public void Back()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
