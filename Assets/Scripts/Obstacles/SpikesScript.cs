using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    [SerializeField] private float deltaY;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeSubtraction = 5.0f;
    [SerializeField] private float damageCooldown = 1.0f;
    private int state = 2;
    private float speedMultiplier = 1;
    // How much time to wait before release.
    private float lingerDurationDown;
    // How much time to wait before retract.
    private float lingerDurationUp;
    // Time trackers.
    private float lingerTimeElapsed = 0;
    private float damageTimeElapsed = 0;
    // Misc.
    private Vector3 initialPos;
    private Vector3 minPos;
    private PlayerController player;

    void Start()
    {
        player = GameManager.Instance.player;
        initialPos = transform.position;
        // Use offset to define minimum position.
        minPos = initialPos + new Vector3(0, deltaY, 0);
        // Randomization.
        lingerDurationDown = Random.Range(2, 7);
        lingerDurationUp = Random.Range(2, 7);
    }

    // Releasing the spikes on the player.
    private void PerformRelease()
    {
        // If reached the minimum position, update state and set up for retract.
        if (transform.position == minPos)
        {
            state = 0;
            lingerTimeElapsed = 0;
            speedMultiplier = 1;
            lingerDurationUp = Random.Range(2, 7);
        }
        else
        {
            // Release spikes.
            transform.position = Vector3.MoveTowards
            (
                transform.position,
                minPos,
                Time.deltaTime * moveSpeed * speedMultiplier
            );
            speedMultiplier += 0.1f;
        }
    }

    // Retract spikes and charge for the next release.
    private void ChargeRelease()
    {
        // If reached the maximum position, update state and set up for release.
        if (transform.position == initialPos)
        {
            state = 2;
            lingerTimeElapsed = 0;
            lingerDurationDown = Random.Range(2, 7);
        }
        else
        {
            // Retract spikes.
            transform.position = Vector3.MoveTowards
            (
                transform.position,
                initialPos,
                Time.deltaTime * moveSpeed
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If damage cooldown expired and contacting player, subtract time.
        if (other.transform.root == player.transform.root &&
            damageTimeElapsed >= damageCooldown)
        {
            GameManager.Instance.SubtractTime(timeSubtraction);
            damageTimeElapsed = 0;
        }
    }

    void Update()
    {
        switch (state)
        {
            case 0:
                // Resting state (maximum displacement).
                if (lingerTimeElapsed >= lingerDurationUp)
                {
                    state = 1;
                }
                break;
            case 1:
                // Moving up.
                ChargeRelease();
                break;
            case 2:
                // Lingering state (minimum displacement).
                if (lingerTimeElapsed >= lingerDurationDown)
                {
                    state = 3;
                }
                break;
            case 3:
                // Moving down
                PerformRelease();
                break;
            default:
                break;
        }
        // Update timers.
        lingerTimeElapsed += Time.deltaTime;
        damageTimeElapsed += Time.deltaTime;
    }
}