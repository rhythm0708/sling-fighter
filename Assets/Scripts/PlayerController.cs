using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    private Hurtbox hurtbox;
    private Hitbox hitbox;
    private PlayerMovement movement;

    void Start()
    {
        hurtbox = GetComponentInChildren<Hurtbox>();
        hurtbox.SubscribeOnHurt(OnHurt);
        hitbox = GetComponentInChildren<Hitbox>();
        movement = GetComponent<PlayerMovement>();
    }

    // Lose appropriate amount of health.
    private void OnHurt(Hitbox.Properties properties, Vector3 direction)
    {
        health -= properties.damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        hitbox.directionOverride = movement.GetForward();
    }
}