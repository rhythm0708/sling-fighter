using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
