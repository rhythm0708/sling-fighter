using System.Collections.Generic;
using UnityEngine;

public class HitlagComponent : MonoBehaviour
{
    private const float LENGTH = 0.1f;
    private float timer;
    List<MonoBehaviour> monoBehaviours;
    public float time 
    {
        get { return timer / LENGTH; }
    }

    void Start()
    {
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
        timer = LENGTH;
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
}
