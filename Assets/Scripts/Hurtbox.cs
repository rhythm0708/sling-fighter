using System;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    delegate void OnHurt();
    List<Action<Hitbox.Properties>> onHurtActions;

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
        onHurtActions = new List<Action<Hitbox.Properties>>();
    }

    public void SubscribeOnHurt(Action<Hitbox.Properties> action)
    {
        onHurtActions.Add(action);
    }

    void OnTriggerEnter(Collider collider)
    {
        Hitbox hitbox = collider.gameObject.GetComponent<Hitbox>();

        // Only check for hits if we actually contact a hitbox
        if (hitbox == null)
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

        foreach (Action<Hitbox.Properties> action in onHurtActions)
        {
            action.Invoke(hitbox.properties);
        }
    }
}