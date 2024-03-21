using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        GameManager.Instance.StartRun();
    }
}