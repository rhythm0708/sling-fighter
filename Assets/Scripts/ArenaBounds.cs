using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBounds : MonoBehaviour
{
    // Reference to player.
    GameObject player; 

    public delegate void OnBoundsEventHandler();
    private event OnBoundsEventHandler onBoundsEvent;

    public void SubscribeOnHit(OnBoundsEventHandler action)
    {
        onBoundsEvent += action;
    }

    public void UnsubscribeOnHit(OnBoundsEventHandler action)
    {
        onBoundsEvent -= action;
    }

    private void Start()
    {
        // Get reference to player.
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (player.transform.position.y < -25.0f)
        {
            onBoundsEvent?.Invoke();
        }
    }
}
