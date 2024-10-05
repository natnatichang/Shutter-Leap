
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public string interactionPrompt = "Press E to interact";
    public float interactionDistance = 2f;

    private Transform playerTransform;
    private bool playerInRange = false;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }

        RegisterWithInteractionManager();
    }

    public void RegisterWithInteractionManager()
    {
        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.AddInteractable(GetComponent<Collider>(), this);
            Debug.Log($"{gameObject.name}: Registered with InteractionManager");
        }
        else
        {
            Debug.LogError($"{gameObject.name}: InteractionManager not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionManager.Instance.AddInteractable(GetComponent<Collider>(), this);
            Debug.Log($"{gameObject.name}: Player entered trigger zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionManager.Instance.RemoveInteractable(GetComponent<Collider>());
            Debug.Log($"{gameObject.name}: Player exited trigger zone");
        }
    }

    public virtual string GetInteractionPrompt()
    {
        return interactionPrompt;
    }

    public virtual void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
        InteractionManager.Instance.ShowInteractionPanel($"You are interacting with {gameObject.name}");
    }

    private void Update()
    {
        if (playerTransform != null && playerInRange)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log($"E key pressed. Distance: {distance}, Threshold: {interactionDistance}");
                if (distance <= interactionDistance)
                {
                    Debug.Log($"Interaction condition met for {gameObject.name}.");
                    Interact();
                }
                else
                {
                    Debug.Log($"Too far to interact with {gameObject.name}.");
                }
            }
        }
    }
}
