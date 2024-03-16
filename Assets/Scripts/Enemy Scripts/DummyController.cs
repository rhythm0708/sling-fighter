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
    [SerializeField] GameObject dummyPrefab;
    Transform spawnPoint;

    // Public getters.
    public float GetMaxHealth { get => maxHealth; }
    public float GetHealth { get => health; set { health = value; } }

    void Start()
    {
        // Get hurtbox.
        hurtbox = GetComponentInChildren<Hurtbox>();
        hurtbox.SubscribeOnHurt(OnHurt);

        health = maxHealth;
        dummyPrefab = this.gameObject;
        spawnPoint = this.gameObject.transform;
    }

    public void Damage(float amount)
    {
        health -= amount;
        if(health <= 0.0f)
        {
            if(reloadOnDeath)
            {
                // Spawn enemy back in the center.
                Instantiate(dummyPrefab,spawnPoint);
            }
            onSlainEvent?.Invoke();
            Destroy(gameObject);
        }
    }

    void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        Damage(properties.damage);
    }

    public delegate void OnSlainEventHandler();

    private event OnSlainEventHandler onSlainEvent;

    public void SubscribeOnSlain(OnSlainEventHandler action)
    {
        onSlainEvent += action;
    }

    public void UnsubscribeOnSlain(OnSlainEventHandler action)
    {
        onSlainEvent -= action;
    }
}
