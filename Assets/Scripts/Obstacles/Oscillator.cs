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
        // Oscillate.
        if (transform.position == targetPos1)
        {
            currTargetPos = targetPos2;
        }
        else if (transform.position == targetPos2)
        {
            currTargetPos = targetPos1;
        }

        transform.position = Vector3.MoveTowards(transform.position, currTargetPos, sidestepSpeed);
    }
}