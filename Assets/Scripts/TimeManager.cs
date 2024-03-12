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
        player = GameObject.Find("Player");
        playerHitbox = GameObject.Find("Player").transform.Find("Hitbox").gameObject.GetComponent<Hitbox>();
        playerHurtbox = GameObject.Find("Player").transform.Find("Hurtbox").gameObject.GetComponent<Hurtbox>();
        gameManager = GameObject.Find("Arena").GetComponent<GameManager>();

        playerHitbox.SubscribeOnHit(HitAddTime);
        playerHurtbox.SubscribeOnHurt(OOBLoseTime);
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

    private void HitAddTime(Collider collider)
    {
        // Simple add time system.
        if(collider.gameObject.tag == "Enemy")
        {
            // TODO: can be varied for if enemy dies.
            Debug.Log("add time");
            currentTime += enemyHitTime;
        }
    }

    private void OOBLoseTime(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        // Check out-of-bounds. Lose time.
        if (collider.gameObject.tag == "Bounds")
        {
            Debug.Log("Hit bounds");
            currentTime -= oOBLostTime;
        }
    }
}