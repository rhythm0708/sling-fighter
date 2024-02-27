using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        WaitSling,
        ChargeSling,
        Move,
        ChargePunch
    };

    [SerializeField] private float punchDistance = 8.0f;
    [SerializeField] private float slingSpeed = 120.0f;
    [SerializeField] private float decceleration = 1.0f;
    [SerializeField] private float sideStepSpeed = 30.0f;
    [SerializeField] private float sideStepLength = 0.15f;

    private Vector3 forward;
    private float speed;
    private State state;
    private Vector3 ropeForward;
    private Camera camera;
    private float sideStepTimer;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        forward = transform.forward;
        ropeForward = forward;
        state = State.WaitSling;
        camera = Camera.main;
        sideStepTimer = 0.0f;
    }

    public Vector3 GetForward()
    {
        return forward;
    }

    public Vector3 GetRopeForward()
    {
        return ropeForward;
    }

    public State GetState()
    {
        return state;
    }

    void Update() 
    {
        // When the punch is charging, slow-mo the game
        if (state == State.ChargePunch)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        // Each state has their own update to control movement
        switch (state)
        {
            case State.WaitSling:
                WaitSlingUpdate();
                break;

            case State.ChargeSling:
                ChargeSlingUpdate();
                break;

            case State.Move:
                MoveUpdate();
                break;

            case State.ChargePunch:
                ChargePunchUpdate();
                break;
        }

        // Draw the aim-line
        if (state == State.ChargeSling || state == State.ChargePunch)
        {
            Debug.DrawLine(transform.position, transform.position - GetAxisInput().normalized * punchDistance, Color.red);
        }
        // Draw the forward line
        Debug.DrawLine(transform.position, transform.position + forward * 5.0f, Color.green);
    }

    // Returns the vector pointing in the direction of the
    // stick, based on the angle of the camera
    Vector3 GetAxisInput()
    {
        Vector3 axis = Vector3.zero;

        // Get the camera forward vector, and flatten it
        Vector3 cameraForward = camera.transform.forward;
        cameraForward.y = 0.0f;
        cameraForward = cameraForward.normalized;

        // Get the camera right vector and flatten it
        Vector3 cameraRight = camera.transform.right;
        cameraRight.y = 0.0f;
        cameraRight = cameraRight.normalized;


        // The forward and right vector summed together create
        // the direction vector. It is then clamped to ensure its
        // value doesn't go beyond 1.0f
        axis += cameraRight * Input.GetAxisRaw("Horizontal");
        axis += cameraForward * Input.GetAxisRaw("Vertical");
        return Vector3.ClampMagnitude(axis, 1.0f);
    }

    void WaitSlingUpdate()
    {
        if (Input.GetButtonDown("Action"))
        {
            state = State.ChargeSling;
        }
    }

    void ChargeSlingUpdate()
    {
        // Once the sling is released, start moving forward
        // (opposite of stick direction)
        Vector3 axisInput = GetAxisInput();
        if (Input.GetButtonUp("Action") && axisInput.magnitude > 0.1f)
        {
            forward = -axisInput.normalized;
            speed = slingSpeed;
            state = State.Move;
        }
    }

    void MoveUpdate()
    {
        // Decelerrate the player as time progresses
        speed -= decceleration * Time.deltaTime;

        // Set the sidestep upon input. 
        // Negative = right 
        // Positive = left
        if (Input.GetButtonDown("StepLeft"))
        {
            sideStepTimer = sideStepLength;
        }
        if (Input.GetButtonDown("StepRight"))
        {
            sideStepTimer = -sideStepLength;
        }
    
        // The side step timer will go towards 0.0
        // and lock on the value once reached
        if (sideStepTimer < 0.0f) {
            sideStepTimer = Mathf.Min(sideStepTimer + Time.deltaTime, 0.0f);
        }
        else
        {
            sideStepTimer = Mathf.Max(sideStepTimer - Time.deltaTime, 0.0f);
        }

        // The player moves to the direction of side step.
        // As sidestep value gets closer to 0.0, the player
        // moves less and less in their sidestep direction
        float sideStepFactor = sideStepTimer / sideStepLength; 
        controller.Move
        (
            Vector3.Cross(forward, transform.up) * 
            sideStepSpeed * 
            sideStepFactor *
            Time.deltaTime
        );

        // Move the player
        controller.Move(forward * speed * Time.deltaTime);

        // This code enables punching, but since we're likely
        // not using the mechanic, this is commented out.
        //
        // if (Input.GetButtonDown("Action"))
        // {
        //     state = State.ChargePunch;
        // }
    }

    void ChargePunchUpdate()
    {
        // Same sidestep code as MoveUpdate
        if (Input.GetButtonDown("StepLeft"))
        {
            sideStepTimer = sideStepLength;
        }
        if (Input.GetButtonDown("StepRight"))
        {
            sideStepTimer = -sideStepLength;
        }
        float sideStepFactor = sideStepTimer / sideStepLength; 
        if (sideStepTimer < 0.0f) {
            sideStepTimer = Mathf.Min(sideStepTimer + Time.deltaTime, 0.0f);
        }
        else
        {
            sideStepTimer = Mathf.Max(sideStepTimer - Time.deltaTime, 0.0f);
        }
        controller.Move
        (
            Vector3.Cross(forward, transform.up) * 
            sideStepSpeed * 
            sideStepFactor *
            Time.deltaTime
        );

        // Upon releasing the punch, we Raycast based on its direction.
        // If it hits, we set our forward direction to the opposite of
        // the input
        if (Input.GetButtonUp("Action"))
        {
            Vector3 axis = GetAxisInput().normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, axis, out hit, punchDistance))
            {
                speed = slingSpeed;
                forward = -axis;
                sideStepTimer = 0.0f;
            }
            state = State.Move;
        }

        // Move the player
        controller.Move(forward * speed * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Reflect")
        {
            // If we hit something that reflects, get the
            // normal of the surface and flatten it
            Vector3 flatNormal = hit.normal;
            flatNormal.y = 0.0f;
            flatNormal = flatNormal.normalized;

            // Determine the direction of travel.
            // This calcualtion accounts for sidestepping
            Vector3 direction = forward;
            float sideStepFactor = sideStepTimer / sideStepLength; 
            direction += 
                Vector3.Cross(forward, transform.up) * 
                sideStepSpeed * 
                sideStepFactor;
            direction = direction.normalized;

            // Using the direction of travel and the normal,
            // calculate the vector of reflection and use that
            // as the new forward vector
            forward = direction - 2 * (Vector3.Dot(direction, flatNormal)) * flatNormal;
            forward = forward.normalized;
            sideStepTimer = 0.0f;
        }
        else if (hit.gameObject.tag == "Rope")
        {
            // Once we hit a rope, go into the
            // WaitSling stat
            ropeForward = hit.normal;
            state = State.WaitSling;
            sideStepTimer = 0.0f;
        }
    }
}
