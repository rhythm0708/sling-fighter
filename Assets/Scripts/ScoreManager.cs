using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Needs to be related to player hitbox.
    Hitbox playerHitbox;
    SFXManager sfxManager;

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

    private void Awake()
    {
        // Subscribe to player hitbox.
        playerHitbox = GameObject.Find("Player").transform.Find("Hitbox").gameObject.GetComponent<Hitbox>();
        playerHitbox.SubscribeOnHit(IncrementMultiplier);
        playerHitbox.SubscribeOnHit(IncrementScore);
        sfxManager = GameObject.Find("SFX Manager").GetComponent<SFXManager>();
    }

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
        ComputeMultiplier();

        // Multiplier ends because of time.
        if (timeSinceHit >= multiplierResetTime)
        {
            ResetMultiplier();
        }
    }

    private void IncrementScore(Collider collider)
    {
        if(collider.tag == "Enemy")
        {
            comboScore += (enemyKilledScore * multiplierValue);
            sfxManager.PlaySfx("On Hit");
        }
    }


    private void IncrementMultiplier(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            timeSinceHit = 0f;
            multiplierRawScore += 1;
            ComputeMultiplier();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            // TODO: Use other.gameObject.name to distinguish enemy types.

            // If enemy took damage.
            // if(other.transform.parent.gameObject.GetComponent<EnemyController>().Health > 0f)
            // {
            //     comboScore += (enemyHitScore * multiplierValue);
            // }
            // // If enemy was killed.
            // else
            // {
            //     comboScore += (enemyKilledScore * multiplierValue);
            // }
            // Debug.Log("Enemy hit.");
        }

        // Commented out for now.

        // else if(other.tag == "Elite")
        // {
        //     timeSinceHit = 0f;
        //     multiplierRawScore += 1;
        //     ComputeMultiplier();

        //     // If enemy took damage.
        //     if (other.transform.parent.gameObject.GetComponent<EnemyController>().Health > 0f)
        //     {
        //         comboScore += (eliteHitScore * multiplierValue);
        //     }
        //     // If enemy was killed.
        //     else
        //     {
        //         comboScore += (eliteKilledScore * multiplierValue);
        //     }
        // }
        // else if(other.tag == "Boss")
        // {
        //     timeSinceHit = 0f;
        //     multiplierRawScore += 1;
        //     ComputeMultiplier();

        //     // If enemy took damage.
        //     if (other.transform.parent.gameObject.GetComponent<EnemyController>().Health > 0f)
        //     {
        //         comboScore += (bossHitScore * multiplierValue);
        //     }
        //     // If enemy was killed.
        //     else
        //     {
        //         comboScore += (bossKilledScore * multiplierValue);
        //     }
        // }
    }

    // Update multiplier value.
    private void ComputeMultiplier()
    {
        foreach (int bound in multiplierBounds)
        {
            if(bound>=multiplierRawScore || multiplierBounds.IndexOf(bound) == multiplierBounds.Count-1)
            {
                multiplierValue = multiplierBounds.IndexOf(bound);
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
        totalScore += comboScore;
        // Debug.Log($"Score added: {comboScore}");
        comboScore = 0;
    }
}
