using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    CharacterMovement cm;
    DialogueHandler dh;
    PauseManager pm;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction dashAction;

    InputAction dialogueConfirmAction;
    InputAction dialogueDenyAction;

    InputAction pauseGameAction;

    string inputMode;
    public bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        pm = GetComponent<PauseManager>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Sprint");
        dialogueDenyAction = InputSystem.actions.FindAction("ConfirmDialogue");
        dialogueConfirmAction = InputSystem.actions.FindAction("DenyDialogue");
        pauseGameAction = InputSystem.actions.FindAction("PauseGame");

        inputMode = "gameplay";
        isPaused = false;
    }

    private void Update()
    {
        if (!isPaused)
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

            if (pauseGameAction.triggered)
            {
                isPaused = true;
                pm.Pause(this);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isPaused)
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
