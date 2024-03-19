using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        WaitSling,
        ChargeSling,
        Move
    };

    [SerializeField] private GameObject slingPrevew;
    [SerializeField] private float slingSpeed = 120.0f;
    [SerializeField] private float decceleration = 1.0f;
    [SerializeField] private float sideStepSpeed = 30.0f;
    [SerializeField] private float sideStepLength = 0.15f;
    [SerializeField] private Rope initialRope;

    private Vector3 forward;
    private Vector3 mouseAxis;
    private bool useMouseAxis;
    private float speed;
    private State state;
    private Vector3 ropeForward;
    private Camera cam;
    private float sideStepTimer;

    private Vector3 ropeStart;
    private Vector3 smoothRopeAim;
    private Rope currentRope;
    private Vector3 recoilOffset;
    private Vector3 recoilVelocity;
    private Rope lastRope;

    // Public getter for speed.
    public float Speed { get => speed; set => speed = value; }

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        forward = transform.forward;
        ropeForward = forward;
        mouseAxis = Vector3.zero;
        state = State.WaitSling;
        cam = Camera.main;
        sideStepTimer = 0.0f;
        useMouseAxis = false;

        currentRope = initialRope;
        lastRope = initialRope;
        if (currentRope != null)
        {
            ropeStart = currentRope.Attach(gameObject);
        }
    }

    public void AttachToLastRope() 
    {
        state = State.WaitSling;
        currentRope = lastRope;
        transform.position = currentRope.transform.position;
        ropeStart = currentRope.Attach(gameObject);
        forward = ropeForward;
    }

    public Vector3 GetForward()
    {
        if (state == State.Move)
        {
            return forward;
        }
        else
        {
            return ropeForward;
        }
    }

    public Vector3 GetRopeForward()
    {
        return ropeForward;
    }

    public State GetState()
    {
        return state;
    }

    public Vector3 GetVelocity()
    {
        Vector3 velocity = forward * speed;
        float sideStepFactor = sideStepTimer / sideStepLength; 
        velocity += 
            Vector3.Cross(forward, transform.up) * 
            sideStepSpeed * 
            sideStepFactor;
        
        return velocity;
    }

    void Update() 
    {
        // Determine whether mouse based, or stick based input
        // should be used, and update the values accordingly
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        if (Mathf.Abs(mouseX) > 0.001f || Mathf.Abs(mouseY) > 0.001f)
        {
            useMouseAxis = true;
            mouseAxis.x += mouseX;
            mouseAxis.z += mouseY;
        }
        else if (
            Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f ||
            Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f
        ) 
        {
            useMouseAxis = false;
            mouseAxis = Vector3.zero;
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
        }

        // Draw the aim-line
        Vector3 axisInput = GetAxisInput();
        if (state == State.ChargeSling && axisInput.magnitude > 0.1f && Vector3.Dot(-axisInput.normalized, ropeForward) > 0.25f)
        {
            slingPrevew.SetActive(true);
            slingPrevew.transform.rotation = Quaternion.LookRotation(-smoothRopeAim);
        }
        else
        {
            slingPrevew.SetActive(false);
            // Play "Rope" SFX
            SoundManager.instance.PlaySfx("Rope");
        }
    }

    // Returns the vector pointing in the direction of the
    // stick, based on the angle of the camera
    Vector3 GetAxisInput()
    {
        Vector3 axis = Vector3.zero;

        // Get the camera forward vector, and flatten it
        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0.0f;
        cameraForward = cameraForward.normalized;

        // Get the camera right vector and flatten it
        Vector3 cameraRight = cam.transform.right;
        cameraRight.y = 0.0f;
        cameraRight = cameraRight.normalized;

        // The forward and right vector summed together create
        // the direction vector. It is then clamped to ensure its
        // value doesn't go beyond 1.0f
        float rightAxis = useMouseAxis ? mouseAxis.x : Input.GetAxisRaw("Horizontal");
        float forwardAxis = useMouseAxis ? mouseAxis.z : Input.GetAxisRaw("Vertical");
        axis += cameraRight * rightAxis;
        axis += cameraForward * forwardAxis;
        return Vector3.ClampMagnitude(axis, 1.0f);
    }

    void WaitSlingUpdate()
    {
        controller.enabled = false;

        // Smoothly return the player's aim to neutral when
        // waiting to sling
        smoothRopeAim = Vector3.Lerp
        (
            smoothRopeAim, 
            Vector3.zero,
            1.0f - Mathf.Exp(-8.0f * Time.deltaTime)
        );

        // Smoothly set the player's recoil velocity towards
        // zero. This will ensure the player will stop moving
        // on the rope eventually.
        recoilVelocity = Vector3.Lerp
        (
            recoilVelocity, 
            Vector3.zero,
            1.0f - Mathf.Exp(-8.0f * Time.deltaTime)
        );
        recoilOffset += recoilVelocity * Time.deltaTime;

        // Smoothly set the player's recoiled position back
        // to neutral to ensure they reach rest position.
        recoilOffset = Vector3.Lerp
        (
            recoilOffset, 
            Vector3.zero,
            1.0f - Mathf.Exp(-8.0f * Time.deltaTime)
        );

        // Apply the offsets from the rope rest position
        transform.position = ropeStart + smoothRopeAim * 10.0f + recoilOffset;

        if (Input.GetButtonDown("Action"))
        {
            state = State.ChargeSling;
            mouseAxis = Vector3.zero;
        }

        // Player model faces forward.
        // var playerModel = this.gameObject.transform.Find("MDL_CactusTall");
        // playerModel.transform.rotation = Quaternion.LookRotation(cam.transform.forward) * Quaternion.Euler(0, 90, 0);

        int layerMask = 1 << 6;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
        }
    }

    void ChargeSlingUpdate()
    {
        controller.enabled = false;
        Vector3 axisInput = GetAxisInput();

        // Smoothly set the player's aim to the given input
        smoothRopeAim = Vector3.Lerp
        (
            smoothRopeAim, 
            axisInput,
            1.0f - Mathf.Exp(-8.0f * Time.deltaTime)
        );

        // Smoothly set the player's recoil velocity towards
        // zero. This will ensure the player will stop moving
        // on the rope eventually.
        recoilVelocity = Vector3.Lerp
        (
            recoilVelocity, 
            Vector3.zero,
            1.0f - Mathf.Exp(-8.0f * Time.deltaTime)
        );
        recoilOffset += recoilVelocity * Time.deltaTime;

        // Smoothly set the player's recoiled position back
        // to neutral to ensure they reach rest position.
        recoilOffset = Vector3.Lerp
        (
            recoilOffset, 
            Vector3.zero,
            1.0f - Mathf.Exp(-8.0f * Time.deltaTime)
        );

        // Apply the offsets from the rope rest position
        transform.position = ropeStart + smoothRopeAim * 10.0f + recoilOffset;

        // Player model faces forward.
        // var playerModel = this.gameObject.transform.Find("MDL_CactusTall");
        // playerModel.transform.rotation = Quaternion.LookRotation(-smoothRopeAim) * Quaternion.Euler(0, 90, 0);

        if (Input.GetButtonUp("Action"))
        {
            if (axisInput.magnitude > 0.1f && Vector3.Dot(-axisInput.normalized, ropeForward) > 0.25f) {
                // Once the sling is released, start moving forward
                // (opposite of stick direction)
                transform.position = ropeStart - axisInput.normalized * 3.0f;
                forward = -axisInput.normalized;
                speed = slingSpeed;
                state = State.Move;
                if (currentRope != null) 
                {
                    currentRope.Release(forward * speed);
                    currentRope = null;
                }
            }
            else
            {
                // If the player is not aiming out of the rope, then
                // return to the waiting state
                state = State.WaitSling;
            }
        }

        int layerMask = 1 << 6;
        RaycastHit hit;
        Vector3 offset = new Vector3(0.0f, 1.0f, 0.0f);
        if (Physics.Raycast(transform.position + offset, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
        }
    }

    void MoveUpdate()
    {
        controller.enabled = true;

        // Decelerrate the player as time progresses
        speed -= decceleration * Time.deltaTime;
        speed = Mathf.Max(speed, 0.0f);

        // Set the sidestep upon input. 
        // Negative = right 
        // Positive = left
        if (Input.GetButtonDown("StepLeft"))
        {
            sideStepTimer = sideStepLength;

            // Play "Side Step" SFX
            SoundManager.instance.PlaySfx("Side Step");
        }
        if (Input.GetButtonDown("StepRight"))
        {
            sideStepTimer = -sideStepLength;

            // Play "Side Step" SFX
            SoundManager.instance.PlaySfx("Side Step");
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
            Vector3 direction = GetVelocity().normalized;

            // Using the direction of travel and the normal,
            // calculate the vector of reflection and use that
            // as the new forward vector
            forward = direction - 2 * Vector3.Dot(direction, flatNormal) * flatNormal;
            forward = forward.normalized;
            sideStepTimer = 0.0f;

            // Play the "Reflect" Sound
            SoundManager.instance.PlaySfx("Reflect");
        }
        else if (hit.gameObject.tag == "Rope")
        {
            // Once we hit a rope, go into the
            // WaitSling stat
            ropeForward = hit.gameObject.transform.forward;

            // Prevent attaching to incorrect ropes
            if (Vector3.Dot(ropeForward, GetVelocity().normalized) > 0.0f)
            {
                return;
            }
            ropeStart = transform.position;

            currentRope = hit.gameObject.GetComponent<Rope>();
            lastRope = currentRope;
            if (currentRope != null)
            {
                ropeStart = currentRope.Attach(gameObject);
            }
            smoothRopeAim = Vector3.zero;
            recoilVelocity = GetVelocity();

            state = State.WaitSling;
            sideStepTimer = 0.0f;
        }
    }
}
