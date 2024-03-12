using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Picks random enemy and follows it at a slow speed.
public class HomingMissileScript : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    private Hurtbox hurtbox;
    private bool isFollowing = false;
    private bool initiated = false;
    private GameObject enemyToFollow;

     public bool Initiated { get => initiated; }

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
        if (!initiated && properties.type == "Player")
        {
            // Errors when there are no enemies, but we should move onto next wave.
            // Hence, need not be handled.
            System.Random rand = new System.Random();
            GameObject[] enemiesArr = GameObject.FindGameObjectsWithTag("Enemy");
            List<GameObject> enemiesList = enemiesArr.ToList();
            enemiesList.RemoveAll(enemy => enemy.name != "Rusher(Clone)");
            if (enemiesList.Count > 0)
            {
                enemyToFollow = enemiesList[rand.Next(enemiesList.Count)];
                initiated = true;
                isFollowing = true;
            }
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
            // Caught up -> explode on collision.
            Destroy(this.gameObject);
        }
    }
}