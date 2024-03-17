using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    [SerializeField] private float deltaY;
    [SerializeField] private float moveSpeed;
    private float lingerDurationDown;
    private float lingerDurationUp;
    private Vector3 initialPos;
    private Vector3 minPos;
    private float timeElapsed = 0;
    private int state = 2;
    private float speedMultiplier = 1;

    void Awake()
    {
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
            timeElapsed = 0;
            speedMultiplier = 1;
            lingerDurationUp = Random.Range(2, 7);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, minPos, Time.deltaTime * moveSpeed * speedMultiplier);
            speedMultiplier += 0.1f;
        }
    }

    private void ChargeRelease()
    {
        if (transform.position == initialPos)
        {
            state = 2;
            timeElapsed = 0;
            lingerDurationDown = Random.Range(2, 7);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, Time.deltaTime * moveSpeed);
        }
    }

    void Update()
    {
        switch (state)
        {
            case 0:
                // Resting state (maximum displacement).
                if (timeElapsed >= lingerDurationUp)
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
                if (timeElapsed >= lingerDurationDown)
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
        timeElapsed += Time.deltaTime;
    }
}