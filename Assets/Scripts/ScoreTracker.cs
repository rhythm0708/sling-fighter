using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    // Don't Destroy On Load.
    public static ScoreTracker instance;

    // References.
    DummyController dummyController;
    TimeManager timeManager;
    GameManager gameManager;

    [SerializeField] float totalScore;
    [SerializeField] float waveBonusMultiplier;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Get references to objects.
        dummyController = GameObject.FindWithTag("Enemy")?.GetComponent<DummyController>();
        timeManager = GameObject.Find("Main HUD").transform.Find("Timer").gameObject.GetComponent<TimeManager>();
        gameManager = GameObject.Find("Arena").GetComponent<GameManager>();

        dummyController.SubscribeOnSlain(UpdateScore);

        // Starting score should be 0.
        totalScore = 0;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Hook up.
        Debug.Log("loaded");
        dummyController = GameObject.FindWithTag("Enemy").GetComponent<DummyController>();
        dummyController.SubscribeOnSlain(UpdateScore);
    }

    void UpdateScore()
    {
        // TODO: Make formula more sophisticated.
        totalScore += (timeManager.CurrentTime * (gameManager.Wave-1) * waveBonusMultiplier);
        Debug.Log($"Time: {timeManager.CurrentTime}, Wave: {gameManager.Wave-1}");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
