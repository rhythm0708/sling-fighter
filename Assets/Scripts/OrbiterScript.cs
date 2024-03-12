using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Hurtbox hurtbox;

    void Start()
    {
        hurtbox = GetComponentInChildren<Hurtbox>();
        if (hurtbox != null)
        {
            hurtbox.SubscribeOnHurt(OnHurt);
        }
    }

    public void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        if (properties.type == "Player")
        {
            Destroy(gameObject);
        }
    }
}