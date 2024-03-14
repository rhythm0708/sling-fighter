using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class Hitbox : MonoBehaviour
{
    [Serializable]
    public struct Properties {
        public float damage;
        public float knockback;
        public string type;
    }

    private Vector3 _directionOverride;
    public Vector3 directionOverride {
        get { return _directionOverride; }
        set { _directionOverride = value; }
    }

    [SerializeField] private Properties _properties;
    public Properties properties 
    {
        get { return _properties; }
    }

    [SerializeField] private bool _active = true;
    public bool active
    {
        get { return _active; }
        set { _active = value; }
    }

    public delegate void OnHitEventHandler(Collider param);

    private event OnHitEventHandler onHitEvent;

    public void Awake()
    {
        directionOverride = Vector3.zero;
    }

    public void SubscribeOnHit(OnHitEventHandler action)
    {
        onHitEvent += action;
    }

    public void UnsubscribeOnHit(OnHitEventHandler action)
    {
        onHitEvent -= action;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!_active)
        {
            return;
        }
        Hurtbox hurtbox = collider.gameObject.GetComponent<Hurtbox>();

        // Only check for hits if we actually contact a hitbox
        if (hurtbox == null)
        {
            return;
        }

        if (hurtbox.invincible)
        {
            return;
        }

        // A hitbox that shares the same root object as the
        // hurtbox should not trigger a hurt action, since
        // we don't want objects hurting themselves :)
        if (hurtbox.transform.root == transform.root)
        {
            return;
        }

        onHitEvent?.Invoke(collider);
    }
}