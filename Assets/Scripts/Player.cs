using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        WaitSling,
        ChargeSling,
        Move,
        ChargePunch
    };

    [SerializeField] private float punchDistance = 8.0f;
    [SerializeField] private float maxTurnSpeed = 25.0f;
    [SerializeField] private float turnAcceleration = 2.5f;
    [SerializeField] private float slingSpeed = 120.0f;
    [SerializeField] private float decceleration = 1.0f;
    [SerializeField] private float sideStepSpeed = 30.0f;
    [SerializeField] private float sideStepLength = 0.15f;

    private Vector3 forward;
    private float speed;
    private float turn;
    private State state;
    private float chargeAmount;
    private Vector3 ropeForward;
    private Camera camera;
    private float sideStepTimer;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        forward = forward.normalized;
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
        if (state == State.ChargePunch)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

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
        if (state == State.ChargeSling || state == State.ChargePunch)
        {
            Debug.DrawLine(transform.position, transform.position - GetAxisInput().normalized * punchDistance, Color.red);
        }
        Debug.DrawLine(transform.position, transform.position + forward * 5.0f, Color.green);
    }

    Vector3 GetAxisInput()
    {
        Vector3 axis = Vector3.zero;

        Vector3 cameraForward = camera.transform.forward;
        cameraForward.y = 0.0f;
        cameraForward = cameraForward.normalized;

        Vector3 cameraRight = camera.transform.right;
        cameraRight.y = 0.0f;
        cameraRight = cameraRight.normalized;

        axis += cameraRight * Input.GetAxisRaw("Horizontal");
        axis += cameraForward * Input.GetAxisRaw("Vertical");
        return Vector3.ClampMagnitude(axis, 1.0f);
    }

    void WaitSlingUpdate()
    {
        if (Input.GetButtonDown("Action"))
        {
            chargeAmount = 25.0f;
            state = State.ChargeSling;
        }
    }

    void ChargeSlingUpdate()
    {
        chargeAmount += 50.0f * Time.deltaTime; 
        if (Input.GetButtonUp("Action"))
        {
            forward = -GetAxisInput().normalized;
            speed = slingSpeed;
            turn = 0.0f;
            state = State.Move;
        }
    }

    void MoveUpdate()
    {
        speed -= decceleration * Time.deltaTime;
        turn += Input.GetAxisRaw("Horizontal") * turnAcceleration * Time.deltaTime;
        turn = Mathf.Clamp(turn, -1.0f, 1.0f);

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

        if (Mathf.Abs(turn) > 0.01f)
        {
            forward = Quaternion.AngleAxis(turn * maxTurnSpeed * Time.deltaTime, Vector3.up) * forward;
        }
        controller.Move(forward * speed * Time.deltaTime);

        if (Input.GetButtonDown("Action"))
        {
            state = State.ChargePunch;
        }
    }

    void ChargePunchUpdate()
    {
        if (Mathf.Abs(turn) > 0.01f)
        {
            forward = Quaternion.AngleAxis(turn * maxTurnSpeed * Time.deltaTime, Vector3.up) * forward;
        }
        controller.Move(forward * speed * Time.deltaTime);

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

        if (Input.GetButtonUp("Action"))
        {
            Vector3 axis = GetAxisInput().normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, axis, out hit, punchDistance))
            {
                speed = slingSpeed;
                turn = 0.0f;
                forward = -axis;
                sideStepTimer = 0.0f;
            }
            state = State.Move;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Reflect")
        {
            Vector3 flatNormal = hit.normal;
            flatNormal.y = 0.0f;
            flatNormal = flatNormal.normalized;

            Vector3 direction = forward;
            float sideStepFactor = sideStepTimer / sideStepLength; 
            direction += 
                Vector3.Cross(forward, transform.up) * 
                sideStepSpeed * 
                sideStepFactor;
            direction = direction.normalized;

            forward = direction - 2 * (Vector3.Dot(direction, flatNormal)) * flatNormal;
            forward = forward.normalized;
            turn = 0.0f;
            sideStepTimer = 0.0f;
        }
        else if (hit.gameObject.tag == "Rope")
        {
            ropeForward = hit.normal;
            state = State.WaitSling;
            sideStepTimer = 0.0f;
        }
    }
}
