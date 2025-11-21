using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth = 3;
    private float invincibilityTimer;
    public float invincibilityCooldown = 1;
    public float damageKnockback = 10;
    private Rigidbody2D rb;
    public TextMeshProUGUI livesText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        invincibilityTimer = invincibilityCooldown;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (invincibilityTimer < invincibilityCooldown)
        {
            invincibilityTimer = invincibilityTimer + Time.deltaTime;
        }
        livesText.text = "Lives: " + currentHealth;
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
                // Do something: damage player, bounce, play sound, etc.
                Hurt(collision.gameObject.GetComponent<Enemy>().strength);
                invincibilityTimer = 0;

                // Knockback
                Vector2 incoming = collision.relativeVelocity;
                incoming.Normalize();
                rb.linearVelocity = incoming * damageKnockback;
            }
        }
    }
}
