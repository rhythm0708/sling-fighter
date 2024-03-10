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
    private float damageCooldown;
    private float timeElapsed = 0;

    void Start()
    {
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
            // CheckGameOver();
            timeElapsed = 0;
        }
    }
/*
    private void CheckGameOver()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("Results Screen");
        }
    }
*/
    private void Update()
    {
        Vector3 currPos = gameObject.transform.position;
        if (currPos.y < -1)
        {
            // Teleport back to the arena for the sake of testing.
            // gameObject.GetComponent<CharacterController>().Move(new Vector3(0,0,0));
            // gameObject.transform.position = respawnLocation;
            // TODO: could have player take damage or die instantly.
            // SceneManager.LoadScene("Results Screen");
        }

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
    }
}