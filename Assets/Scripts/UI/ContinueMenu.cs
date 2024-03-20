using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueMenu;

    public void Restart()
    {
        GameManager.Instance.RetryWave();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Results Screen");
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

    
