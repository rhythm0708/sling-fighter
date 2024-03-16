using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}