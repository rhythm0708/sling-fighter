using System;
using System.Collections.Generic;
using UnityEngine;

public class SmasherScript : MonoBehaviour
{
    [SerializeField] private float deltaY;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lingerDuration;
    // private Hurtbox hurtbox;
    private Vector3 initialPos;
    private Vector3 peakPos;
    private float timeElapsed = 0;
    private float speedMultiplier = 1;
    private int state = 0;

    void Awake()
    {
        initialPos = transform.position;
        peakPos = initialPos + new Vector3(0, deltaY, 0);
    }

    private void PerformSmash()
    {
        if (transform.position == initialPos)
        {
            state = 0;
            timeElapsed = 0;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, Time.deltaTime * moveSpeed * speedMultiplier);
        }
        speedMultiplier += 0.1f;
    }

    private void ChargeSmash()
    {
        if (transform.position == peakPos)
        {
            state = 2;
            timeElapsed = 0;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, peakPos, Time.deltaTime * moveSpeed);
        }
    }

    void Update()
    {
        switch (state)
        {
            case 0:
                // Resting state (minimum displacement).
                if (timeElapsed >= lingerDuration)
                {
                    state = 1;
                }
                break;
            case 1:
                // Moving up.
                ChargeSmash();
                break;
            case 2:
                // Lingering state (maximum displacement).
                if (timeElapsed >= lingerDuration)
                {
                    state = 3;
                }
                break;
            case 3:
                // Moving down
                PerformSmash();
                break;
            default:
                break;
        }
        timeElapsed += Time.deltaTime;
    }
}