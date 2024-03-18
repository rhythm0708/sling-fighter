using UnityEngine;
using System;

public class DummyController : MonoBehaviour
{
    [SerializeField] private string _displayName;
    public string displayName { get => _displayName; }

    [SerializeField] private float _maxHealth = 100.0f;
    [SerializeField] private float falloffDamage = 20.0f;
    public float maxHealth { get => _maxHealth; }
    public float health { get; private set; }

    private PlayerController player;

    private bool returning;
    private float returnSpeed;
    private Vector3 returnDirection;

    [SerializeField] private float knockbackStrength = 400.0f;
    [SerializeField] private float knockbackDecay = 2.0f;
    private Vector3 knockbackVelocity;

    private CharacterController controller;
    private GravityComponent gravity;
    private HitlagComponent hitlag;

    private Action onSlainActions;
    private Action hitByPlayerActions;

    void Start()
    {
        gravity = GetComponent<GravityComponent>();
        controller = GetComponent<CharacterController>();
        hitlag = GetComponent<HitlagComponent>();

        health = maxHealth;

        player = GameManager.Instance.player;
        returning = false;
        knockbackVelocity = Vector3.zero;
    }

    void StartReturn(float jumpStrength = 100.0f)
    {
        knockbackVelocity = Vector3.zero;
        float airTime = gravity.Jump(jumpStrength, 5.0f);

        Vector3 planarPosition = transform.position;
        planarPosition.y = 0.0f;
        Vector3 directionToOrigin = Vector3.zero - planarPosition;
        float distanceToOrigin = directionToOrigin.magnitude;
        directionToOrigin /= distanceToOrigin;

        returnDirection = directionToOrigin;
        returnSpeed = distanceToOrigin / airTime;
        returning = true;
        Damage(falloffDamage, false);
    }

    void Update()
    {
        if (returning) 
        {
            // Use returning velocity when in returning state
            controller.Move(returnDirection * returnSpeed * Time.deltaTime);

            // Returning state ends upon touching the ground
            if (gravity.grounded)
            {
                returning = false;
            }
        }
        else
        {
            // Smoothly interpolate the knockback velocity to zero
            // this simulates friction.
            knockbackVelocity = Vector3.Lerp
            (
                knockbackVelocity, 
                Vector3.zero,
                1.0f - Mathf.Exp(-knockbackDecay * Time.deltaTime)
            );
            controller.Move(knockbackVelocity * Time.deltaTime);
        }

        if (transform.position.y < -50.0f)
        {
            StartReturn(250.0f);
        }
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
            rope.Bounce(hit.point, knockbackVelocity);
        }

        // If we hit something that reflects, get the
        // normal of the surface and flatten it
        Vector3 flatNormal = hit.normal;
        flatNormal.y = 0.0f;
        flatNormal = flatNormal.normalized;

        // Determine the direction of travel.
        // This calcualtion accounts for sidestepping
        Vector3 direction = knockbackVelocity.normalized;
        
        // Using the direction of travel and the normal,
        // calculate the vector of reflection and use that
        // as the new forward vector
        direction = direction - 2 * Vector3.Dot(direction, flatNormal) * flatNormal;
        direction = direction.normalized;
        knockbackVelocity = direction * knockbackVelocity.magnitude; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == player.transform.root && player.moving)
        {
            // Apply knockback by determing the direction of approach
            Vector3 direction = transform.position - player.transform.position;
            direction.y = 0.0f;
            direction = direction.normalized;
            direction = Vector3.Lerp(direction, player.GetComponent<PlayerMovement>().GetVelocity().normalized, 0.75f).normalized;
            knockbackVelocity = direction * knockbackStrength;

            Damage(player.damageOutput);
            hitByPlayerActions?.Invoke();
            hitlag.StartHitlag();
        }
    }

    public void Damage(float damage, bool canKill = true)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            health = 0.0f;
            if (canKill)
            {
                onSlainActions?.Invoke();

                // Play "Slain" sound
                SoundManager.instance.StopSfx("Rope");
                SoundManager.instance.PlaySfx("Slain");
            }
        }
        // Play the "On Damage" sound
        SoundManager.instance.PlaySfx("On Hit");
        SoundManager.instance.StopSfx("Rope");
    }

    public void SubscribeOnSlain(Action action)
    {
        onSlainActions += action;
    }

    public void SubscribeOnHitByPlayer(Action action)
    {
        hitByPlayerActions += action;
    }
}