using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Vector3 apexOffset;
    private Transform target;
    private Renderer renderer;
    private Vector3 localTargetPos;
    private Vector3 velocity;
    private BoxCollider collider;
    private float power = 1.25f;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<BoxCollider>();
    }

    public Vector3 Attach(GameObject gameObject)
    {
        target = gameObject.transform;
        Vector3 localAttachPoint = transform.InverseTransformPoint(gameObject.transform.position);
        localAttachPoint.z = 0.0f;
        localAttachPoint -= apexOffset;
        return transform.TransformPoint(localAttachPoint);
    }

    public void Release(Vector3 releaseVelocity)
    {
        target = null;
        velocity = releaseVelocity;
    }

    public void Bounce(Vector3 position, Vector3 bounceVelocity) 
    {
        localTargetPos = transform.InverseTransformPoint(position);
        localTargetPos.y = 0.0f;
        localTargetPos.z = 0.0f;
        velocity = bounceVelocity;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Convert the target's world position to local space
            localTargetPos = transform.InverseTransformPoint(target.transform.position) + apexOffset;
            localTargetPos.y = 0.0f;
            power = 1.0f;
            collider.center = new Vector3(collider.center.x, collider.center.y, localTargetPos.z);
        }
        else 
        {
            // Since the transform is stored in local space, we need to convert it to world space
            // to properly compute physics
            Vector3 worldPosition = transform.TransformPoint(localTargetPos);
            Vector3 worldTargetPosition = transform.TransformPoint(Vector3.zero);
            
            // Smoothly interpolate the velocity towards zero so the rope
            // stops moving
            velocity = Vector3.Lerp
            (
                velocity, 
                Vector3.zero,
                1.0f - Mathf.Exp(-16.0f * Time.deltaTime)
            );
            worldPosition += velocity * Time.deltaTime;

            // Smoothly interpolate towards the rope's rest position, so
            // even when velocity == 0, the rope gets to rest
            worldPosition = Vector3.Lerp
            (
                worldPosition, 
                worldTargetPosition,
                1.0f - Mathf.Exp(-16.0f * Time.deltaTime)
            );

            power = Mathf.Lerp
            (
                power, 
                1.25f,
                1.0f - Mathf.Exp(-16.0f * Time.deltaTime)
            );

            // Apply the position to local space
            localTargetPos = transform.InverseTransformPoint(worldPosition);
            collider.center = Vector3.zero;
        }

        // Apply the position to the shader
        renderer.material.SetVector("_Point", localTargetPos);
        renderer.material.SetFloat("_Power", power);
    }
}
