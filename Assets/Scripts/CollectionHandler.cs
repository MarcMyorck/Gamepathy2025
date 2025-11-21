using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    private int cartAmount;
    private CartHandler ch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cartAmount = 0;
        ch = GetComponent<CartHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Types of Collectibles
        if (collision.gameObject.CompareTag("CartCollectible"))
        {
            Destroy(collision.gameObject);
            cartAmount++;
            ch.PickupCart();
        }
    }
}
