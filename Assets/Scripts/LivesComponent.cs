using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesComponent : MonoBehaviour
{
    // Related scripts.
    GameObject player;
    Hurtbox playerHurtbox;
    ArenaBounds bounds;
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
        bounds = GameObject.Find("Arena").GetComponent<ArenaBounds>();
        gameManager = GameObject.Find("Arena").GetComponent<GameManager>();
        sfxManager = GameObject.Find("SFX Manager").GetComponent<SFXManager>();
    }

    private void Start()
    {
        // Subscribe to player hurtbox.
        playerHurtbox.SubscribeOnHurt(LoseLife);
        bounds.SubscribeOnHit(BoundsLoseLife);
        
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
        var hitbox = collider.gameObject.transform.parent.gameObject.GetComponentInChildren<Hitbox>();
        var enemySpeed = collider.gameObject.GetComponent<Knockback>()?.Speed;

        if (hitbox.properties.type == "Enemy" && enemySpeed < 50)
        {
            // Debug.Log($"Enemy speed: {enemySpeed}");
            lives -= 1;
        }
    }

    void BoundsLoseLife()
    {
        lives -= 1;
    }

    void CheckGameOver()
    {
        if(lives<=0)
        {
            SceneManager.LoadScene("Results Screen");
        }
    }
}
