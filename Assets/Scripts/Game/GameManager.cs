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
    private const float FALL_TIME = 5.0f;
    public float timer { get; private set; }
    public float totalTime { get; private set; }

    private bool computedScore;
    public float Score { get; private set; }
    public float TotalScore { get; private set; }

    // Whether or not player has entered wave select mode.
    // Do not go through game progression if did.
    public bool waveSelectMode { get; set; }

    public bool clearedWave { get; private set; }
    public float clearTimer { get; private set;}

    public static GameManager Instance { get; private set; } = null;
    
    private Action subtractTimeActions;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            // Delete any duplicate GameManagers
            Destroy(gameObject);
        }
        else
        {
            // Assign the singleton GameManager and bind
            // its scene load action
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            totalTime -= INITIAL_TIME;
            // TODO: query for retry, handle accordingly.
            timer = INITIAL_TIME;

            // Show the continue menu when totalTime reaches or falls below 0
            ContinueMenu.instance.ShowContinueMenu();
        }
        
        if (clearedWave)
        {
            if(!computedScore)
            {
                ComputeScore();
                computedScore = true;
            }

            Time.timeScale = 0.1f;
            clearTimer += Time.deltaTime / Time.timeScale;

            if (clearTimer >= 12.0f)
            {
                NextWave();
            }
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    // Show score
    // Move to next wave.
    void NextWave()
    {
        if (waveSelectMode)
        {
            waveSelectMode = false;
            SceneManager.LoadScene("Main Menu");
        }

        clearedWave = false;
        clearTimer = 0.0f;

        this.Score = 0;
        computedScore = false;

        DamageEngine.Instance.maxComboThisWave = 0;

        // Add the last wave's timer to the total time, then
        // reset the timer
        totalTime += timer;
        timer = INITIAL_TIME;

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

        // Clear all actions, since the objects got removed on scene load
        subtractTimeActions = null;

        // Assign the dummy and player from the scene
        FindDummy();
        FindPlayer();
    }

    // Resets everything back to default.
    public void SetUpRun()
    {
        timer = INITIAL_TIME;
        this.Score = 0;
        computedScore = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
        totalTime = 0.0f;
        clearTimer = 0.0f;
    }

    public void SubtractTime(float amount)
    {
        timer -= amount;
        subtractTimeActions?.Invoke();
    }

    public void SubscribeOnSubtractTime(Action action)
    {
        subtractTimeActions += action;
    }
    
    void FindDummy()
    {
        dummy = FindObjectOfType<DummyController>();
        dummy.SubscribeOnSlain(() => {
            clearedWave = true;
        });
    }

    void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
        player.SubscribeOnFall(() => {
            SubtractTime(FALL_TIME);

            // Play the "Out Of Bounds".
            SoundManager.instance.PlaySfx("Out Of Bounds");
        });
    }

    // Score Formula: (100*Time) + (10*MaxCombo*Time) - (500*FallCount)
    void ComputeScore()
    {
        var maxCombo = DamageEngine.Instance.maxComboThisWave;
        var fallCount = player.fallCount;
        this.Score = (100*timer) + (10*maxCombo*timer) - (500*fallCount);

        this.TotalScore += this.Score;
    }

    // Kill dummy (for debugging).
    [ContextMenu("Kill Dummy")]
    public void KillDummy()
    {
        dummy.Damage(dummy.maxHealth);
    }
}