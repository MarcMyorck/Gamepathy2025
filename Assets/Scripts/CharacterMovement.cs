using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private float dashTimer = 0;
    public float dashCooldown = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (dashTimer < dashCooldown)
        {
            dashTimer = dashTimer + Time.deltaTime;
        }
    }

    public void MoveHorizontal(float dir)
    {
        if (dir < 0f)
        {
            rb.AddForce(Vector2.left * 10);
        }
        else if (dir > 0f)
        {
            rb.AddForce(Vector2.right * 10);
        }
    }

    public void Dash(float dir)
    {
        if (dashTimer >= dashCooldown)
        {
            if (dir < 0f)
            {
                rb.linearVelocityX = 0f;
                rb.AddForce(Vector2.left * 100);
                dashTimer = 0f;
            }
            else if (dir > 0f)
            {
                rb.linearVelocityX = 0f;
                rb.AddForce(Vector2.right * 100);
                dashTimer = 0f;
            }
        }
    }

    public void TryJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * 100);
        }
    }

    private bool IsGrounded()
    {
        float radius = 0.1f;
        Vector2 circleOffset = new Vector2(0f, -0.7f);

        Vector2 pos = (Vector2)transform.position + circleOffset;
        Collider2D c = Physics2D.OverlapCircle(pos, radius);
        // Debug.DrawLine(pos + Vector2.left * radius, pos + Vector2.right * radius, c ? Color.green : Color.red);
        return c != null;
    }
}
