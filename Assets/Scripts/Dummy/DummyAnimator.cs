using UnityEngine;

public class DummyAnimator : MonoBehaviour, IIgnoreHitlag
{
    private DummyController dummy;
    private Animator animator;
    private HitlagComponent hitlag;

    bool didHit;
    int hitIndex = 0;

    void Start()
    {
        dummy = GetComponent<DummyController>();
        hitlag = GetComponent<HitlagComponent>();
        animator = GetComponentInChildren<Animator>();

        dummy.SubscribeOnHitByPlayer(OnHitByPlayer);
    }

    void Update()
    {
        if (hitlag.time > 0.0f && didHit)
        {
            animator.speed = 0.0f;
        }
        else
        {
            animator.speed = 1.0f;
            //if (didPunch)
            //{
                //animator.transform.rotation = Quaternion.LookRotation(-player.movement.GetForward());
            //}
        }

        if (!didHit)
        {
            didHit = true;
        }
        else
        {
            animator.SetInteger("HitIndex", -1);
        }
    }

    void OnHitByPlayer() {
        hitIndex += 1;
        if (hitIndex > 1)
        {
            hitIndex = 0;
        }
        animator.SetInteger("HitIndex", hitIndex);

        didHit = false;
        //Vector3 direction = GameManager.Instance.dummy.transform.position - player.transform.position;
        //direction.y = 0.0f;
        //direction = direction.normalized;
        //direction = Vector3.Lerp(direction, player.GetComponent<PlayerMovement>().GetVelocity().normalized, 0.75f).normalized;

        //animator.transform.rotation = Quaternion.LookRotation(-direction);
    }
}
