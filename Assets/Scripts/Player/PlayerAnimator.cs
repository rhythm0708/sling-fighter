using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IIgnoreHitlag
{
    private PlayerController player;
    private Animator animator;
    private HitlagComponent hitlag;

    bool didPunch;
    int punchIndex = 0;

    void Start()
    {
        player = GetComponent<PlayerController>();
        hitlag = GetComponent<HitlagComponent>();
        animator = GetComponentInChildren<Animator>();

        player.SubscribeOnHitDummy(OnHitDummy);
    }

    void Update()
    {
        if (hitlag.time > 0.0f && didPunch)
        {
            animator.speed = 0.0f;
        }
        else
        {
            animator.speed = 1.0f;
            if (didPunch)
            {
                animator.transform.rotation = Quaternion.LookRotation(-player.movement.GetForward());
            }
        }

        animator.SetBool("Move", player.movement.GetState() == PlayerMovement.State.Move);
        animator.SetBool("Charge", player.movement.GetState() == PlayerMovement.State.ChargeSling);

        if (!didPunch)
        {
            didPunch = true;
        }
        else
        {
            animator.SetInteger("PunchIndex", -1);
        }
    }

    void OnHitDummy() {
        punchIndex += 1;
        if (punchIndex > 1)
        {
            punchIndex = 0;
        }
        animator.SetInteger("PunchIndex", punchIndex);

        didPunch = false;
        Vector3 direction = GameManager.Instance.dummy.transform.position - player.transform.position;
        direction.y = 0.0f;
        direction = direction.normalized;
        direction = Vector3.Lerp(direction, player.GetComponent<PlayerMovement>().GetVelocity().normalized, 0.75f).normalized;

        animator.transform.rotation = Quaternion.LookRotation(-direction);
    }
}
