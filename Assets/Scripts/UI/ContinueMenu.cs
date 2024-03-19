using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueMenu : MonoBehaviour
{
    // Set this to true when you want the ContinueMenu to pop-up
    private bool isContinue;
    [SerializeField]private GameObject continueMenu;
    [SerializeField] private Button primaryButton;

    void Start()
    {
        // Set continueMenu to false initially so that it doesn't show up
        continueMenu.SetActive(false);
        primaryButton.Select();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            continueMenu.SetActive(true);
            
            // Play Continue Soundn
            SoundManager.instance.PlaySfx("Continue");
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
}

    
