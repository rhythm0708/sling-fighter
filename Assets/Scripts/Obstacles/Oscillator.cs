using UnityEngine;

public class OscillatorScript : MonoBehaviour
{
    [SerializeField] private float sidestepSpeed;
    [SerializeField] private Vector3 targetPos1;
    [SerializeField] private Vector3 targetPos2;
    private Vector3 currTargetPos;

    void Awake()
    {
        currTargetPos = targetPos1;
    }
    
    void Update()
    {
        // Oscillate between targetPos1 and targetPos2.
        if (transform.position == targetPos1)
        {
            currTargetPos = targetPos2;
        }
        else if (transform.position == targetPos2)
        {
            currTargetPos = targetPos1;
        }

        // Move towards current target position.
        transform.position = Vector3.MoveTowards
        (
            transform.position,
            currTargetPos,
            sidestepSpeed
        );
    }
}