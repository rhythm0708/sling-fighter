using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    // Variables.
    private Hurtbox hurtbox;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    // For playtesting.
    [SerializeField] private bool reloadOnDeath = false;

    // Public getters.
    public float GetMaxHealth { get => maxHealth; }
    public float GetHealth { get => health; }

    void Start()
    {
        // Get hurtbox.
        hurtbox = GetComponentInChildren<Hurtbox>();
        hurtbox.SubscribeOnHurt(OnHurt);

        health = maxHealth;
    }

    void Damage(float amount)
    {
        health -= amount;
        if(health <= 0.0f)
        {
            if(reloadOnDeath)
            {
                // Spawn enemy back in the center.
            }
            health = 0f;
            Destroy(gameObject);
        }
    }

    void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        Damage(properties.damage);
    }
}
