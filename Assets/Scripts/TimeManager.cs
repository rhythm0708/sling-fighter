using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    // References.
    Hitbox playerHitbox;
    ArenaBounds bounds;

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
        playerHitbox = GameObject.Find("Player").transform.Find("Hitbox").gameObject.GetComponent<Hitbox>();
        bounds = GameObject.Find("Arena").GetComponent<ArenaBounds>();

        playerHitbox.SubscribeOnHit(HitAddTime);
        bounds.SubscribeOnHit(OOBLoseTime);
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

        // TEMP: Check out-of-bounds. Lose time.
        if (transform.position.y < -25.0)
        {
            Debug.Log("Hit bounds");
            currentTime -= oOBLostTime;
        }
    }

    private void HitAddTime(Collider collider)
    {
        // Simple add time system.
        if (collider.gameObject.transform.root.tag == "Enemy")
        {
            // TODO: can be varied for if enemy dies.
            currentTime += enemyHitTime;
        }
    }

    private void OOBLoseTime()
    {
         currentTime -= oOBLostTime;
    }
}