using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement { get; private set; }
    private ShoulderCameraController cameraController;
    private DummyController dummy;
    private HitlagComponent hitlag;
    private GravityComponent gravity;

    private Action onFallActions;
    private Action hitDummyActions;

    [SerializeField] private GameObject shoulderCam;
    [SerializeField] private GameObject clearCam;
    [SerializeField] private ParticleSystem moveParticles;

    public bool moving
    {
        get { return movement.GetState() == PlayerMovement.State.Move; }
    }

    [SerializeField] private float baseDamageOutput = 10.0f; 
    
    // This property can be modified later to adjust the damage
    // output. This is where someone would write damage scaling 
    // for combo counter.
    public float damageOutput
    {
        get 
        {
            return baseDamageOutput;
        }
    }

    void Start()
    {
        dummy = GameManager.Instance.dummy;
        cameraController = GetComponentInChildren<ShoulderCameraController>();
        movement = GetComponent<PlayerMovement>();
        hitlag = GetComponent<HitlagComponent>();
        gravity = GetComponent<GravityComponent>();
    }

    private void Update()
    {
        if (transform.position.y < -25.0f)
        {
            SnapCameraForward();
            movement.AttachToLastRope();
            onFallActions?.Invoke();
        }

        if (movement.GetState() == PlayerMovement.State.Move && gravity.grounded)
        {
            moveParticles.Play();
        }
        else
        {
            moveParticles.Stop();
        }
    }

    private void SnapCameraForward()
    {
        var forward = movement.GetForward();
        cameraController.SnapToForward(forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == dummy.transform.root && moving)
        {
            moveParticles.Pause();
            hitDummyActions?.Invoke();
            hitlag.StartHitlag();
        }
    }

    public void SubscribeOnFall(Action action)
    {
        onFallActions += action;
    }

    public void SubscribeOnHitDummy(Action action)
    {
        hitDummyActions += action;
    }
}