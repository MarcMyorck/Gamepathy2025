using TMPro;
using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    public int currentCartAmount;
    public int maxCartAmount;
    private CartHandler ch;
    public TextMeshProUGUI cartsText;
    public AudioSource collectAS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxCartAmount = GameObject.FindGameObjectsWithTag("CartCollectible").Length;
        currentCartAmount = 0;
        ch = GetComponent<CartHandler>();
        cartsText.text = "Carts: " + currentCartAmount + "/" + maxCartAmount;
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
            collectAS.Play();
            Destroy(collision.gameObject);
            currentCartAmount++;
            ch.PickupCart();
            cartsText.text = "Carts: " + currentCartAmount + "/" + maxCartAmount;
        }
    }
}
