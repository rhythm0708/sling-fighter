using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
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

    // Property to get timeStamp
    public int CurrentTime { get => (int)currentTime; }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // Adds to time if enemy was hit.
            
        }
    }
}