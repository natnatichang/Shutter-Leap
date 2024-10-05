using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [Header("Interaction Panel")]
    private GameObject interactionPanel;
    private Text interactionPanelText;

    private Dictionary<Collider, IInteractable> interactablesInRange = new Dictionary<Collider, IInteractable>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        FindUIElements();
        HideInteractionPanel();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        interactablesInRange.Clear();
        FindUIElements();
        HideInteractionPanel();

        StartCoroutine(RegisterNPCsCoroutine());
    }

    private IEnumerator RegisterNPCsCoroutine()
    {
        yield return null;
        InteractableNPC[] npcs = FindObjectsOfType<InteractableNPC>();
        foreach (InteractableNPC npc in npcs)
        {
            npc.RegisterWithInteractionManager();
        }
    }
    private void FindUIElements()
    {
        Canvas[] canvases = GameObject.FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            interactionPanel = canvas.transform.Find("InteractionPanel")?.gameObject;
            if (interactionPanel != null)
            {
                Debug.Log("InteractionPanel found");
                interactionPanelText = interactionPanel.GetComponentInChildren<Text>();
                if (interactionPanelText == null)
                {
                    Debug.LogError("Text component not found in InteractionPanel!");
                }
                return;
            }
        }

        Debug.LogError("InteractionPanel not found in any canvas in the scene!");
    }


    public void ShowInteractionPanel(string text)
    {
        if (interactionPanel == null || interactionPanelText == null)
        {
            FindUIElements();
        }

        if (interactionPanel != null && interactionPanelText != null)
        {
            interactionPanelText.text = text;
            interactionPanel.SetActive(true);
            Debug.Log($"Showing Interaction Panel: {text}");
        }
        else
        {
            Debug.LogError("Cannot show interaction panel: UI elements are missing.");
        }
    }

    public void HideInteractionPanel()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }
    }

    public void AddInteractable(Collider collider, IInteractable interactable)
    {
        if (Instance != null)
        {
            if (!interactablesInRange.ContainsKey(collider))
            {
                interactablesInRange.Add(collider, interactable);
                UpdateInteractionPanel();
            }
        }
        else
        {
            Debug.LogError("InteractionManager instance is null");
        }
    }

    // Get rid of this 
    public void RemoveInteractable(Collider collider)
    {
        if (interactablesInRange.ContainsKey(collider))
        {
            interactablesInRange.Remove(collider);
            UpdateInteractionPanel();
        }
    }

    //private void UpdateInteractionPanel()
    //{
    //    if (interactablesInRange.Count > 0)
    //    {
    //        ShowInteractionPanel(interactablesInRange.Values.GetEnumerator().Current.GetInteractionPrompt());
    //    }
    //    else
    //    {
    //        HideInteractionPanel();
    //    }
    //}

    private void UpdateInteractionPanel()
    {
        if (interactablesInRange.Count > 0)
        {
            foreach (var interactable in interactablesInRange.Values)
            {
                ShowInteractionPanel(interactable.GetInteractionPrompt());
                break;
            }
        }
        else
        {
            HideInteractionPanel();
        }
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}





