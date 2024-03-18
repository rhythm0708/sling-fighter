using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    [SerializeField] private float deltaY;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeSubtraction = 5.0f;
    [SerializeField] private float damageCooldown = 1.0f;
    private int state = 2;
    private float lingerDurationDown;
    private float lingerDurationUp;
    private float lingerTimeElapsed = 0;
    private float damageTimeElapsed = 0;
    private float speedMultiplier = 1;
    private Vector3 initialPos;
    private Vector3 minPos;
    private PlayerController player;

    void Start()
    {
        player = GameManager.Instance.player;
        initialPos = transform.position;
        minPos = initialPos + new Vector3(0, deltaY, 0);
        lingerDurationDown = Random.Range(2, 7);
        lingerDurationUp = Random.Range(2, 7);
    }

    private void PerformRelease()
    {
        if (transform.position == minPos)
        {
            state = 0;
            lingerTimeElapsed = 0;
            speedMultiplier = 1;
            lingerDurationUp = Random.Range(2, 7);
        }
        else
        {
            transform.position = Vector3.MoveTowards
            (
                transform.position,
                minPos,
                Time.deltaTime * moveSpeed * speedMultiplier
            );
            speedMultiplier += 0.1f;
        }
    }

    private void ChargeRelease()
    {
        if (transform.position == initialPos)
        {
            state = 2;
            lingerTimeElapsed = 0;
            lingerDurationDown = Random.Range(2, 7);
        }
        else
        {
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
        lingerTimeElapsed += Time.deltaTime;
        damageTimeElapsed += Time.deltaTime;
    }
}