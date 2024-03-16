using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNames : MonoBehaviour
{
    // Dummy.
    DummyController dummyController;

    // Lists.
    [SerializeField] string currentEnemyName;
    [SerializeField] List<string> enemyNames;
    [SerializeField] List<string> slainEnemyNames;

    // Public properties.
    public string CurrentEnemyName { get => currentEnemyName; }

    void Awake()
    {
        // DDOL.


        // Generate an Enemy Name to start.
        currentEnemyName = GenerateNewEnemyName();
    }

    private void Start()
    {
        // Get dummy GameObject.
        dummyController = GameObject.FindWithTag("Enemy")?.GetComponent<DummyController>();
        dummyController.SubscribeOnSlain(OnSlain);
    }

    public string GenerateNewEnemyName()
    {
        foreach(string name in enemyNames)
        {
            string newName = enemyNames[Random.Range(0, enemyNames.Count - 1)];
            if (!slainEnemyNames.Contains(newName))
            {
                currentEnemyName = newName;
                return newName;
            }
        }
        currentEnemyName = "XXX";
        return "XXX";
    }

    void OnSlain()
    {
        slainEnemyNames.Add(currentEnemyName);
        currentEnemyName = GenerateNewEnemyName();
    }
}
