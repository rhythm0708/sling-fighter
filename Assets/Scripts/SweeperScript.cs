using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Goes around arena at varying speeds.
public class SweeperScript : MonoBehaviour
{
    [SerializeField] private Vector3[] checkPoints;
    [SerializeField] private float speedChangeCooldown;
    private float speed = 150;
    private float timeElapsed = 0;
    // Index of checkpoint Sweeper is moving towards right now.
    private int targetCheckpoint = 1;
    
    void Update()
    {
        if (timeElapsed >= speedChangeCooldown)
        {
            // Randomly assign new speed.
            speed = Random.Range(150, 350);
            timeElapsed = 0;
        }

        if (transform.position == checkPoints[targetCheckpoint])
        {
            // Update index, always between 0 and checkPoints.Length.
            targetCheckpoint = (targetCheckpoint + 1) % checkPoints.Length;
        }

        var target = checkPoints[targetCheckpoint];
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        timeElapsed += Time.deltaTime;
    }
}