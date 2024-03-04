using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSpawnRotation : MonoBehaviour
{
    [SerializeField] private Vector3 min;
    [SerializeField] private Vector3 max;
    void Start()
    {
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        float z = Random.Range(min.z, max.z);
        transform.rotation = Quaternion.Euler(x, y, z);
    }
}
