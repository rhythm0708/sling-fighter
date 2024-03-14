using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Current wave.
    private static int currWave = 1;

    // Dummy reference.
    private GameObject dummy;
    
    void Start()
    {
        dummy = GameObject.Find("Rusher");
    }

    // Get current wave.
    public float Wave { get => currWave; }
    
    void Update()
    {
        // Uncomment once Health getter is implemented.
        // if (dummy.GetComponent<RusherController>().Health <= 0)
        // {
        //     currWave++;
        //     SceneManager.LoadScene("Wave" + currWave);
        //     dummy = GameObject.Find("Rusher");
        // }
    }
}