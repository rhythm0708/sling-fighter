using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Have to run into it in order to kill.
public class WizardScript : MonoBehaviour
{
    [SerializeField] private GameObject projTemplate;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float projSpawnCooldown;
    private float timeElapsed = 0;
    private PlayerController player;
    private GameObject projectile;
    private bool projDestroyed = false;

    void Start()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        // Rotate to face the player.
        var targetRot = Quaternion.LookRotation(player.transform.position - transform.transform.position);
        transform.rotation = Quaternion.Lerp
        (
            transform.rotation,
            targetRot,
            Time.deltaTime * rotationSpeed
        );

        if (!projDestroyed && projectile == null)
        {
            projDestroyed = true;
            timeElapsed = 0;
        }
        
        if (projDestroyed && timeElapsed >= projSpawnCooldown)
        {
            projectile = Instantiate
            (
                projTemplate,
                transform.position,
                Quaternion.identity
            );
            projDestroyed = false;
        }

        timeElapsed += Time.deltaTime;
    }
}