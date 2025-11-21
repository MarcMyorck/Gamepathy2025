using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    private InputHandler ih;

    public int dialogueNumber = 1;
    public bool alreadyActivated = false;
    public Image background;
    public TextMeshProUGUI text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyActivated)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                alreadyActivated = true;
                ih = collision.gameObject.GetComponent<InputHandler>();
                StartDialogue(dialogueNumber);
            }
        }
    }

    private void StartDialogue(int number)
    {
        ih.SwitchInputMode(this);
        switch (number)
        {
            case 1:
                text.enabled = true;
                text.text = "Dialog 1";
                background.enabled = true;
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    public void Confirm()
    {
        // Todo
        EndDialogue();
    }

    public void Deny()
    {
        // Todo
        EndDialogue();
    }

    private void EndDialogue()
    {
        ih.SwitchInputMode(this);
        text.enabled = false;
        text.text = "";
        background.enabled = false;
    }
}
