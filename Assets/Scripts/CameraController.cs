using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    public Vector2 offset = new Vector2(0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -10); // Camera follows the player with specified offset position
    }
}
