using UnityEngine;

// Note this component should be attached to a base
// camera anchor gameObject. The camera is then offset
// from that anchor object so it stays in the same relative
// position. The anchor rotates then to move the camera
public class ShoulderCameraController : MonoBehaviour, IIgnoreHitlag
{
    [SerializeField] private float followSpeed = 0.95f;
    [SerializeField] private float shakeStrength = 2.0f;
    [SerializeField] private float shakeSpeed = 100.0f;
    private PlayerMovement playerMovement;
    private HitlagComponent hitlagComponent;
    private float initialY;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>(); 
        hitlagComponent = GetComponentInParent<HitlagComponent>();
        initialY = transform.position.y;
    }

    public void SnapToForward(Vector3 snapForward)
    {
        transform.rotation = Quaternion.LookRotation(snapForward);
    }

    void LateUpdate()
    {
        if (hitlagComponent.time > 0.0f)
        {
            float x = Mathf.Pow(Mathf.Sin(Time.time * shakeSpeed + 16.0f), 2.0f) * shakeStrength * hitlagComponent.time;
            float y = Mathf.Pow(Mathf.Sin(Time.time * shakeSpeed + 32.0f), 2.0f) * shakeStrength * hitlagComponent.time;
            float z = Mathf.Pow(Mathf.Sin(Time.time * shakeSpeed + 64.0f), 2.0f) * shakeStrength * hitlagComponent.time;
            transform.localPosition = new Vector3(x, y, z);
            return;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }

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

        if (transform.position.y < initialY)
        {
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
        }
        else
        {
            transform.localPosition = Vector3.zero;
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
