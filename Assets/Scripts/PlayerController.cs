using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float damage;
    private float health = 100;

    void Awake()
    {
        // From the doc: Base damage is a randomly generated integer between 8 and 12.
        damage = Random.Range(8, 12);
    }
    
    // Gets the damage.
    public float GetDamage()
    {
        return this.damage;
    }

    // Lose appropriate amount of health.
    private void TakeDamage(float enemyDamage)
    {
        // Potentially integrate a damage table?
        this.health -= enemyDamage;
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
            // Display Game Over screen.
        }
    }
    
    // When player collides with the enemy, they take damage.
    private void OnTriggerEnter(Collider other)
    {
        if ("Enemy" == other.tag)
        {
            var enemyDamage = other.GetComponent<EnemyController>().GetDamage();
            this.TakeDamage(enemyDamage);
        }
    }
}