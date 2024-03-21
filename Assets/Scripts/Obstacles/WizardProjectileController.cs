using UnityEngine;

// Tracks player and explodes on collision with anything.
public class WizardProjectileController  : MonoBehaviour
{
    [SerializeField] private float timeSubtraction = 5.0f;
    [SerializeField] private float trackingSpeed;
    [SerializeField] private Transform model;
    [SerializeField] private bool destroyOnPlayerOnly = true;
    [SerializeField] private bool onlyHitOnRopes = true;
    private Vector3 initialScale;
    private PlayerController player;
    private DummyController dummy;

    void Start()
    {
        player = GameManager.Instance.player;
        dummy = GameManager.Instance.dummy; 
        initialScale = model.localScale;
    }

    void Update()
    {
        Vector3 planarPlayerPosition = new Vector3
        (
            player.transform.position.x,
            transform.position.y,
            player.transform.position.z
        );

        transform.position = Vector3.MoveTowards
        (
            transform.position, 
            planarPlayerPosition, 
            trackingSpeed * Time.deltaTime
        );

        float sinTime = (Mathf.Sin(Time.time * 25.0f) + 1.0f) * 0.5f;
        sinTime *= sinTime;
        model.transform.localScale = 
            initialScale + 
            Vector3.one * 0.5f * sinTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool ropeValid = true;
        if (onlyHitOnRopes && player.movement.GetState() == PlayerMovement.State.Move)
        {
            ropeValid = false;
        }

        if (other.transform.root == player.transform.root)
        {
            if (ropeValid)
            {
                GameManager.Instance.SubtractTime(timeSubtraction);
            }
            Destroy(gameObject);
        }
        else if (!other.transform.parent.gameObject.name.Contains("Spikes")
                && !destroyOnPlayerOnly)
        {
            Destroy(gameObject);
        }
    }
}