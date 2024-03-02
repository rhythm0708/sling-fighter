using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Possibly integrate enemy healthbar in the future.
public class EnemyController : MonoBehaviour
{
    private float catchUpSpeed = 1;
    private float health = 100;
    private float damage = 10;
    private GameObject player;
    // private GameObject hitbox;
    // private EnemyHitbox controller;

    void Awake()
    {
        this.player = GameObject.Find("Player");
        // this.hitbox = this.gameObject.transform.Find("Target").gameObject;
        // controller = this.hitbox.GetComponent<EnemyHitbox>();
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
    // TODO: go back and figure out information hiding.
    public void TakeDamage(float playerDamage)
    {
        // Potentially integrate a damage table?
        this.health -= playerDamage;
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
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
        if (this.gameObject.transform.position.y <= -1)
        {
            Destroy(this.gameObject);
        }
    }
}