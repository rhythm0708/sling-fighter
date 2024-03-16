using UnityEngine;

public class Smasher : MonoBehaviour
{
    [SerializeField] private float deltaY;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lingerDurationDown;
    [SerializeField] private float lingerDurationUp;
    [SerializeField] private float initDelay;
    [SerializeField] private bool firstPass;
    private Vector3 initialPos;
    private Vector3 minPos;
    private float timeElapsed = 0;
    private int state = 2;
    private bool initiated = false;
    private float speedMultiplier = 1;

    void Awake()
    {
        initialPos = transform.position;
        minPos = initialPos + new Vector3(0, -deltaY, 0);
    }

    private void PerformSmash()
    {
        if (transform.position == minPos)
        {
            state = 0;
            timeElapsed = 0;
            firstPass = false;
            speedMultiplier = 1;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, minPos, Time.deltaTime * moveSpeed * speedMultiplier);
            speedMultiplier += 0.1f;
        }
    }

    private void ChargeSmash()
    {
        if (transform.position == initialPos)
        {
            state = 2;
            timeElapsed = 0;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, Time.deltaTime * moveSpeed);
        }
    }

    void Update()
    {
        if (!initiated)
        {
            initiated = timeElapsed >= initDelay;
        }

        if (initiated)
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
                    ChargeSmash();
                    break;
                case 2:
                    // Lingering state (minimum displacement).
                    if (timeElapsed >= lingerDurationDown || firstPass)
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
        }
        timeElapsed += Time.deltaTime;
    }
}