using UnityEngine;
using System.Collections.Generic;
using System;

public class Hitbox : MonoBehaviour
{
    [Serializable]
    public struct Properties {
        public float damage;
        public float knocback;
    }

    [SerializeField] private Properties _properties;
    public Properties properties {
        get { return _properties; }
    }

    List<Action> onHitActions;

     public void Awake()
    {
        onHitActions = new List<Action>();
    }

    public void SubscribeOnHit(Action action)
    {
        onHitActions.Add(action);
    }

    void OnTriggerEnter(Collider collider)
    {
        Hurtbox hurtbox = collider.gameObject.GetComponent<Hurtbox>();

        // Only check for hits if we actually contact a hitbox
        if (hurtbox == null)
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

        foreach (Action action in onHitActions)
        {
            action.Invoke();
        }
    }
}