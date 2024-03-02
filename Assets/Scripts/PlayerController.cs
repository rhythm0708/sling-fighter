using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    private Hurtbox hurtbox;

    void Start()
    {
        hurtbox = GetComponentInChildren<Hurtbox>();
        hurtbox.SubscribeOnHurt(OnHurt);
    }

    // Lose appropriate amount of health.
    private void OnHurt(Hitbox.Properties properties)
    {
        health -= properties.damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}