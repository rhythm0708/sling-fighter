using UnityEngine;

// Tracks player and explodes on collision with anything.
public class WizardProjectileController  : MonoBehaviour
{
    [SerializeField] private float timeSubtraction = 5.0f;
    [SerializeField] private float trackingSpeed;
    private PlayerController player;

    void Start()
    {
        player = GameManager.Instance.player;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == player.transform.root)
        {
            GameManager.Instance.SubtractTime(timeSubtraction);
            Destroy(gameObject);
        }
    }
}