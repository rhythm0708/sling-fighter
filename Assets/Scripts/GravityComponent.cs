using UnityEngine;

public class GravityComponent : MonoBehaviour
{
    [SerializeField] private float traceDistance = 10.0f;
    private const float ACCELERATION = 250.0f;
    private float gravity;
    private CharacterController controller;
    public bool grounded { get; private set; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gravity = 0.0f;
    }

    public float Jump(float strength, float groundHeight = 0.0f)
    {
        gravity = -strength;

        // Perform quadratic formula to get the time in air
        const float A = -ACCELERATION * 0.5f;
        float b = strength;
        float c = transform.position.y - groundHeight;

        return (-b - Mathf.Sqrt(Mathf.Pow(b, 2.0f) - (4 * A * c))) / (2 * A);
    }

    void Update()
    {
        if (!controller.enabled)
        {
            return;
        }
        RaycastHit hit;

        // Only raycast on the aerna
        int layerMask = 1 << 6;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, traceDistance, layerMask) && gravity >= 0.0f)
        {
            grounded = true;
            gravity = 0.0f;
            controller.Move(Vector3.down * 1000.0f * Time.deltaTime);
        }
        else
        {
            grounded = false;
            controller.Move(Vector3.down * gravity * Time.deltaTime);
            gravity += ACCELERATION * Time.deltaTime;
        }
    }
}
