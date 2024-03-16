using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Have to run into it in order to kill.
public class WizardScript : MonoBehaviour
{
    [SerializeField] private GameObject projTemplate;
    [SerializeField] private float rotationSpeed;
    private PlayerController player;
    private GameObject projectile;

    void Awake()
    {
        player = GameManager.Instance.player;
    }
    
    void Start()
    {
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