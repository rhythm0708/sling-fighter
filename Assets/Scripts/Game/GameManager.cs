using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Current wave.
    private static int _wave = 1;
    public float wave 
    { 
        get {return _wave; }
    }

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
        _wave++;
        SceneManager.LoadScene("Wave" + _wave);
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
        FindDummy();
        FindPlayer();
        totalTime += timer;
        timer = INITIAL_TIME;
    }
}