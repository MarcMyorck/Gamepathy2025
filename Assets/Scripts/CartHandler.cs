using UnityEngine;

public class CartHandler : MonoBehaviour
{
    public GameObject cartPrefab;                   // cart prefab with CartFollower2D and Rigidbody2D
    public Transform cartSpawnParent;               // optional parent for spawned carts (scene grouping)

    public float spacing = 1.2f;                    // distance between stacked carts
    public float forwardOffset = 1.0f;              // distance in front of player for the first cart

    // list of spawned carts
    private readonly System.Collections.Generic.List<GameObject> carts = new System.Collections.Generic.List<GameObject>();

    public void PickupCart()
    {
        if (cartPrefab == null) return;

        // Determine offset for the new cart based on count
        int index = carts.Count;
        Vector2 offset = new Vector2(forwardOffset + index * spacing, 0f);

        // Spawn cart
        GameObject cart = Instantiate(cartPrefab, transform.position + (Vector3)offset, Quaternion.identity,
                                      cartSpawnParent ? cartSpawnParent : null);
        // Configure the follower
        CartFollower2D follower = cart.GetComponent<CartFollower2D>();
        if (follower != null)
        {
            follower.SetTarget(transform, offset);
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
        var f = last.GetComponent<CartFollower2D>();
        if (f != null) f.ClearTarget();
    }
}
