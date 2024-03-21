using UnityEngine;

// Have to run into it in order to kill.
public class WizardController : MonoBehaviour
{
    [SerializeField] private GameObject projTemplate;
    [SerializeField] private float projSpawnCooldown;
    [SerializeField] private float rotationSpeed;
    private PlayerController player;
    private GameObject projectile;
    private Bouncer bouncer;

    private float timeElapsed = 0;
    private bool projDestroyed = false;

    void Start()
    {
        player = GameManager.Instance.player;
        bouncer = GetComponent<Bouncer>();
    }

    void Update()
    {
        Vector3 planarPlayer = player.transform.position;
        planarPlayer.y = 0.0f;

        Vector3 planarPosition = transform.position;
        planarPosition.y = 0.0f;

        var targetRot = Quaternion.LookRotation(planarPlayer- planarPosition);
        transform.rotation = Quaternion.Slerp
        (
            transform.rotation, 
            targetRot,
            1.0f - Mathf.Exp(-rotationSpeed * Time.deltaTime)
        );

        if (!projDestroyed && projectile == null)
        {
            projDestroyed = true;
            timeElapsed = 0.0f;
        }

        if (projDestroyed && timeElapsed > projSpawnCooldown)
        {
            bouncer.Bounce();
            projectile = Instantiate(
                projTemplate, 
                transform.position + new Vector3(0.0f, 8.0f, 0.0f),
                Quaternion.identity
            );
            projDestroyed = false;
        }

        timeElapsed += Time.deltaTime;
    }
}