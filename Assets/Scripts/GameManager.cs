using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Current wave.
    private static int currWave = 1;

    // Dummy reference.
    private GameObject dummy;
    DummyController dummyController;
    
    void Start()
    {
        dummy = GameObject.FindWithTag("Enemy");
        dummyController = dummy?.GetComponent<DummyController>();

        dummyController.SubscribeOnSlain(NextWave);
    }

    // Get current wave.
    public float Wave { get => currWave; }

    void NextWave()
    {
        currWave++;
        SceneManager.LoadScene("Wave" + currWave);
        dummy = GameObject.FindWithTag("Enemy");
    }
}