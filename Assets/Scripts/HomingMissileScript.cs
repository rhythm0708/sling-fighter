using System;
using System.Collections.Generic;
using UnityEngine;

// Picks random enemy and follows it at a slow speed.
public class HomingMissileScript : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    private Hurtbox hurtbox;
    private bool isFollowing = false;
    private GameObject enemyToFollow;

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
        if (properties.type == "Player")
        {
            isFollowing = true;
            // Errors when there are no enemies, but we should move onto next wave.
            // Hence, need not be handled.
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyToFollow = enemies[UnityEngine.Random.Range(0, enemies.Length)];
        }
    }

    void Update()
    {
        if (isFollowing && enemyToFollow != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyToFollow.transform.position, Time.deltaTime * followSpeed);
        }
        else if (isFollowing && enemyToFollow == null)
        {
            // Caught up -> explode on collision
            Destroy(this.gameObject);
        }
    }
}