using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Vector3 apexOffset;
    private Transform target;
    private Renderer renderer;
    private Vector3 localTargetPos;
    private Vector3 velocity;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void Attach(GameObject gameObject)
    {
        target = gameObject.transform;
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
            localTargetPos = transform.InverseTransformPoint(target.transform.position) + apexOffset;
        }
        else 
        {
            Vector3 worldPosition = transform.TransformPoint(localTargetPos);
            Vector3 worldTargetPosition = transform.TransformPoint(Vector3.zero);

            velocity = Vector3.Lerp
            (
                velocity, 
                Vector3.zero,
                1.0f - Mathf.Exp(-16.0f * Time.deltaTime)
            );
            worldPosition += velocity * Time.deltaTime;
            worldPosition = Vector3.Lerp
            (
                worldPosition, 
                worldTargetPosition,
                1.0f - Mathf.Exp(-16.0f * Time.deltaTime)
            );

            localTargetPos = transform.InverseTransformPoint(worldPosition);
        }
        renderer.material.SetVector("_Point", localTargetPos);
    }
}
