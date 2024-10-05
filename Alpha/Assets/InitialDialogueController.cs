using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InitialDialogueController : MonoBehaviour
{
    public static InitialDialogueController Instance { get; private set; }

    [SerializeField] private GameObject playerSpeakPanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private PlayerController playerMovement;

    private List<string> dialogueLines = new List<string>
    {
        "Ow...my head hurts...\nWhere am I...",
        "Why am I wearing these clothes?",
        "Wasn't I just in the ruins?",
        "It looks just like the scene captured \nin those old investigation photos.",
        "I see some people ahead...perhaps I should ask them."

        // Add more lines as needed
    };

    private int currentLineIndex = 0;
    private bool dialogueActive = false;
    private bool hasPlayedOnce = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (playerSpeakPanel == null)
        {
            playerSpeakPanel = GameObject.Find("PlayerSpeak");
        }

        if (playerSpeakPanel != null)
        {
            playerSpeakPanel.SetActive(false); 
        }
        else
        {
            Debug.LogError("PlayerSpeak panel not found. Make sure it is named 'PlayerSpeak' in the scene.");
        }

        if (!hasPlayedOnce)
        {
            StartDialogue();
        }
    }

    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }

    private void StartDialogue()
    {
        if (hasPlayedOnce) return;

        dialogueActive = true;
        currentLineIndex = 0;
        playerSpeakPanel.SetActive(true);
        DisplayNextLine();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    private void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Count)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueActive = false;
        hasPlayedOnce = true;
        playerSpeakPanel.SetActive(false);

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }
}

