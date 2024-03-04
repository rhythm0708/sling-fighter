using UnityEngine;

public class Tracker : MonoBehaviour
{
    private GameObject player;
    private float trackingSpeed;

    void Awake()
    {
        player = GameObject.Find("Player");
        // Different enemies have different movement speeds.
        switch (gameObject.name.Replace("(Clone)", ""))
        {
            case "DummyEnemy":
                trackingSpeed = 0;
                break;
            case "OrbitEnemy":
                trackingSpeed = Random.Range(10, 30);
                break;
            default:
                trackingSpeed = Random.Range(5, 10);
                break;
        }
    }
    
    void Update()
    {
        var currPos = transform.position;
        var playerPos = player.transform.position;
        transform.position = Vector3.MoveTowards(currPos, playerPos, trackingSpeed * Time.deltaTime);
    }
}
