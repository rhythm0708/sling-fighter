using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Possibly integrate enemy healthbar in the future.
public class EnemyController : MonoBehaviour
{
    private float catchUpSpeed = 1.0f;
    private float health = 100.0f;
    private float damage = 10.0f;
    private GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
        BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
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

    // Uses specifications to update attributes.
    public void SetSpec(EnemySpec spec)
    {
        this.health = spec.health;
        this.damage = spec.damage;
        this.catchUpSpeed = spec.catchUpSpeed;
    }

    // Gets the damage.
    public float GetDamage()
    {
        return this.damage;
    }

    // Lose appropriate amount of health.
    private void TakeDamage(float playerDamage)
    {
        // Potentially integrate a damage table?
        this.health -= playerDamage;
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Handle getting hit by player.
    private void OnCollisionStay(Collision collision)
    {
        // Can use switch statement if there will be more things that deal damage.
        if ("Fist" == collision.gameObject.tag)
        {
            var playerDamage = collision.gameObject.GetComponent<PlayerController>().GetDamage();
            this.TakeDamage(playerDamage);
        }
    }
    
    void Update()
    {
        // Simplistic pathfinding implementation.
        // Possibly change later, but does the job.
        var currPos = this.gameObject.transform.position;
        var playerPos = player.transform.position;
        var dir = playerPos - currPos;
        var normalizedDir = dir.normalized;
        this.gameObject.GetComponent<Rigidbody>().velocity = catchUpSpeed * normalizedDir;
        // If enemy fell off the arena, destroy it.
        if (this.gameObject.transform.position.y <= -1f)
        {
            Destroy(this.gameObject);
        }
    }
}