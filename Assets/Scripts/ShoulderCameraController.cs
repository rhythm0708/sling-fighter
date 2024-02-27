using UnityEngine;

public class ShoulderCameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 0.995f;
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>(); 
    }

    void LateUpdate()
    {
        Vector3 forward;
        if (player.GetState() == Player.State.ChargeSling || player.GetState() == Player.State.WaitSling)
        {
            forward = player.GetRopeForward();
        }
        else
        {
            forward = player.GetForward(); 
        }

        transform.rotation = Quaternion.Slerp
        (
            transform.rotation, 
            Quaternion.LookRotation(forward),
            1.0f - Mathf.Pow(1.0f - followSpeed, Time.deltaTime)
        );
    }
}
