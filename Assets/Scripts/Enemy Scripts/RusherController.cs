using UnityEngine;

public class RusherController : MonoBehaviour
{
    public enum State
    {
        Hunt,
        Return,
        Charge,
        Dizzy,
        Hit
    };

    [SerializeField] private float huntTime = 2.0f;
    [SerializeField] private float chargeSpeed = 250.0f;
    [SerializeField] private float dizzyTime = 2.0f;


    private bool landDizzy;
    private float huntTimer;
    private float dizzyTimer;
    private float returnSpeed;
    private Vector3 direction;
    private CharacterController controller;
    private State state;
    private GameObject player;
    private Hurtbox hurtbox;
    private Hitbox hitbox;
    private Knockback knockback;
    private GravityComponent gravity;
    private HealthComponent health;
    private Renderer renderer;

    void Start()
    {
        health = GetComponent<HealthComponent>();
        gravity = GetComponent<GravityComponent>();
        controller = GetComponent<CharacterController>();
        hurtbox = GetComponentInChildren<Hurtbox>();
        hitbox = GetComponentInChildren<Hitbox>();
        knockback = GetComponent<Knockback>();
        renderer = GetComponentInChildren<Renderer>();
        hurtbox.SubscribeOnHurt(OnHurt);
        hitbox.SubscribeOnHit(OnHit);
        player = GameObject.FindGameObjectWithTag("Player");
        huntTimer = 0.0f;
    }

    void StartReturn(float jumpStrength = 100.0f)
    {
        knockback.Stop();
        state = State.Return;
        float airTime = gravity.Jump(jumpStrength, 5.0f);

        Vector3 planarPosition = transform.position;
        planarPosition.y = 0.0f;
        Vector3 origin = Random.onUnitSphere * Random.Range(0.0f, 30.0f);
        Vector3 directionToOrigin = origin - planarPosition;
        float distanceToOrigin = directionToOrigin.magnitude;
        directionToOrigin /= distanceToOrigin;

        direction = directionToOrigin;
        returnSpeed = distanceToOrigin / airTime;
    }

    void Update()
    {
        switch (state)
        {
            case State.Hunt:
                huntTimer += Time.deltaTime;
                if (huntTimer > huntTime)
                {
                    huntTimer = 0.0f;
                    direction = (player.transform.position - transform.position).normalized;
                    direction.y = 0.0f;
                    state = State.Charge;
                }
                break;

            case State.Return:
                controller.Move(direction * returnSpeed * Time.deltaTime);
                if (gravity.grounded)
                {
                    if (landDizzy)
                    {
                        landDizzy = false;
                        state = State.Dizzy;
                        dizzyTimer = dizzyTime;
                    }
                    else
                    {
                        state = State.Hunt;
                    }
                    huntTimer = 0.0f;
                }
                break;

            case State.Charge:
                controller.Move(direction * chargeSpeed * Time.deltaTime);
                break;

            case State.Dizzy:
                dizzyTimer -= Time.deltaTime;
                if (dizzyTimer <= 0.0f)
                {
                    state = State.Hunt;
                }
                break;

            case State.Hit:
                if (knockback.Speed < 25.0f && gravity.grounded)
                {
                    StartReturn();
                }
                break;
        }

        if (state == State.Charge)
        {
            hitbox.active = true;
        }
        else
        {
            hitbox.active = false;
        }

        if (state == State.Dizzy || state == State.Hit || state == State.Hunt)
        {
            hurtbox.invincible = false;
        }
        else
        {
            hurtbox.invincible = true;
        }

        if (state == State.Charge)
        {
            renderer.material.color = Color.red;
        }
        else if (state == State.Dizzy || state == State.Hit || state == State.Hunt)
        {
            renderer.material.color = Color.green;
        }
        else
        {
            renderer.material.color = Color.blue;
        }

        if (transform.position.y < -50.0f && gravity.falling)
        {
            landDizzy = false;
            StartReturn(250.0f);
            health.Damage(20.0f);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (state == State.Hunt || state == State.Hit)
        {
            return;
        }

        if (hit.gameObject.tag != "Reflect" && hit.gameObject.tag != "Rope")
        {
            return;
        }
        
        // Do bounce physics on the rope upon contanct
        Rope rope = hit.gameObject.GetComponent<Rope>();
        if (rope != null)
        {
            rope.Bounce(hit.point, direction * chargeSpeed);
        }

        // If we hit something that reflects, get the
        // normal of the surface and flatten it
        Vector3 flatNormal = hit.normal;
        flatNormal.y = 0.0f;
        flatNormal = flatNormal.normalized;

        // Using the direction of travel and the normal,
        // calculate the vector of reflection and use that
        // as the new forward vector
        direction = direction - 2 * Vector3.Dot(direction, flatNormal) * flatNormal;
        direction = direction.normalized;

        if (state == State.Charge)
        {
            landDizzy = true;
            StartReturn();
        }
    }

    void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction) 
    {
        state = State.Hit;
    }

    void OnHit(Collider collider)
    {
        landDizzy = false;
        StartReturn();
    }
}