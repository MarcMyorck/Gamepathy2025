using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    CharacterMovement cm;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction dashAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cm = GetComponent<CharacterMovement>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Sprint");
    }

    private void FixedUpdate()
    {
        // Movement
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        if (moveValue.x != 0)
        {
            if (moveValue.x < 0)
            {
                cm.MoveHorizontal("left");
            }
            else if (moveValue.x > 0)
            {
                cm.MoveHorizontal("right");
            }
        }

        // Jumping
        if (jumpAction.IsPressed())
        {
            cm.TryJump();
        }

        // Dashing
        if (dashAction.IsPressed())
        {
            if (moveValue.x != 0)
            {
                if (moveValue.x < 0)
                {
                    cm.Dash("left");
                }
                else if (moveValue.x > 0)
                {
                    cm.Dash("right");
                }
            }
        }
    }
}
