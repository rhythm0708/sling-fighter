using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Current wave.
    public int wave { get; private set; } = 1;

    public DummyController dummy { get; private set; }
    public PlayerController player { get; private set; }

    private const float INITIAL_TIME = 90.0f;
    public float timer { get; private set; }
    public float totalTime { get; private set; }

    public static GameManager Instance { get; private set; } = null;
    
    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            timer = INITIAL_TIME;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        // TODO: Game over on time out;
    }

    void FindDummy()
    {
        dummy = FindObjectOfType<DummyController>();
        dummy.SubscribeOnSlain(NextWave);
    }

    void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
        player.SubscribeOnFall(() => {
            timer -= 10.0f;
        });
    }

    // Move to next wave.
    void NextWave()
    {
        // To determine the next wave, we parse the name of the scene
        // and find its number at the end. We increment this number then
        // construct the string of the next scene. Note that scene names
        // must be formtted as "Wave[#]"
        string sceneName = SceneManager.GetActiveScene().name;
        try 
        {
            int waveNumber = Convert.ToInt32(sceneName.Remove(0, 4));
            SceneManager.LoadScene("Wave" + Convert.ToString(waveNumber + 1));
        }
        catch
        {
            Debug.Log("Tried loading next Wave, but scene name does not contain Wave[#]");
        }
    }
    
    // Kill dummy (for debugging).
    [ContextMenu("Kill Dummy")]
    public void KillDummy()
    {
        //dummyController.GetHealth = 0;
        //dummyController.Damage(0);
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Once a scene is loaded, we try to detect the wave number by
        // parsing the scene's name. This allows automatic assignment
        // based on the scene name. Note the scene's name must be
        // formatted "Wave[#]"
        try 
        {
            wave = Convert.ToInt32(scene.name.Remove(0, 4));
        }
        catch
        {
            Debug.Log("Tried assigning wave number, but scene name does not contain Wave[#]");
        }

        FindDummy();
        FindPlayer();
        totalTime += timer;
        timer = INITIAL_TIME;
    }
}