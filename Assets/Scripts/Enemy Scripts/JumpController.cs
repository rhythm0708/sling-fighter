using UnityEngine;

public class JumperController : MonoBehaviour
{
    public enum State
    {
        Land,
        Jump,
        Wave,
        Hit,
        Return,
    };

    [SerializeField] private float waveTime = 2.0f;
    [SerializeField] private float landTime = 250.0f;

    private float waveTimer;
    private float landTimer;
    private float speed;
    private Vector3 direction;
    private CharacterController controller;
    private State state;
    private GameObject player;
    private Hurtbox hurtbox;
    private Hitbox hitbox;
    private Knockback knockback;
    private GravityComponent gravity;
    private HealthComponent health;

    void Start()
    {
        health = GetComponent<HealthComponent>();
        gravity = GetComponent<GravityComponent>();
        controller = GetComponent<CharacterController>();
        hurtbox = GetComponentInChildren<Hurtbox>();
        hitbox = GetComponentInChildren<Hitbox>();
        knockback = GetComponent<Knockback>();
        hurtbox.SubscribeOnHurt(OnHurt);
        player = GameObject.FindGameObjectWithTag("Player");
        
        landTimer = 0.0f;
    }

    void StartReturn(float jumpStrength = 100.0f)
    {
        knockback.Stop();
        state = State.Return;
        float airTime = gravity.Jump(jumpStrength, 5.0f);

        Vector3 planarPosition = transform.position;
        planarPosition.y = 0.0f;
        Vector3 directionToOrigin = Vector3.zero - planarPosition;
        float distanceToOrigin = directionToOrigin.magnitude;
        directionToOrigin /= distanceToOrigin;

        direction = directionToOrigin;
        speed = distanceToOrigin / airTime;
    }

    void StartJump(Vector3 target, float jumpStrength = 100.0f)
    {
        knockback.Stop();
        state = State.Jump;
        float airTime = gravity.Jump(jumpStrength, 5.0f);

        Vector3 planarPosition = transform.position;
        planarPosition.y = 0.0f;
        target.y = 0.0f;
        Vector3 directionToTarget = target - planarPosition;
        float distanceToOrigin = directionToTarget.magnitude;
        directionToTarget /= distanceToOrigin;

        direction = directionToTarget;
        speed = distanceToOrigin / airTime;
    }

    void Update()
    {
        switch (state)
        {
            case State.Land:
                landTimer += Time.deltaTime;
                if (landTimer > landTime)
                {
                    landTimer = 0.0f;
                    Vector3 target = Random.onUnitSphere * Random.Range(0.0f, 80.0f);
                    StartJump(target);
                }
                break;

            case State.Jump:
                controller.Move(direction * speed * Time.deltaTime);
                if (gravity.grounded)
                {
                    state = State.Wave;
                }
                break;

            case State.Wave:
                waveTimer += Time.deltaTime;
                if (waveTimer > waveTime)
                {
                    waveTimer = 0.0f;
                    state = State.Land;
                }
                break;

            case State.Return:
                controller.Move(direction * speed * Time.deltaTime);
                if (gravity.grounded)
                {
                    state = State.Wave;
                }
                break;

            case State.Hit:
                if (knockback.Speed < 25.0f && gravity.grounded)
                {
                    StartReturn();
                }
                break;
        }

        if (state == State.Wave)
        {
            hitbox.active = true;
        }
        else
        {
            hitbox.active = false;
        }

        if (state == State.Jump || state == State.Wave || state == State.Return)
        {
            hurtbox.invincible = true;
        }
        else
        {
            hurtbox.invincible = false;
        }

        if (transform.position.y < -50.0f && gravity.falling)
        {
            StartReturn(250.0f);
            health.Damage(20.0f);
        }
    }

    void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction) 
    {
        state = State.Hit;
    }
}