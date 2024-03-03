using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Needs to be linked to Player.

    // Related to tracking score.
    [SerializeField] int totalScore;
    [SerializeField] int comboScore;

    // Related to enemy score values.
    [Header("ENEMY POINT VALUES")]
    [SerializeField] int enemyHitScore;
    [SerializeField] int enemyKilledScore;
    [SerializeField] int eliteHitScore;
    [SerializeField] int eliteKilledScore;

    // Related to multiplier limits.
    [Header("MULTIPLIER UPPER BOUNDS")]
    [SerializeField] List<int> multiplierBounds;

    // Related to live multiplier values.
    [SerializeField] int multiplierRawScore;
    [SerializeField] int multiplierValue { get; set; }
    [SerializeField] float timeSinceHit;
    [SerializeField] float multiplierResetTime;

    private void Update()
    {
        timeSinceHit += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            timeSinceHit = 0f;
        }
    }

    // Update multiplier value.
    private void ComputeMultiplier(int multiplierRawScore)
    {
        foreach (int bound in multiplierBounds)
        {
            if(bound>multiplierRawScore)
            {
                multiplierValue = multiplierBounds.IndexOf(bound) - 1;
                break;
            }
        }
    }

    // Reset multiplier.
    private void ResetMultiplier()
    {
        multiplierRawScore = 0;
        timeSinceHit = 0f;
    }
}
