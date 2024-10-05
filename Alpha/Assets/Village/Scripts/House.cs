using UnityEngine;
using UnityEngine.UI; // Make sure to include this namespace

public class InteractableHouse : MonoBehaviour, IInteractable
{
    public Canvas objectinteraction; // Reference to the Canvas
    public Text objectText; // Reference to the Text component

    private void Start()
    {
        // Optionally, find the objects if not set in the Inspector
        if (objectinteraction == null)
            objectinteraction = GameObject.Find("ObjectInteraction").GetComponent<Canvas>();

        if (objectText == null)
            objectText = objectinteraction.transform.Find("ObjectPanel/ObjectText").GetComponent<Text>();

        // Hide the Canvas initially
        objectinteraction.enabled = false;
    }

    public string GetInteractionPrompt()
    {
        return "An old dingy house";
    }

    public void Interact()
    {
        ShowInteraction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger with: " + other.name);
        if (other.CompareTag("Player"))
        {
            ShowInteraction();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited trigger with: " + other.name);
        if (other.CompareTag("Player"))
        {
            HideInteraction();
        }
    }

    private void ShowInteraction()
    {
        objectText.text = GetInteractionPrompt();
        objectinteraction.enabled = true; // Show the Canvas
    }

    private void HideInteraction()
    {
        objectinteraction.enabled = false; // Hide the Canvas
    }
}
