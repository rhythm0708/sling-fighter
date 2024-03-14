using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    // References.
    GameObject player;
    Hitbox playerHitbox;
    Hurtbox playerHurtbox;
    GameManager gameManager;

    // Related to timer values.
    [Header("TIMER VALUES")]
    [SerializeField] float startTime;
    [SerializeField] float currentTime;
    [SerializeField] float startBufferTime;

    // Related to enemy time values.
    [Header("ENEMY POINT VALUES")]
    [SerializeField] int enemyHitTime;
    [SerializeField] int enemyKilledTime;
    [SerializeField] int eliteHitTime;
    [SerializeField] int eliteKilledTime;
    [SerializeField] int bossHitTime;
    [SerializeField] int bossKilledTime;

    // Related to losing time.
    [Header("LOSING TIME VALUES")]
    [SerializeField] int oOBLostTime;

    // Property to get CurrentTime.
    public int CurrentTime { get => (int)currentTime; }

    private void Awake()
    {
        // Get reference to playerHitbox.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHitbox = player.GetComponentInChildren<Hitbox>();
        playerHurtbox = player.GetComponentInChildren<Hurtbox>();

        playerHurtbox.SubscribeOnHurt(OnHurt);
    }

    void Start()
    {
        // Apply a buffer before time starts ticking down.
        startTime += startBufferTime;
        currentTime = startTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        // Game Overs if time goes to zero.
        if (currentTime <= 0)
        {
            SceneManager.LoadScene("Results Screen");
        }
    }

    private void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        // Check out-of-bounds. Lose time.
        if (collider.gameObject.tag == "Enemy")
        {
            currentTime -= 5;
        }
    }
}