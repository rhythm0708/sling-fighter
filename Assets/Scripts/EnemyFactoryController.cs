using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyTemplate;

    // Creates an enemy based on specifications.
    public void Build(EnemySpec spec)
    {
        float xPos = Random.Range(-80,80);
        float zPos = Random.Range(-80,80);
        Vector3 pos = new Vector3(xPos, 1.5f, zPos);
        GameObject enemy = Instantiate(enemyTemplate, pos, Quaternion.identity);
        EnemyController controller = enemy.GetComponent<EnemyController>();
        controller.SetSpec(spec);
    }

    // Generates a random enemy.
    public void GenerateRandomEnemy()
    {
        EnemySpec spec = new EnemySpec
        {
            health = Random.Range(50.0f, 100.0f),
            damage = Random.Range(8.0f, 12.0f),
            catchUpSpeed = Random.Range(0.5f, 3.0f)
        };
        this.Build(spec);
    }
}