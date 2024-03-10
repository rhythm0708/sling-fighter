using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesComponent : MonoBehaviour
{
    // Related scripts.
    GameObject player;
    Hurtbox playerHurtbox;
    GameManager gameManager;
    SFXManager sfxManager;

    float playerSpeed;

    // Lives variables.
    [SerializeField] int lives;
    [SerializeField] int startLives;
    [SerializeField] int maxLives;

    [SerializeField] float timeStamp;
    [SerializeField] float throttle;

    // Public getter for lives value.
    public int Lives { get => lives; }

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerSpeed = player.GetComponent<PlayerMovement>().Speed;
        playerHurtbox = GetComponentInChildren<Hurtbox>();
        gameManager = GameObject.Find("Arena").GetComponent<GameManager>();
        sfxManager = GameObject.Find("SFX Manager").GetComponent<SFXManager>();
    }

    private void Start()
    {
        // Subscribe to player hurtbox.
        playerHurtbox.SubscribeOnHurt(LoseLife);
        playerHurtbox.SubscribeOnHurt(CheckOutOfBounds);

        lives = startLives;
        timeStamp = 0f;
    }

    private void FixedUpdate()
    {
        timeStamp += Time.deltaTime;        
        CheckGameOver();
    }

    void LoseLife(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        var enemySpeed = collider.gameObject.GetComponent<Knockback>()?.Speed;
        if (collider.gameObject.tag == "Enemy" && enemySpeed < 50)
        {
            // Debug.Log($"Enemy speed: {enemySpeed}");
            lives -= 1;
        }
    }

    void CheckOutOfBounds(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        // Check out-of-bounds. Lose life.
        if (collider.gameObject.tag == "Bounds")
        {
            Debug.Log("Out of bounds.");
            lives -= 1;
            RespawnRandomPoint();

            // Play Death sound
            sfxManager.PlaySfx("Death");
        }
    }

    void RespawnRandomPoint()
    {
        // Select a random spawnPoint.
        Vector3 spawnPoint = gameManager.playerSpawnPoints[Random.Range(0, gameManager.playerSpawnPoints.Count)].transform.position;

        // Return to spawn point.
        player.transform.position = spawnPoint;
        playerSpeed = 0f;

    }
    void CheckGameOver()
    {
        if(lives<=0)
        {
            SceneManager.LoadScene("Results Screen");
        }
    }
}
