using UnityEngine;

public class CartHandler : MonoBehaviour
{
    private CharacterMovement cm;
    private string direction;

    public GameObject cartPrefab;                   // cart prefab with CartFollower2D and Rigidbody2D
    public Transform cartSpawnParent;               // optional parent for spawned carts (scene grouping)

    public float spacing = 1.2f;                    // distance between stacked carts
    public float forwardOffset = 1.2f;              // distance in front of player for the first cart

    // list of spawned carts
    private readonly System.Collections.Generic.List<GameObject> carts = new System.Collections.Generic.List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        direction = cm.direction;
    }

    void FixedUpdate()
    {
        if (direction != cm.direction)
        {
            foreach (GameObject cart in carts)
            {
                cart.GetComponent<CartFollower>().SwitchTargetDirection();
                cart.transform.position = new Vector3(transform.position.x - (cart.transform.position.x - transform.position.x), cart.transform.position.y, cart.transform.position.z);
                cart.GetComponent<SpriteRenderer>().flipX = (direction == "right");
            }
            direction = cm.direction;
        }
    }

    public void PickupCart()
    {
        if (cartPrefab == null) return;

        // Determine offset for the new cart based on count
        int index = carts.Count;
        Vector2 offset = new Vector2(forwardOffset + index * spacing, -0.1f);

        // Spawn cart
        GameObject cart;
        if (direction == "left")
        {
            cart = Instantiate(cartPrefab, transform.position - (Vector3)offset, Quaternion.identity, cartSpawnParent ? cartSpawnParent : null);
        }
        else
        {
            cart = Instantiate(cartPrefab, transform.position + (Vector3)offset, Quaternion.identity, cartSpawnParent ? cartSpawnParent : null);
        }

        // Configure the follower
        CartFollower follower = cart.GetComponent<CartFollower>();
        if (follower != null)
        {
            if (direction == "left")
            {
                follower.SetTarget(transform, -offset);
            }
            else
            {
                follower.SetTarget(transform, offset);
            }
        }

        carts.Add(cart);
    }

    // Optionally: expose method to drop last cart
    public void DropLastCart()
    {
        if (carts.Count == 0) return;
        var last = carts[carts.Count - 1];
        carts.RemoveAt(carts.Count - 1);

        // detach follower so it becomes independent
        var f = last.GetComponent<CartFollower>();
        if (f != null) f.ClearTarget();
    }

    public void Dash(float dir, float dashForce)
    {
        if (dir < 0f)
        {
            foreach (GameObject cart in carts)
            {
                Rigidbody2D temp = cart.GetComponent<Rigidbody2D>();
                temp.linearVelocityX = 0f;
                temp.AddForce(Vector2.left * dashForce);
            }
        }
        else if (dir > 0f)
        {
            foreach (GameObject cart in carts)
            {
                Rigidbody2D temp = cart.GetComponent<Rigidbody2D>();
                temp.linearVelocityX = 0f;
                temp.AddForce(Vector2.right * dashForce);
            }
        }
    }
}
