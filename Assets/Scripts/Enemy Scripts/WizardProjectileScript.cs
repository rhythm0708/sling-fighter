using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracks player and explodes on collision with anything.
public class WizardProjectileScript : MonoBehaviour
{
    [SerializeField] private float trackingSpeed;
    private Hurtbox hurtbox;
    private GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
    }
    
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
        if (properties.type != "Enemy")
        {  
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        var currPos = transform.position;
        var playerPos = player.transform.position;
        transform.position = Vector3.MoveTowards(currPos, playerPos, trackingSpeed * Time.deltaTime);
    }
}