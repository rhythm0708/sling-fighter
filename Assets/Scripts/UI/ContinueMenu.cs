using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueMenu;
    public static ContinueMenu instance;

    void Start()
    {
        continueMenu.SetActive(false);
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Show the continue menu
    public void ShowContinueMenu()
    {
        continueMenu.SetActive(true);
        // Pause the game
        Time.timeScale = 0f; 

        SoundManager.instance.PlaySfx("Continue");
        SoundManager.instance.StopSfx("Rope");
        SoundManager.instance.StopMusic("In Game");
    }

    // Hide the continue menu and resume the game
    public void HideContinueMenu()
    {
        continueMenu.SetActive(false);
        // Resume the game
        Time.timeScale = 1f; 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Hide the continue menu before reloading the scene
        HideContinueMenu(); 
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Results Screen");
        // Hide the continue menu before loading the results screen
        HideContinueMenu(); 
    }

    public void YesHover()
    {
        // Play Continue Sound
        SoundManager.instance.PlaySfx("Ready?");
    }

    public void NoHover()
    {
        // Play Continue Soundn
        SoundManager.instance.PlaySfx("No");
    }
}

    
