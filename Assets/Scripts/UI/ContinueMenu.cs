using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueMenu : MonoBehaviour
{
    // Set this to true when you want the ContinueMenu to pop-up
    private bool isContinue;
    [SerializeField]private GameObject continueMenu;

    void Start()
    {
        // Set continueMenu to false initially so that it doesn't show up
        continueMenu.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            continueMenu.SetActive(true);
            // Play Continue Soundn
            SoundManager.instance.PlaySfx("Continue");
            Time.timeScale = 0f;
            SoundManager.instance.StopMusic("In Game");
        }
    }

    public void Restart()
    {
        continueMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Results Screen");
        continueMenu.SetActive(false);
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

    
