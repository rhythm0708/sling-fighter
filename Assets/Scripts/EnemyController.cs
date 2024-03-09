using System;
using System.Collections.Generic;
using UnityEngine;

// TODO: Possibly integrate enemy healthbar in the future.
public class EnemyController : MonoBehaviour
{
    // [SerializeField] private float health = 100.0f;
    // private float damageCooldown;
    // private float timeElapsed = 0;
    private Hurtbox hurtbox;

    // For other scripts to access Health.
    // public float Health { get => health; }

    void Awake()
    {
        // damageCooldown = UnityEngine.Random.Range(2, 5);
    }

    void Start()
    {
        hurtbox = GetComponentInChildren<Hurtbox>();
        if (hurtbox != null)
        {
            hurtbox.SubscribeOnHurt(OnHurt);
        }
    }

    // Lose appropriate amount of health.
    public void OnHurt(Hitbox.Properties properties, Vector3 direction)
    {
        // Potentially integrate a damage table?
        // Make sure that cooldown has expired.
        // if (timeElapsed >= damageCooldown)
        // {
        //     health -= properties.damage;
        //     if (health <= 0)
        //     {
        //         foreach (Action<EnemyController> action in onDestroyActions)
        //         {
        //             action.Invoke(this);
        //         }
        //         Destroy(gameObject);
        //     }
        //     timeElapsed = 0;
        // }
        if (properties.type == "Player")
        {
            Destroy(gameObject);
        }
    }
}