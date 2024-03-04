using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Debug.Log("entere");
        SceneManager.LoadScene("ThirdPersonTest");
    }
}