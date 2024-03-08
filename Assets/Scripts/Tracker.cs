using UnityEngine;

public class Tracker : MonoBehaviour
{
    private GameObject player;
    private float trackingSpeed;

    void Awake()
    {
        player = GameObject.Find("Player");
        trackingSpeed = Random.Range(10, 30);
    }
    
    void Update()
    {
        var currPos = transform.position;
        var playerPos = player.transform.position;
        transform.position = Vector3.MoveTowards(currPos, playerPos, trackingSpeed * Time.deltaTime);
    }
}
