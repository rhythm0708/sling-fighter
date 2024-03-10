using System.Collections.Generic;
using UnityEngine;

// TODO: Handle multiplier wrt specifications on design doc.
public class GameManager : MonoBehaviour
{
    // Prefabs.
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private List<GameObject> toolPrefabs;

    // Spawn points.
    [SerializeField] public List<GameObject> enemySpawnpoints;
    [SerializeField] public List<GameObject> obstacleSpawnpoints;
    [SerializeField] public List<GameObject> playerSpawnPoints;

    // Wave and spawn variables.
    private int currWave = 1;
    private int killCount = 0;
    private int killCountToAdvance = 4;
    private int obstacleCount = 1;
    private int enemyCount = 1;
    private int toolCount = 3;
    
    void Start()
    {
        SetUpNewWave();
    }

    // Get current wave.
    public float Wave { get => currWave; }

    // Sets up the game for a new wave.
    // Generates the appropriate amount of enemies, updates fields.
    private void SetUpNewWave()
    {
        // TODO: update wave sign at the top of the screen, not added yet.

        // Clean up after last wave.
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] tools = GameObject.FindGameObjectsWithTag("Tool");

        foreach (var tool in tools)
        {
            Destroy(tool);
        }

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        // Cap the number of obstacles by possible spawn location count.
        // Every five waves, increment number of obstacles by one.
        if (obstacleCount <= obstacleSpawnpoints.Count && currWave % 5 == 0)
        {
            obstacleCount++;
        }
        // Every three waves, increase number of enemies and tools.
        if (currWave % 3 == 0)
        {
            enemyCount++;
            toolCount += 2;
        }

        killCount = 0;
        killCountToAdvance = enemyCount;
        for (int i = 0; i < enemyCount; i++)
        {
            // Instantiate a random enemy prefab at the given position.
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Vector3 pos = enemySpawnpoints[Random.Range(0, enemySpawnpoints.Count)].transform.position;
            Instantiate(prefab, pos, Quaternion.identity);
        }

        for (int i = 0; i < obstacleCount; i++)
        {
            // Instantiate a random obstacle prefab at the given position.
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
            Vector3 pos = obstacleSpawnpoints[Random.Range(0, obstacleSpawnpoints.Count)].transform.position;
            Instantiate(prefab, pos, Quaternion.identity);
        }

        for (int i = 0; i < toolCount; i++)
        {
            // Instantiate a random obstacle prefab at the given position.
            GameObject prefab = toolPrefabs[Random.Range(0, toolPrefabs.Count)];
            // Fine tune this later to prevent overlap.
            float xPos = Random.Range(-80, 80);
            float zPos = Random.Range(-80, 80);
            Vector3 pos = new Vector3(xPos, 1.5f, zPos);
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    // TODO: Handle destruction.
    
    void Update()
    {
        if (killCount == killCountToAdvance)
        {
            currWave++;
            SetUpNewWave();
        }
    }
}