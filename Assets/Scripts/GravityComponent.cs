using UnityEngine;

public class GravityComponent : MonoBehaviour
{
    [SerializeField] private float acceleration = 1.0f;
    private float gravity;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gravity = 0.0f;
    }

    void Update()
    {
        if (!controller.enabled)
        {
            return;
        }
        RaycastHit hit;

        // Only raycast on the aerna
        int layerMask = 1 << 6;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f, layerMask))
        {
            gravity = 0.0f;
            controller.Move(Vector3.down * 20.0f * Time.deltaTime);
        }
        else
        {
            controller.Move(Vector3.down * gravity);
            gravity += acceleration * Time.deltaTime;
        }
    }
}
