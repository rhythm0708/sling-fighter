using System.Collections.Generic;
using UnityEngine;

// TODO: Handle multiplier wrt specifications on design doc.
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Vector3> spawnpoints;
    private float score = 0;
    private float multiplier = 0;
    private int curWave = 1;
    private const int WAVE_CAP = 7;
    private int enemiesKilled = 0;
    private int killCountToAdvance = 10;

    void Start()
    {
        SetUpNewWave();
    }

    // Gets the current score.
    public float GetScore()
    {
        return score;
    }

    // Sets up the game for a new wave.
    // Generates the appropriate amount of enemies, updates fields.
    private void SetUpNewWave()
    {
        enemiesKilled = 0;
        killCountToAdvance *= curWave;
        for (int i = 0; i < killCountToAdvance; i++)
        {
            // Instantiate a random enemy prefab at the give position

            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Vector3 pos = spawnpoints[Random.Range(0, spawnpoints.Count)];

            // Restored random spawning, this is only temporary. Please remove this on
            // the merge!
            float x = Random.Range(-75.0f, 75.0f);
            float z = Random.Range(-75.0f, 75.0f);

            Instantiate(prefab, new Vector3(x, 0.0f, z), Quaternion.identity);
        }
    }

    private void OnEnemyDestroyed(EnemyController enemyController) 
    {
        enemiesKilled += 1;
    }
    
    void Update()
    {
        // score += localKilled * score * multiplier;
        // score determined using a table based on enemy type.
        // Keep it simple for now.
        score += enemiesKilled * 10 * multiplier;
        if (enemiesKilled == killCountToAdvance)
        {
            curWave++;
            SetUpNewWave();
        }
    }
}