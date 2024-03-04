using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class HitlagComponent : MonoBehaviour
{
    [SerializeField] private float length = 0.05f;
    private float timer;
    private Hitbox hitbox;
    private Hurtbox hurtbox;
    List<MonoBehaviour> monoBehaviours;

    void Start()
    {
        hurtbox = GetComponentInChildren<Hurtbox>();
        if (hurtbox != null)
        {
            hurtbox.SubscribeOnHurt(OnHurt);
        }

        hitbox = GetComponentInChildren<Hitbox>();
        if (hitbox != null)
        {
            hitbox.SubscribeOnHit(OnHit);
        }

        monoBehaviours = GetComponentsInChildren<MonoBehaviour>().ToList();
        timer = 0.0f;
    }

    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0.0f)
        {
            StopHitlag();
        }
    }

    public void StartHitlag()
    {
        timer = length;
        foreach (MonoBehaviour behaviour in monoBehaviours)
        {
            if (behaviour != this)
            {
                behaviour.enabled = false;
            }
        }
    }

    public void StopHitlag()
    {
        timer = 0.0f;
        foreach (MonoBehaviour behaviour in monoBehaviours)
        {
            behaviour.enabled = true;
        }
    }

    void OnHurt(Hitbox.Properties properties, Vector3 direction) 
    {
        StartHitlag();
    }

    void OnHit()
    {
        StartHitlag();
    }
}
