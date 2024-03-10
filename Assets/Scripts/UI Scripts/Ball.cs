using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movementSpeed;
    private float dirX, dirZ;
    private static Ball instance;
    private void Awake()
    {
        // Ensure only one instance of Ball exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += Vector3.up;
        }
        dirX = Input.GetAxis("Horizontal") * movementSpeed;
        dirZ = Input.GetAxis("Vertical") * movementSpeed;

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(dirX,rb.velocity.y,dirZ );
    }
}