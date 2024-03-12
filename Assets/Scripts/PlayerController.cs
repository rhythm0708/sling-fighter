using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float health;
    // [SerializeField] private Vector3 respawnLocation;
    private Hurtbox hurtbox;
    private Hitbox hitbox;
    private PlayerMovement movement;
    private ShoulderCameraController cameraController;
    private float damageCooldown;
    private float timeElapsed = 0;

    void Start()
    {
        cameraController = GetComponentInChildren<ShoulderCameraController>();
        hurtbox = GetComponentInChildren<Hurtbox>();
        hurtbox.SubscribeOnHurt(OnHurt);
        hitbox = GetComponentInChildren<Hitbox>();
        movement = GetComponent<PlayerMovement>();
        damageCooldown = UnityEngine.Random.Range(2, 5);
    }

    // Lose appropriate amount of health.
    private void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        // Make sure that cooldown has expired.
        if (timeElapsed >= damageCooldown &&
            (properties.type == "Enemy" || properties.type == "Obstacle"))
        {
            health -= properties.damage;
            timeElapsed = 0;
        }
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        hitbox.directionOverride = movement.GetVelocity().normalized;

        if (movement.GetState() == PlayerMovement.State.Move)
        {
            hitbox.active = true;
        }
        else
        {
            hitbox.active = false;
        }

        if (transform.position.y < -25.0f)
        {
            movement.AttachToLastRope();
            cameraController.SnapToForward(movement.GetForward());
        }
    }
}