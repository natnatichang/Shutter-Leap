//using UnityEngine;

//public class PuzzlePiecePickup : MonoBehaviour
//{
//    // The distance within which the player can pick up a puzzle piece
//    public float interactionDistance = 0.5f;

//    // Array to hold references to the puzzle pieces
//    public GameObject[] puzzlePieces;

//    // Array to track which puzzle pieces have been picked up
//    private bool[] hasPuzzlePiece;

//    // Reference to the player
//    private Transform player;

//    void Start()
//    {
//        // Initialize the puzzle piece tracking array
//        hasPuzzlePiece = new bool[puzzlePieces.Length];

//        // Find the player in the scene
//        player = GameObject.FindWithTag("Player").transform;

//        if (player == null)
//        {
//            Debug.LogError("Player not found in the scene!");
//        }

//        for (int i = 0; i < puzzlePieces.Length; i++)
//        {
//            if (puzzlePieces[i] == null)
//            {
//                Debug.LogError($"Puzzle piece {i} not assigned in the inspector!");
//            }
//        }
//    }

//    void Update()
//    {
//        // Check for puzzle piece pickup in each frame
//        for (int i = 0; i < puzzlePieces.Length; i++)
//        {
//            if (!hasPuzzlePiece[i] && puzzlePieces[i] != null)
//            {
//                TryPickUpPuzzlePiece(i);
//            }
//        }
//    }

//    // Check if the player can pick up a specific puzzle piece
//    private void TryPickUpPuzzlePiece(int index)
//    {
//        // Calculate distance between player and the puzzle piece
//        float distanceToPiece = Vector2.Distance(player.position, puzzlePieces[index].transform.position);

//        // Check if player is within interaction distance
//        if (distanceToPiece <= interactionDistance)
//        {
//            // Check if the E key is pressed
//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                // Pick up the puzzle piece
//                PickUpPuzzlePiece(index);
//            }
//        }
//    }

//    // Handle the logic for picking up a puzzle piece
//    private void PickUpPuzzlePiece(int index)
//    {
//        hasPuzzlePiece[index] = true;
//        puzzlePieces[index].SetActive(false);
//        Debug.Log($"Puzzle piece {index} picked up!");
//    }

//    // Check if all puzzle pieces have been collected
//    public bool AllPiecesCollected()
//    {
//        foreach (bool pieceCollected in hasPuzzlePiece)
//        {
//            if (!pieceCollected)
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//}
using UnityEngine;

public class PuzzlePiecePickup : MonoBehaviour
{
    public float interactionDistance = 0.5f;
    public GameObject[] puzzlePieces;
    private bool[] hasPuzzlePiece;
    private Transform player;

    // Reference to the UI element to activate
    public GameObject puzzleCompleteUI;

    // Reference to the panel named "Panel" to hide
    public GameObject panel;

    private bool isSoundMuted = false;

    void Start()
    {
        hasPuzzlePiece = new bool[puzzlePieces.Length];
        player = GameObject.FindWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found in the scene!");
        }

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (puzzlePieces[i] == null)
            {
                Debug.LogError($"Puzzle piece {i} not assigned in the inspector!");
            }
        }

        // Ensure the UI is initially inactive
        if (puzzleCompleteUI != null)
        {
            puzzleCompleteUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Puzzle complete UI not assigned in the inspector!");
        }
    }

    void Update()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (!hasPuzzlePiece[i] && puzzlePieces[i] != null)
            {
                TryPickUpPuzzlePiece(i);
            }
        }

        // Check if all pieces are collected and activate the UI
        if (AllPiecesCollected() && puzzleCompleteUI != null)
        {
            puzzleCompleteUI.SetActive(true);

            // Mute all sounds and hide the panel after the UI is activated
            if (!isSoundMuted)
            {
                MuteAllSounds();
                HidePanel();
                isSoundMuted = true;
            }
        }
    }

    private void TryPickUpPuzzlePiece(int index)
    {
        float distanceToPiece = Vector2.Distance(player.position, puzzlePieces[index].transform.position);

        if (distanceToPiece <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpPuzzlePiece(index);
            }
        }
    }

    private void PickUpPuzzlePiece(int index)
    {
        hasPuzzlePiece[index] = true;
        puzzlePieces[index].SetActive(false);
        Debug.Log($"Puzzle piece {index} picked up!");
    }

    public bool AllPiecesCollected()
    {
        foreach (bool pieceCollected in hasPuzzlePiece)
        {
            if (!pieceCollected)
            {
                return false;
            }
        }
        return true;
    }

    private void MuteAllSounds()
    {
        AudioListener.volume = 0f;
        Debug.Log("All sounds have been muted.");
    }

    private void HidePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }

        Debug.Log("Panel has been hidden.");
    }
}
