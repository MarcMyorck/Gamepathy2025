using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth = 3;
    private float invincibilityTimer;
    public float invincibilityCooldown = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        invincibilityTimer = invincibilityCooldown;
    }

    private void FixedUpdate()
    {
        if (invincibilityTimer < invincibilityCooldown)
        {
            invincibilityTimer = invincibilityTimer + Time.deltaTime;
        }
    }

    public void Hurt(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 )
        {
            // Handle Death
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincibilityTimer >= invincibilityCooldown) {
            // fast tag check
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Example: read collision impact
                Vector2 contactPoint = collision.GetContact(0).point;
                Vector2 impactVelocity = collision.relativeVelocity;

                // Do something: damage player, bounce, play sound, etc.
                Hurt(collision.gameObject.GetComponent<Enemy>().strength);
                BounceOff(contactPoint, impactVelocity);
                invincibilityTimer = 0;
            }
        }
    }

    void BounceOff(Vector2 contactPoint, Vector2 impactVelocity)
    {
        // simple bounce example: reflect current velocity
        var rb = GetComponent<Rigidbody2D>();
        Vector2 normal = (Vector2)transform.position - contactPoint;
        normal.Normalize();
        rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, normal) * 50f;
    }
}
