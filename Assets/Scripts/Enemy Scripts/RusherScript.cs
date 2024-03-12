using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only way to kill is with a tool.
public class RusherScript : MonoBehaviour
{
    [SerializeField] private float trackingSpeed;
    private GameObject player;
    private Hurtbox hurtbox;

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

    // Lose appropriate amount of health.
    public void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction)
    {
        if (properties.type == "Tool")
        {  
            GameObject tool = collider.transform.parent.gameObject;
            bool initiated = false;
            switch (tool.name.Replace("(Clone)", ""))
            {
                case "HomingMissile":
                    initiated = tool.GetComponent<HomingMissileScript>().Initiated;
                    break;
                case "DummyEnemy":
                    initiated = tool.GetComponent<Knockback>().Initiated;
                    break;
                case "Pillar":
                    initiated = tool.GetComponent<PillarScript>().Initiated;
                    break;
                default:
                    break;
            }

            if (initiated)
            {
                Destroy(gameObject);
            }
        }
    }
    
    void Update()
    {
        var currPos = transform.position;
        var playerPos = player.transform.position;
        transform.position = Vector3.MoveTowards(currPos, playerPos, trackingSpeed * Time.deltaTime);
    }
}