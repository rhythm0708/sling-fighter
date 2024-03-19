using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement { get; private set; }
    private ShoulderCameraController cameraController;
    private DummyController dummy;
    private HitlagComponent hitlag;

    private Action onFallActions;
    private Action hitDummyActions;

    [SerializeField] private GameObject shoulderCam;
    [SerializeField] private GameObject clearCam;

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
    }

    private void Update()
    {
        if (transform.position.y < -25.0f)
        {
            SnapCameraForward();
            movement.AttachToLastRope();
            onFallActions?.Invoke();
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

    public void SubscribeOnAttachToRope(Action action)
    {
        movement.SubscribeOnAttachToRope(action);
    }
}