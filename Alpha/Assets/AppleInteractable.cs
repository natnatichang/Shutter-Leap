// Might give errors, recheck this
using UnityEngine;

public class AppleInteractable : MonoBehaviour, IInteractable
{
    private bool canInteract = false;
    private BoxCollider2D appleCollider;

    [SerializeField] private string interactionPrompt = "Press E to pick up the Golden Apple";

    private void Start()
    {
        appleCollider = GetComponent<BoxCollider2D>();
        if (appleCollider == null)
        {
            appleCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        appleCollider.enabled = false;

        // Load the apple collected state
        if (PlayerPrefs.GetInt("AppleCollected", 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    public string GetInteractionPrompt()
    {
        return canInteract ? interactionPrompt : "";
    }

    public void Interact()
    {
        if (canInteract)
        {
            PickUpApple();
        }
    }

    public void EnableInteraction()
    {
        canInteract = true;
        appleCollider.enabled = true;
        Debug.Log("Apple interaction enabled");
    }

    private void PickUpApple()
    {
        PlayerInventory.Instance.AddItem("Apple1");
        InteractionManager.Instance.ShowInteractionPanel("You picked up the Golden Apple!");
        PlayerPrefs.SetInt("AppleCollected", 1);

        gameObject.SetActive(false);
    }

    public void SetInteractionPrompt(string newPrompt)
    {
        interactionPrompt = newPrompt;
    }
}

