using System;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    delegate void OnHurt();

    public delegate void OnHurtEventHandler(Collider param, Hitbox.Properties properties, Vector3 direction);
    private event OnHurtEventHandler onHurtEvent;

    public void Awake()
    {
        // For trigger to trigger collision, on trigger must have
        // a kinematic rigidbody.
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        rigidbody.isKinematic = true;
    }

    public void SubscribeOnHurt(OnHurtEventHandler action)
    {
        onHurtEvent += action;
    }

    void OnTriggerEnter(Collider collider)
    {
        Hitbox hitbox = collider.gameObject.GetComponent<Hitbox>();

        // Only check for hits if we actually contact a hitbox
        if (hitbox == null)
        {
            return;
        }
        // Ignore inactive hitboxes
        if (!hitbox.active)
        {
            return;
        }

        // A hitbox that shares the same root object as the
        // hurtbox should not trigger a hurt action, since
        // we don't want objects hurting themselves :)
        if (hitbox.transform.root == transform.root)
        {
            return;
        }

        Vector3 direction = transform.position - hitbox.transform.position;
        direction.y = 0.0f;
        direction = direction.normalized;
        if (hitbox.directionOverride != Vector3.zero)
        {
            direction = Vector3.Lerp(direction, hitbox.directionOverride, 0.75f).normalized;
        }

        onHurtEvent?.Invoke(collider, hitbox.properties, direction);
    }
}