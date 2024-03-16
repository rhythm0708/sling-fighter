using UnityEngine;

public class Tracker : MonoBehaviour
{
    private PlayerController player;
    private float trackingSpeed;

    void Awake()
    {
        player = GameManager.Instance.player;
        trackingSpeed = Random.Range(10, 30);
    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, trackingSpeed * Time.deltaTime);
    }
}
