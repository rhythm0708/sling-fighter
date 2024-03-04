using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [SerializeField] private Vector3 respawnLocation;
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
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("Results Screen");
        }
    }

    private void Update()
    {
        Vector3 currPos = gameObject.transform.position;
        if (currPos.y < -1)
        {
            // Teleport back to the arena for the sake of testing.
            // gameObject.transform.position = respawnLocation;
            // TODO: could have player take damage or die instantly.
            SceneManager.LoadScene("Results Screen");
        }

        hitbox.directionOverride = movement.GetForward();
    }
}