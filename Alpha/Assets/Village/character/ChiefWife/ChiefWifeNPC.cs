using UnityEngine;

public class ChiefWideNPC : InteractableNPC
{
    private int currentDialogueIndex = 0;
    private string[] dialogueLines = {
        "May we have another success festival this year!"
    };

    public override string GetInteractionPrompt()
    {
        return "Chief's Wife";
    }

    public override void Interact()
    {
        if (currentDialogueIndex < dialogueLines.Length)
        {
            InteractionManager.Instance.ShowInteractionPanel(dialogueLines[currentDialogueIndex]);
            currentDialogueIndex++;
        }
        else
        {
            InteractionManager.Instance.HideInteractionPanel();
            currentDialogueIndex = 0; // Reset for next interaction
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show interaction prompt
            InteractionManager.Instance.ShowInteractionPanel(GetInteractionPrompt());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide interaction prompt and reset dialogue
            InteractionManager.Instance.HideInteractionPanel();
            currentDialogueIndex = 0; // Reset dialogue index
        }
    }
}
