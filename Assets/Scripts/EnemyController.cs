using System;
using System.Collections.Generic;
using UnityEngine;

// TODO: Possibly integrate enemy healthbar in the future.
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    private Hurtbox hurtbox;
    private List<Action<EnemyController>> onDestroyActions;

    // For other scripts to access Health.
    public float Health { get => health; }

    void Awake()
    {
        onDestroyActions = new List<Action<EnemyController>>();
    }

    void Start()
    {
        hurtbox = GetComponentInChildren<Hurtbox>();
        if (hurtbox != null)
        {
            hurtbox.SubscribeOnHurt(OnHurt);
        }
    }

    public void SubscribeOnDestroy(Action<EnemyController> action)
    {
        onDestroyActions.Add(action);
    }

    // Lose appropriate amount of health.
    public void OnHurt(Hitbox.Properties properties, Vector3 direction)
    {
        // Potentially integrate a damage table?
        health -= properties.damage;
        if (health <= 0)
        {
            foreach (Action<EnemyController> action in onDestroyActions)
            {
                action.Invoke(this);
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Handle enemies falling off arena.
        var currPos = gameObject.transform.position;
        if (currPos.y < -1)
        {
            Destroy(gameObject);
        }
    }
}