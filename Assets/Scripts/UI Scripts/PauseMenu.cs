using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    private string previousScene;

    // Start is called before the first frame update
    void Start()
    {
        // Store the name of the initial scene
        previousScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
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

    public void PauseGame()
    {
        SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // Unload the pause menu scene
        SceneManager.UnloadSceneAsync("Pause Menu");

        // Load back the previous scene
        SceneManager.LoadScene(previousScene);
    }
}
