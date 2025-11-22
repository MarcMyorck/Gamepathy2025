using TMPro;
using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    public int currentCartAmount;
    public int maxCartAmount;
    private CartHandler ch;
    public TextMeshProUGUI cartsText;
    public int currentEmpathie;
    public int maxEmpathie;
    public TextMeshProUGUI empathieText;
    public AudioSource collectAS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxEmpathie = GameObject.FindGameObjectsWithTag("DialogueTrigger").Length - 1;
        currentEmpathie = 0;
        empathieText.text = "        " + currentEmpathie + "/" + maxEmpathie;

        maxCartAmount = GameObject.FindGameObjectsWithTag("CartCollectible").Length + maxEmpathie;
        currentCartAmount = 0;
        ch = GetComponent<CartHandler>();
        cartsText.text = "        " + currentCartAmount + "/" + maxCartAmount;
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
            PickupCartInitiation();
        }
    }

    public void IncreaseEmpathie()
    {
        currentEmpathie++;
        empathieText.text = "        " + currentEmpathie + "/" + maxEmpathie;
    }

    public void PickupCartInitiation()
    {
        collectAS.Play();
        currentCartAmount++;
        ch.PickupCart();
        cartsText.text = "        " + currentCartAmount + "/" + maxCartAmount;
    }
}
