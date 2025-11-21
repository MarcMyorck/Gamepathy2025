using UnityEngine;

public class CartFollower : MonoBehaviour
{
    public Rigidbody2D rb;
    public float followStrength = 50f;      // spring force toward desired position
    public float maxFollowSpeed = 10f;      // clamp the corrective velocity
    public float damping = 5f;              // damps relative velocity for stability
    public float rotationSpeed = 10f;       // how fast the cart rotates to face movement

    private Transform target;               // player transform
    private Vector2 targetLocalOffset = Vector2.zero; // offset in local player space
    private bool hasTarget = false;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!hasTarget || target == null) return;

        // desired world position based on player's transform and local offset
        Vector2 desired = (Vector2)target.TransformPoint(targetLocalOffset);

        // compute spring-like force: proportional to displacement and damp relative velocity
        Vector2 toTarget = desired - rb.position;
        //Vector2 relativeVel = rb.linearVelocity - (Vector2)target.GetComponent<Rigidbody2D>()?.linearVelocity ?? Vector2.zero;

        // spring force (proportional to distance)
        Vector2 springForce = toTarget * followStrength;

        // damping: reduce oscillation using relative velocity
        //Vector2 dampingForce = -relativeVel * damping;

       // Vector2 total = springForce + dampingForce;

        // Optionally clamp the applied corrective velocity to avoid explosions
       // if (total.magnitude > maxFollowSpeed * rb.mass) total = total.normalized * maxFollowSpeed * rb.mass;

      //  rb.AddForce(total);

        // rotate cart to face movement direction (optional)
        Vector2 lookDir = rb.linearVelocity;
        if (lookDir.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            float z = Mathf.LerpAngle(transform.eulerAngles.z, angle, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(z);
        }
    }

    // Set the player as target and define the offset in player's local space
    public void SetTarget(Transform playerTransform, Vector2 localOffset)
    {
        target = playerTransform;
        targetLocalOffset = localOffset;
        hasTarget = true;

        // ensure cart can collide and has a Rigidbody2D (non-kinematic) for physics interactions
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // optional: allow rotation via MoveRotation or unfreeze if you want physics rotation
        }
    }

    // Detach from target so cart becomes independent
    public void ClearTarget()
    {
        hasTarget = false;
        target = null;
    }
}
