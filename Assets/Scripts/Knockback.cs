using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float decay = 0.05f;
    private Vector3 velocity;
    private CharacterController controller;
    private Hurtbox hurtbox;

    // Public getter for velocity (speed)
    public float Speed { get => velocity.magnitude; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        hurtbox = GetComponentInChildren<Hurtbox>();
        hurtbox.SubscribeOnHurt(OnHurt);
    }

    void Update()
    {
        velocity = Vector3.Lerp
        (
            velocity, 
            Vector3.zero,
            1.0f - Mathf.Exp(-decay * Time.deltaTime)
        );
        controller.Move(velocity * Time.deltaTime);
    }

    void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction) 
    {
        velocity = direction * properties.knockback;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag != "Reflect" && hit.gameObject.tag != "Rope")
        {
            return;
        }
        
        // Do bounce physics on the rope upon contanct
        Rope rope = hit.gameObject.GetComponent<Rope>();
        if (rope != null)
        {
            rope.Bounce(hit.point, velocity);
        }

        // If we hit something that reflects, get the
        // normal of the surface and flatten it
        Vector3 flatNormal = hit.normal;
        flatNormal.y = 0.0f;
        flatNormal = flatNormal.normalized;

        // Determine the direction of travel.
        // This calcualtion accounts for sidestepping
        Vector3 direction = velocity.normalized;
        
        // Using the direction of travel and the normal,
        // calculate the vector of reflection and use that
        // as the new forward vector
        direction = direction - 2 * Vector3.Dot(direction, flatNormal) * flatNormal;
        direction = direction.normalized;
        velocity = direction * velocity.magnitude;
    }
}
