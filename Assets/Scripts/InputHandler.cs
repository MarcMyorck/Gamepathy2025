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

    private void Update()
    {
        // Jumping
        if (jumpAction.triggered)
        {
            cm.TryJump();
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        // Movement
        if (moveAction.IsPressed())
        {
            cm.MoveHorizontal(moveValue.x);
        }

        // Dashing
        if (dashAction.IsPressed())
        {
            cm.Dash(moveValue.x);
        }
    }
}
