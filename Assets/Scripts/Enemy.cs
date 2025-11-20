using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float strength = 1;
    public Vector2[] walkPath = new Vector2[] {new Vector2(5, 2), new Vector2(7, -1)};
    private int walkIndex;
    private string walkDirection;
    private int walkLength;
    public float walkSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkIndex = 0;
        walkDirection = "forward";
        walkLength = walkPath.Length - 1;
    }

    private void FixedUpdate()
    {
        if (walkDirection == "forward")
        {
            transform.position = Vector2.MoveTowards(transform.position, walkPath[walkIndex + 1], walkSpeed * Time.deltaTime);
            if (new Vector2(transform.position.x, transform.position.y) == walkPath[walkIndex + 1])
            {
                walkIndex++;
                if (walkIndex == walkLength)
                {
                    walkDirection = "backward";
                }
            }
        }
        else if (walkDirection == "backward")
        {
            transform.position = Vector2.MoveTowards(transform.position, walkPath[walkIndex - 1], walkSpeed * Time.deltaTime);
            if (new Vector2(transform.position.x, transform.position.y) == walkPath[walkIndex - 1])
            {
                walkIndex--;
                if (walkIndex == 0)
                {
                    walkDirection = "forward";
                }
            }
        }            
    }
}
