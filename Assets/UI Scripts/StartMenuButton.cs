using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("ThirdPersonTest");
    }
}
