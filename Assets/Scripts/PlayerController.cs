using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float damage;
    private float health = 100;
    private float damageCooldown = 2;
    private float contactDuration = 0;
    // private bool isDead = false;

    void Awake()
    {
        // From the doc: Base damage is a randomly generated integer between 8 and 12.
        damage = Random.Range(8, 12);
        CapsuleCollider collider = this.gameObject.GetComponent<CapsuleCollider>();
        PhysicMaterial mat = new PhysicMaterial
        {
            bounciness = 0,
            dynamicFriction = 0,
            staticFriction = 0,
            bounceCombine = PhysicMaterialCombine.Minimum,
            frictionCombine = PhysicMaterialCombine.Minimum
        };
        collider.material = mat;
    }
    
    // Gets the damage.
    public float GetDamage()
    {
        return damage;
    }

    // Checks if player is dead.
    // public bool CheckIfDead()
    // {
    //     return isDead;
    // }

    // Lose appropriate amount of health.
    private void TakeDamage(float enemyDamage)
    {
        // Potentially integrate a damage table?
        health -= enemyDamage;
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
            // isDead = true;
            // Switch to Game Over screen.
            // SceneManager.LoadScene("Result Screen");
        }
    }
    
    // When player collides with the enemy, they take damage.
    private void OnCollisionStay(Collision collision)
    {
        if ("Enemy" == collision.gameObject.tag && contactDuration >= damageCooldown)
        {
            var enemyDamage = collision.gameObject.GetComponent<EnemyController>().GetDamage();
            TakeDamage(enemyDamage);
            contactDuration = 0;
        }
        else if ("Reflect" == collision.gameObject.tag && contactDuration >= damageCooldown)
        {
            // 10 is a placeholder damage value for all bumpers. Up to change.
            // Reflect <=> Bumper.
            TakeDamage(10);
            contactDuration = 0;
        }
        else if ("Target" == collision.gameObject.name)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(this.damage);
        }
    }

    void Update()
    {
        contactDuration += Time.deltaTime;
    }
}