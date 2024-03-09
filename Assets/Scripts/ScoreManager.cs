using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Needs to be attached to Player GameObject.

    // Related to enemy score values.
    [Header("ENEMY POINT VALUES")]
    [SerializeField] int enemyHitScore;
    [SerializeField] int enemyKilledScore;
    [SerializeField] int eliteHitScore;
    [SerializeField] int eliteKilledScore;
    [SerializeField] int bossHitScore;
    [SerializeField] int bossKilledScore;

    // Related to multiplier limits.
    [Header("MULTIPLIER UPPER BOUNDS")]
    [SerializeField] List<int> multiplierBounds;
    [SerializeField] float multiplierResetTime;

    // Related to tracking score.
    [Header("SCORE AND MULTIPLIER VALUES")]
    [SerializeField] int totalScore;
    [SerializeField] int comboScore;

    // Related to live multiplier values.
    [SerializeField] int multiplierRawScore;
    [SerializeField] int multiplierValue;
    private float timeSinceHit;

    // Public getters to obtain score values.
    public int TotalScore { get => totalScore; }
    public int ComboScore { get => comboScore; }
    public int MultiplierValue { get => multiplierValue; }

    private void Start()
    {
        // Initial values to start the game.
        totalScore = 0;
        comboScore = 0;
        multiplierRawScore = 0;
        multiplierValue = 0;
        timeSinceHit = 0;

    }
    private void Update()
    {
        timeSinceHit += Time.deltaTime;

        // Multiplier ends because of time.
        if(timeSinceHit >= multiplierResetTime)
        {
            ResetMultiplier();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            timeSinceHit = 0f;
            multiplierRawScore += 1;

            // If enemy took damage.
            if(other.transform.parent.gameObject.GetComponent<EnemyController>().Health > 0f)
            {
                comboScore += enemyHitScore;
            }
            // If enemy was killed.
            else
            {
                comboScore += enemyKilledScore;
            }
            // Debug.Log("Enemy hit.");
        }
        else if(other.tag == "Elite")
        {
            timeSinceHit = 0f;
            multiplierRawScore += 1;

            // If enemy took damage.
            if (other.transform.parent.gameObject.GetComponent<EnemyController>().Health > 0f)
            {
                comboScore += eliteHitScore;
            }
            // If enemy was killed.
            else
            {
                comboScore += eliteKilledScore;
            }
        }
        else if(other.tag == "Boss")
        {
            timeSinceHit = 0f;
            multiplierRawScore += 1;

            // If enemy took damage.
            if (other.transform.parent.gameObject.GetComponent<EnemyController>().Health > 0f)
            {
                comboScore += bossHitScore;
            }
            // If enemy was killed.
            else
            {
                comboScore += bossKilledScore;
            }
        }

        ComputeMultiplier(multiplierRawScore);
    }

    // Update multiplier value.
    private void ComputeMultiplier(int multiplierRawScore)
    {
        foreach (int bound in multiplierBounds)
        {
            if(bound>multiplierRawScore || multiplierBounds.IndexOf(bound) == multiplierBounds.Count-1)
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

        // Add combo score to total score. Reset combo score.
        // Debug.Log($"Score added: {comboScore}");
        totalScore += comboScore;
        comboScore = 0;
    }
}
