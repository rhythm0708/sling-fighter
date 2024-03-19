using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageEngine : MonoBehaviour
{
    [SerializeField] const float BASE_DAMAGE = 10.0f;
    public int maxComboThisWave { get; set; }

    public static DamageEngine Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            // Set to DontDestroyOnLoad.
            DontDestroyOnLoad(gameObject);
        }

        maxComboThisWave = 0;
    }

    // Reset max combo per wave. 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        maxComboThisWave = 0;
    }

    // Damage Formula: BASE_DAMAGE + (0.5c^2 + 2.5c - 6)
    public float ComputeDamage(int combo)
    {
        // Update highest combo.
        if(combo > maxComboThisWave)
        {
            maxComboThisWave = combo;
        }

        return BASE_DAMAGE + (0.5f * Mathf.Pow(combo, 2f) + 2.5f * combo - 6f);
    }
}
