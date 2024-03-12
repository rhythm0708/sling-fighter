using System;
using System.Collections.Generic;
using UnityEngine;

// Have enemies run into it to kill them.
public class PillarScript : MonoBehaviour
{
    private Hurtbox hurtbox;
    private bool initiated = false;

     public bool Initiated { get => initiated; }

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
            initiated = true;
        }
    }
}