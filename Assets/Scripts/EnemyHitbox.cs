using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private GameObject parent;
    private float rotationalSpeed;
    
    void Awake()
    {
        parent = this.gameObject.transform.parent.gameObject;
        rotationalSpeed = Random.Range(15, 60);
    }

    void Update()
    {
        var parentPos = parent.transform.position;
        transform.RotateAround(parentPos, Vector3.up, rotationalSpeed * Time.deltaTime);
    }
}