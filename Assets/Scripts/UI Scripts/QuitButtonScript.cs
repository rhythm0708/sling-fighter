using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonScript : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Application.Quit();
    }
}
