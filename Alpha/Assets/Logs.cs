using UnityEngine;
using UnityEngine.UI;

public class InteractableLog : MonoBehaviour, IInteractable
{
    public Canvas objectinteraction;
    public Text objectText;

    private void Start()
    {
        if (objectinteraction == null)
            objectinteraction = GameObject.Find("ObjectInteraction").GetComponent<Canvas>();

        if (objectText == null)
            objectText = objectinteraction.transform.Find("ObjectPanel/ObjectText").GetComponent<Text>();

        objectinteraction.enabled = false;
    }

    public string GetInteractionPrompt()
    {
        return "A pile of logs for the raft.";
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
        objectinteraction.enabled = true;
    }

    private void HideInteraction()
    {
        objectinteraction.enabled = false;
    }
}
