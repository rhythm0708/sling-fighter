using UnityEngine;

// Note this component should be attached to a base
// camera anchor gameObject. The camera is then offset
// from that anchor object so it stays in the same relative
// position. The anchor rotates then to move the camera
public class ShoulderCameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 0.95f;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>(); 
    }

    void LateUpdate()
    {
        Vector3 forward;
        if (
            playerMovement.GetState() == PlayerMovement.State.ChargeSling || 
            playerMovement.GetState() == PlayerMovement.State.WaitSling
        )
        {
            // When preparing the sling, face the camera in
            // the direction of the rope
            forward = playerMovement.GetRopeForward();
        }
        else
        {
            // When doing anything else, use the players forward
            // travel direction
            forward = playerMovement.GetForward(); 
        }

        // Interpolate the camera's rotation towards the
        // forward direction with framerate independence
        transform.rotation = Quaternion.Slerp
        (
            transform.rotation, 
            Quaternion.LookRotation(forward),
            1.0f - Mathf.Exp(-followSpeed * Time.deltaTime)
        );
    }
}
