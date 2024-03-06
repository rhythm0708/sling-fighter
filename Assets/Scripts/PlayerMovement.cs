using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
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
    [SerializeField] private float punchDistance = 8.0f;
    [SerializeField] private float slingSpeed = 120.0f;
    [SerializeField] private float decceleration = 1.0f;
    [SerializeField] private float sideStepSpeed = 30.0f;
    [SerializeField] private float sideStepLength = 0.15f;

    private Vector3 forward;
    private Vector3 mouseAxis;
    private bool useMouseAxis;
    private float speed;
    private State state;
    private Vector3 ropeForward;
    private Camera cam;
    private float sideStepTimer;

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
        if (state == State.ChargeSling)
        {
            slingPrevew.SetActive(true);
            slingPrevew.transform.rotation = Quaternion.LookRotation(-GetAxisInput());
        }
        else
        {
            slingPrevew.SetActive(false);
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
        Vector3 normalizeMouseAxis = mouseAxis.normalized;
        float rightAxis = useMouseAxis ? normalizeMouseAxis.x : Input.GetAxisRaw("Horizontal");
        float forwardAxis = useMouseAxis ? normalizeMouseAxis.z : Input.GetAxisRaw("Vertical");
        axis += cameraRight * rightAxis;
        axis += cameraForward * forwardAxis;
        return Vector3.ClampMagnitude(axis, 1.0f);
    }

    void WaitSlingUpdate()
    {
        if (Input.GetButtonDown("Action"))
        {
            state = State.ChargeSling;
            mouseAxis = Vector3.zero;
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
        speed = Mathf.Max(speed, 0.0f);

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
