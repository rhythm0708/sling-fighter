using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class HitlagComponent : MonoBehaviour
{
    [SerializeField] private float length = 0.15f;
    private float timer;
    private Hitbox hitbox;
    private Hurtbox hurtbox;
    List<MonoBehaviour> monoBehaviours;
    public float time 
    {
        get { return timer / length; }
    }

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

        monoBehaviours = new List<MonoBehaviour>();
        foreach (MonoBehaviour behaviour in GetComponentsInChildren<MonoBehaviour>()) {
            if (behaviour == this)
            {
                continue;
            }
            if (behaviour is IIgnoreHitlag)
            {
                continue;
            }
            monoBehaviours.Add(behaviour);
        }
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
            behaviour.enabled = false;
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

    void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction) 
    {
        StartHitlag();
    }

    void OnHit(Collider collider)
    {
        StartHitlag();
    }
}
