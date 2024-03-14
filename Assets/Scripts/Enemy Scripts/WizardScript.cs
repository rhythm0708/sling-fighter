using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Have to run into it in order to kill.
public class WizardScript : MonoBehaviour
{
    [SerializeField] private GameObject projTemplate;
    [SerializeField] private float rotationSpeed;
    private Hurtbox hurtbox;
    private GameObject player;
    private GameObject projectile;

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
        // if (properties.type == "Player")
        // {  
        //     Destroy(projectile);
        //     Destroy(gameObject);
        // }
    }
    
    void Update()
    {
        var targetRot = Quaternion.LookRotation(player.transform.position - transform.transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
        if (projectile == null)
        {
            projectile = Instantiate(projTemplate, transform.position, Quaternion.identity);
        }
    }
}