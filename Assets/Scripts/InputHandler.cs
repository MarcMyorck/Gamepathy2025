using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    CharacterMovement cm;
    DialogueHandler dh;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction dashAction;
    InputAction dialogueConfirmAction;
    InputAction dialogueDenyAction;

    string inputMode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cm = GetComponent<CharacterMovement>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Sprint");
        dialogueConfirmAction = InputSystem.actions.FindAction("ConfirmDialogue");
        dialogueDenyAction = InputSystem.actions.FindAction("DenyDialogue");

        inputMode = "gameplay";
    }

    private void Update()
    {
        if (inputMode == "gameplay")
        {
            // Jumping
            if (jumpAction.triggered)
            {
                cm.TryJump();
            }
        }

        if (inputMode == "dialogue")
        {
            // Confirm/Deny Dialogue
            if (dialogueConfirmAction.triggered)
            {
                dh.Confirm();
            }
            else if (dialogueDenyAction.triggered)
            {
                dh.Deny();
            }
        }
    }

    private void FixedUpdate()
    {
        if (inputMode == "gameplay")
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

    public void SwitchInputMode(DialogueHandler tempdh)
    {
        dh = tempdh;
        if (inputMode == "gameplay")
        {
            inputMode = "dialogue";
        }
        else if (inputMode == "dialogue")
        {
            inputMode = "gameplay";
        }
    }
}
