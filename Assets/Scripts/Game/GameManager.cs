using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Current wave.
    public int wave { get; private set; } = -1;
    public bool inWaveScene { get; private set; }

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
            SceneManager.sceneLoaded += OnSceneLoaded;
            SetUpRun();
            FindDummy();
            FindPlayer();
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f && inWaveScene)
        {
            // Play Continue Soundn
            SoundManager.instance.PlaySfx("Continue");
            SceneManager.LoadScene("ContinueMenu");
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

        wave += 1;
        if (wave <= 15)
        {
            SceneManager.LoadScene("Wave" + Convert.ToString(wave));
        }
        else
        {
            SceneManager.LoadScene("Results Screen");
        }
    }

    public void RetryWave()
    {
        TotalScore -= 10000;
        clearedWave = false;
        clearTimer = 0.0f;

        this.Score = 0;
        computedScore = false;

        DamageEngine.Instance.maxComboThisWave = 0;
        timer = INITIAL_TIME;

        SceneManager.LoadScene("Wave" + Convert.ToString(wave));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inWaveScene = scene.name.Substring(0, 4) == "Wave";

        // Once a scene is loaded, we try to detect the wave number by
        // parsing the scene's name. This allows automatic assignment
        // based on the scene name. Note the scene's name must be
        // formatted "Wave[#]".
        //
        // This number is only assigned via string if it hasn't been
        // assigned yet (-1).
        if (wave == -1 && inWaveScene)
        {
            wave = Convert.ToInt32(scene.name.Remove(0, 4));
        }

        // Clear all actions, since the objects got removed on scene load
        subtractTimeActions = null;

        // Assign the dummy and player from the scene
        if (inWaveScene)
        {
            FindDummy();
            FindPlayer();
        }
    }

    // Resets everything back to default.
    public void SetUpRun()
    {
        timer = INITIAL_TIME;
        this.Score = 0;
        TotalScore = 0;
        computedScore = false;
        totalTime = 0.0f;
        clearTimer = 0.0f;
    }

    public void StartRun()
    {
        waveSelectMode = false;
        wave = 1;
        SetUpRun();
        SceneManager.LoadScene("Wave1");
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
        if (dummy == null)
        {
            return;
        }

        dummy.SubscribeOnSlain(() => {
            clearedWave = true;
        });
    }

    void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            return;
        }
        
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