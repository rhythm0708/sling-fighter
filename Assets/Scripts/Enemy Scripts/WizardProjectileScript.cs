using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracks player and explodes on collision with anything.
public class WizardProjectileScript : MonoBehaviour
{
    [SerializeField] private float trackingSpeed;
    private PlayerController player;

    void Start()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards
        (
            transform.position, 
            player.transform.position, 
            trackingSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            Destroy(this);
            Debug.Log("Projectile break");
        }
    }
}