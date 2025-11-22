using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishHandler : MonoBehaviour
{
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (true) // Check for points?
        {
            SessionData.cartsCollected = player.GetComponent<CollectionHandler>().currentCartAmount;
            SessionData.cartsTotal = player.GetComponent<CollectionHandler>().maxCartAmount;
            SessionData.empathieCollected = player.GetComponent<CollectionHandler>().currentEmpathie;
            SessionData.empathieTotal = player.GetComponent<CollectionHandler>().maxEmpathie;
            SceneManager.LoadScene("GameFinish");
        }
    }
}
