using System;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    delegate void OnHurt();
    List<Action<Hitbox.Properties, Vector3>> onHurtActions;
    // private float damageCooldown;
    // private float timeElapsed = 0;

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
        onHurtActions = new List<Action<Hitbox.Properties, Vector3>>();
        // damageCooldown = UnityEngine.Random.Range(2, 5);
    }

    public void SubscribeOnHurt(Action<Hitbox.Properties, Vector3> action)
    {
        onHurtActions.Add(action);
    }

    void OnTriggerEnter(Collider collider)
    {
        Hitbox hitbox = collider.gameObject.GetComponent<Hitbox>();

        // Make sure that cooldown has expired.
        // if (timeElapsed < damageCooldown)
        // {
        //     return;
        // }
        // else{
        //     timeElapsed = 0;
        // }

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

        Vector3 direction = (transform.position - hitbox.transform.position).normalized;
        if (hitbox.directionOverride != Vector3.zero)
        {
            direction = Vector3.Lerp(direction, hitbox.directionOverride, 0.65f).normalized;
        }
            
        foreach (Action<Hitbox.Properties, Vector3> action in onHurtActions)
        {
            action.Invoke(hitbox.properties, direction);
        }
    }

    // void Update()
    // {
    //     timeElapsed -= Time.deltaTime;
    // }
}