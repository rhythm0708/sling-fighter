using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private GameObject settingsMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

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

        // Shortcut for restarting levels.
        // TODO: Add Xbox buttons.
        if(Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.RetryWave();
        }
    }

    // Pause the game
    public void PauseGame()
    {
        pauseMenu.SetActive(true);

        // Disable player controller.
        GameManager.Instance.player.enabled = false;
        GameManager.Instance.player.gameObject.GetComponent<PlayerMovement>().enabled = false;
        isPaused = true;
    }
    
    // Resume game upon pressing esc in pause, or pressing resume
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);

        // Enable player controller.
        GameManager.Instance.player.enabled = true;
        GameManager.Instance.player.gameObject.GetComponent<PlayerMovement>().enabled = true;
        isPaused = false;
    }

    // Restart Game
    public void Restart()
    {
        GameManager.Instance.RetryWave();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // pauseMenu.SetActive(false);
        // isPaused = false;
    }

    // Go into Settings
    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        isPaused = true;
    }

    // Quit to go back into main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // From Settings Scene go back
    public void Back()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        isPaused = true;
    }
}
