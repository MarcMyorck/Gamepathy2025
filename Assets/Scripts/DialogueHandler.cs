using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    private InputHandler ih;
    public CartHandler cartH;
    public CollectionHandler collectionH;

    public int dialogueNumber = 1;
    public bool alreadyActivated = false;
    public Image background;
    public TextMeshProUGUI text;
    public TextMeshProUGUI actions;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyActivated)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                alreadyActivated = true;
                ih = collision.gameObject.GetComponent<InputHandler>();
                StartDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        ih.SwitchInputMode(this);
        switch (dialogueNumber)
        {
            case 0:
                text.enabled = true;
                text.text = "Bring' meine Wägen um jeden Preis zurück!";
                actions.enabled = true;
                background.enabled = true;
                break;
            case 1:
                text.enabled = true;
                text.text = "Bitte lass mir meinen Wagen!";
                actions.enabled = true;
                background.enabled = true;
                break;
            case 2:
                text.enabled = true;
                text.text = "Meine Familie braucht diesen Wagen!";
                actions.enabled = true;
                background.enabled = true;
                break;
            case 3:
                text.enabled = true;
                text.text = "Ich habe nur diesen Wagen!";
                actions.enabled = true;
                background.enabled = true;
                break;
            case 4:
                text.enabled = true;
                text.text = "Warum passiert mir immer sowas?";
                actions.enabled = true;
                background.enabled = true;
                break;
            case 5:
                text.enabled = true;
                text.text = "Ich flehe dich an!";
                actions.enabled = true;
                background.enabled = true;
                break;
            case 6:
                text.enabled = true;
                text.text = "Alles, nur nicht den Wagen!";
                actions.enabled = true;
                background.enabled = true;
                break;
        }
    }

    public void Confirm()
    {
        if (dialogueNumber != 0)
        {
            collectionH.PickupCartInitiation();
        }
        EndDialogue();
    }

    public void Deny()
    {
        if (dialogueNumber != 0)
        {
            collectionH.IncreaseEmpathie();
        }
        EndDialogue();
    }

    private void EndDialogue()
    {
        ih.SwitchInputMode(this);
        text.enabled = false;
        text.text = "";
        actions.enabled = false;
        background.enabled = false;
    }
}
