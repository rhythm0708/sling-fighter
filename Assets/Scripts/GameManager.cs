using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Handle multiplier wrt specifications on design doc.
public class GameManager : MonoBehaviour
{
    private float score = 0;
    private float multiplier = 0;
    private GameObject enemyFactory;
    private int currWave = 0;
    private const int WaveCap = 7;
    private int enemiesKilled = 0;
    private int killCountToAdvance = 10;
    private int prevEnemyCount;

    void Awake()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        prevEnemyCount = enemies.Length;
        enemyFactory = GameObject.Find("EnemyFactory");
    }

    // Gets the current score.
    public float GetScore()
    {
        return this.score;
    }

    // Sets up the game for a new wave.
    // Generates the appropriate amount of enemies, updates fields.
    private void SetUpNewWave()
    {
        EnemyFactoryController factoryController = enemyFactory.GetComponent<EnemyFactoryController>();
        enemiesKilled = 0;
        killCountToAdvance *= currWave;
        for (int i = 0; i < killCountToAdvance; i++)
        {
            factoryController.GenerateRandomEnemy();
        }
    }
    
    // 
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var currEnemyCount = enemies.Length;
        var localKilled = prevEnemyCount - currEnemyCount;
        // score += localKilled * score * multiplier;
        // score determined using a table based on enemy type.
        // Keep it simple for now.
        score += localKilled * 10 * multiplier;
        enemiesKilled += localKilled;
        if (enemiesKilled == killCountToAdvance)
        {
            currWave++;
            SetUpNewWave();
            // All enemies are there, all need to be killed to advance.
            currEnemyCount = killCountToAdvance;
        }
        prevEnemyCount = currEnemyCount;
    }
}