using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public string direction = "right";
    public float softcapSpeed = 10;
    public float moveForce = 10;
    public float jumpForce = 100;
    private int leftExtraJumpAmount = 0;
    public int maxExtraJumpAmount = 1;
    public float dashForce = 1000;
    private float dashTimer = 0;
    public float dashCooldown = 1;
    public CartHandler ch;

    public float currentSpeed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTimer = 0f;
        rb.freezeRotation = true;
        ch = GetComponent<CartHandler>();
    }

    private void FixedUpdate()
    {
        if (dashTimer < dashCooldown)
        {
            dashTimer = dashTimer + Time.deltaTime;
        }

        if (IsGrounded() && (leftExtraJumpAmount < maxExtraJumpAmount))
        {
            leftExtraJumpAmount = maxExtraJumpAmount;
        }

        currentSpeed = rb.linearVelocityX;
    }

    public void MoveHorizontal(float dir)
    {
        if (dir < 0f)
        {
            if (!(rb.linearVelocityX < -softcapSpeed))
            {
                rb.AddForce(Vector2.left * moveForce);
                direction = "left";
            }
        }
        else if (dir > 0f)
        {
            if (!(rb.linearVelocityX > softcapSpeed))
            {
                rb.AddForce(Vector2.right * moveForce);
                direction = "right";
            }
        }
    }

    public void Dash(float dir)
    {
        if (dashTimer >= dashCooldown)
        {
            if (dir < 0f)
            {
                rb.linearVelocityX = 0f;
                rb.AddForce(Vector2.left * dashForce);
                dashTimer = 0f;
                ch.Dash(dir, dashForce);
            }
            else if (dir > 0f)
            {
                rb.linearVelocityX = 0f;
                rb.AddForce(Vector2.right * dashForce);
                dashTimer = 0f;
                ch.Dash(dir, dashForce);
            }
        }
    }

    public void TryJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        else if (leftExtraJumpAmount > 0)
        {
            leftExtraJumpAmount--;
            rb.linearVelocityY = 0f;
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private bool IsGrounded()
    {
        float radius = 0.1f;
        Vector2 circleOffset = new Vector2(0f, -1.1f);

        Vector2 pos = (Vector2)transform.position + circleOffset;
        Collider2D c = Physics2D.OverlapCircle(pos, radius);
         Debug.DrawLine(pos + Vector2.left * radius, pos + Vector2.right * radius, c ? Color.green : Color.red);
        return c != null;
    }
}
