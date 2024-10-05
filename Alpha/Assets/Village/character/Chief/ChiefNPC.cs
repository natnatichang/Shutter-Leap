using UnityEngine;

public class ChiefNPC : InteractableNPC
{
    [SerializeField] private GameObject appleSprite; // Reference to the apple sprite in the scene
    private int currentDialogueIndex = 0;
    private string[] initialDialogueLines = {
        "Thank you for your contribution to the Hungry Ghost Festival.",
        "Oh...you don't have anything.",
        "You know the rules.",
        "Here in Taiyuan village, everybody must contribute to the offering.",
        "Be quick, bring me something to appease the spirits."
    };

    private string[] afterAppleDialogueLines = {
        "Wow, you managed to find something.",
        "Time to see what the spirits think about it..."
    };

    private bool hasReceivedApple = false;

    public override string GetInteractionPrompt()
    {
        return "Chief";
    }

    private void Awake()
    {
        if (appleSprite != null)
        {
            appleSprite.SetActive(true);
        }
    }

    public override void Interact()
    {
        if (!hasReceivedApple && PlayerInventory.Instance.HasItem("Apple1"))
        {
            ReceiveApple();
            ShowDialogue(); // Immediately show the after-apple dialogue
        }
        else
        {
            ShowDialogue();
        }
    }


    private void ReceiveApple()
    {
        PlayerInventory.Instance.RemoveItem("Apple1");
        hasReceivedApple = true;
        currentDialogueIndex = 0;
        HideAppleSprite();
    }

    private void HideAppleSprite()
    {
        if (appleSprite != null)
        {
            appleSprite.SetActive(false);
        }
    }

    private void ShowDialogue()
    {
        string[] currentDialogue = hasReceivedApple ? afterAppleDialogueLines : initialDialogueLines;

        if (currentDialogueIndex < currentDialogue.Length)
        {
            InteractionManager.Instance.ShowInteractionPanel(currentDialogue[currentDialogueIndex]);
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
