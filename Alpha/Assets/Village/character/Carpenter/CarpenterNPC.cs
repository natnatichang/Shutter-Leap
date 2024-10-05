using UnityEngine;

public class CarpenterNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject axeSprite; // Reference to the axe sprite in the scene
    private int currentDialogueIndex = 0;
    private string[] initialDialogueLines = {
        "You want my axe?",
        "Sorry, I can't give it to you...",
        "I need to find some blessing herbs first.",
        "They're for the raft I built for the festival.",
        "Without it, I can't proceed with the ceremony...",
        "Let alone lend you anything."
    };

    private string[] afterHerbDialogueLines = {
        "See you at the raft ceremony."
    };

    private bool hasReceivedHerb = false;
    private bool hasGivenAxe = false;

    public string GetInteractionPrompt()
    {
        return "Carpenter";
    }


    private void Awake()
    {
        
        if(axeSprite != null)
        {
            axeSprite.SetActive(true);
        } else
        {
            Debug.LogWarning("Axe sprite not assigned.");
        }
    }
    public void Interact()
    {
        if (!hasReceivedHerb && PlayerInventory.Instance.HasItem("Herb"))
        {
            ReceiveHerb();
        }
        else if (hasReceivedHerb && !hasGivenAxe)
        {
            GiveAxe();
        }
        else
        {
            ShowDialogue();
        }
    }

    private void ReceiveHerb()
    {
        PlayerInventory.Instance.RemoveItem("Herb");
        hasReceivedHerb = true;
        currentDialogueIndex = 0;
        InteractionManager.Instance.ShowInteractionPanel("You found the blessing herbs...");
    }

    private void GiveAxe()
    {
        PlayerInventory.Instance.AddItem("Axe");
        hasGivenAxe = true;
        currentDialogueIndex = 0;
        InteractionManager.Instance.ShowInteractionPanel("Here's my axe, as promised. Be careful with it.");
        HideAxeSprite();
    }

    private void HideAxeSprite()
    {
        if (axeSprite != null)
        {
            axeSprite.SetActive(false);
        }
    }

    private void ShowDialogue()
    {
        string[] currentDialogue = hasReceivedHerb ? afterHerbDialogueLines : initialDialogueLines;

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
            InteractionManager.Instance.HideInteractionPanel();
            currentDialogueIndex = 0; // Reset dialogue index
        }
    }
}
