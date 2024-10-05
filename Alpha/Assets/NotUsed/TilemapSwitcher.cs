//using UnityEngine;
//using UnityEngine.Tilemaps;
//using UnityEngine.InputSystem;

//public class TilemapSwitcher : MonoBehaviour
//{
//    // References to tilemaps for present and past
//    public Tilemap presentTilemap;
//    public Tilemap pastTilemap;
//    public Tilemap presentTopPieces;
//    public Tilemap pastTopPieces;

//    // Track which timeline is active 
//    private bool isPresentActive = true;

//    // Reference to the CameraPickup script 
//    private CameraPickup cameraPickup;

//    // Input action for switching tilemaps and interacting 
//    private InputAction switchAction;
//    private InputAction interactAction;

//    // Audio source for the camera shutter sound
//    public AudioSource shutterAudio;

//    public GhostMovement ghostMovement;

//    // Track if the player has switched timelines
//    private bool hasSwitchedOnce = false;

//    // Awake called when script instance is loaded
//    void Awake()
//    {
//        // Find CameraPickup script in the scene
//        cameraPickup = FindObjectOfType<CameraPickup>();
//        if (cameraPickup == null)
//        {
//            Debug.LogError("CameraPickup script not found in the scene!");
//        }

//        // Check to make sure all the tilemaps are there and already set to the state
//        if (presentTilemap != null && pastTilemap != null && presentTopPieces != null && pastTopPieces != null)
//        {
//            SetTilemapState(true);
//        }

//        // Set input action for switching tilemaps
//        switchAction = new InputAction("SwitchTilemap", InputActionType.Button);
//        switchAction.AddBinding("<Mouse>/rightButton");
//        switchAction.performed += ctx => TrySwitchTilemap();
//        switchAction.Enable();

//        // Set up input action for interaction with camera 
//        interactAction = new InputAction("Interact", InputActionType.Button);
//        interactAction.AddBinding("<Keyboard>/e");
//        interactAction.performed += ctx => TryPickUpCamera();
//        interactAction.Enable();
//    }

//    // Attempt to switch tilemaps if player has the camera
//    void TrySwitchTilemap()
//    {
//        // If player picked up the camera and hasn't switched yet
//        if (cameraPickup != null && cameraPickup.HasCameraItem())
//        {
//            if (!hasSwitchedOnce)
//            {
//                SwitchTilemap();
//                hasSwitchedOnce = true; // Mark that the player has switched at least once
//            }
//            else
//            {
//                Debug.Log("You need to capture the ghost before switching timelines again!");
//            }
//        }
//        else
//        {
//            Debug.Log("You need to pick up the camera first!");
//        }
//    }

//    // Switch between present + past tilemaps
//    void SwitchTilemap()
//    {
//        // Check before proceeding 
//        if (presentTilemap == null || pastTilemap == null || presentTopPieces == null || pastTopPieces == null)
//        {
//            return;
//        }

//        // Change the states 
//        isPresentActive = !isPresentActive;
//        SetTilemapState(isPresentActive);

//        // Start ghost movement after switching
//        ghostMovement.StartMovement();

//        // Play the shutter sound 
//        if (shutterAudio != null)
//        {
//            shutterAudio.PlayOneShot(shutterAudio.clip);
//        }

//        // Personal check for states 
//        Debug.Log("Switched to " + (isPresentActive ? "Present" : "Past") + " timeline");
//    }

//    // Set active state of the tilemap based on what the current timeline is
//    void SetTilemapState(bool isPresent)
//    {
//        presentTilemap.gameObject.SetActive(isPresent);
//        presentTopPieces.gameObject.SetActive(isPresent);
//        pastTilemap.gameObject.SetActive(!isPresent);
//        pastTopPieces.gameObject.SetActive(!isPresent);
//    }

//    // Check if present timeline is active 
//    public bool IsPresentActive()
//    {
//        return isPresentActive;
//    }

//    // Attempt to pick up the camera 
//    void TryPickUpCamera()
//    {
//        if (cameraPickup != null)
//        {
//            Vector2 playerPosition = transform.position;
//            cameraPickup.TryPickUpCamera(playerPosition);
//        }
//    }

//    // Method to call when the ghost is captured
//    public void OnGhostCaptured()
//    {
//        hasSwitchedOnce = false; // Reset the switch flag after capturing the ghost
//        Debug.Log("Ghost has been captured! You can now switch timelines again.");
//    }

//    // Disable the input actions when the script is disabled 
//    void OnDisable()
//    {
//        switchAction.Disable();
//        interactAction.Disable();
//    }
//}


using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class TilemapSwitcher : MonoBehaviour
{
    // References to tilemaps for present and past
    public Tilemap presentTilemap;
    public Tilemap pastTilemap;
    public Tilemap presentTopPieces;
    public Tilemap pastTopPieces;

    // Track which timeline is active 
    private bool isPresentActive = true;

    // Reference to the CameraPickup script 
    private CameraPickup cameraPickup;

    // Input action for switching tilemaps and interacting 
    private InputAction switchAction;
    private InputAction interactAction;

    // Audio source for the camera shutter sound
    public AudioSource shutterAudio;

    // Track if the player has switched timelines
    // private bool hasSwitchedOnce = false;

    // Awake called when script instance is loaded
    void Awake()
    {
        // Find CameraPickup script in the scene
        cameraPickup = FindObjectOfType<CameraPickup>();
        if (cameraPickup == null)
        {
            Debug.LogError("CameraPickup script not found in the scene!");
        }

        // Check to make sure all the tilemaps are there and already set to the state
        if (presentTilemap != null && pastTilemap != null && presentTopPieces != null && pastTopPieces != null)
        {
            SetTilemapState(true);
        }

        // Set input action for switching tilemaps
        switchAction = new InputAction("SwitchTilemap", InputActionType.Button);
        switchAction.AddBinding("<Mouse>/rightButton");
        switchAction.performed += ctx => TrySwitchTilemap();
        switchAction.Enable();

        // Set up input action for interaction with camera 
        interactAction = new InputAction("Interact", InputActionType.Button);
        interactAction.AddBinding("<Keyboard>/e");
        interactAction.performed += ctx => TryPickUpCamera();
        interactAction.Enable();
    }

    // Attempt to switch tilemaps if player has the camera
    void TrySwitchTilemap()
    {
        // If player picked up the camera and hasn't switched yet
        if (cameraPickup != null && cameraPickup.HasCameraItem())
        {

            SwitchTilemap();
            //if (!hasSwitchedOnce)
            //{
            //    SwitchTilemap();
            //    hasSwitchedOnce = true; // Mark that the player has switched at least once
            //}
            //else
            //{
            //    Debug.Log("You need to capture the ghost before switching timelines again!");
            //}
        }
        else
        {
            Debug.Log("You need to pick up the camera first!");
        }
    }

    // Switch between present + past tilemaps
    void SwitchTilemap()
    {
        // Check before proceeding 
        if (presentTilemap == null || pastTilemap == null || presentTopPieces == null || pastTopPieces == null)
        {
            return;
        }

        // Change the states 
        isPresentActive = !isPresentActive;
        SetTilemapState(isPresentActive);

        // Play the shutter sound 
        if (shutterAudio != null)
        {
            shutterAudio.PlayOneShot(shutterAudio.clip);
        }

        // Personal check for states 
        Debug.Log("Switched to " + (isPresentActive ? "Present" : "Past") + " timeline");
    }

    // Set active state of the tilemap based on what the current timeline is
    void SetTilemapState(bool isPresent)
    {
        presentTilemap.gameObject.SetActive(isPresent);
        presentTopPieces.gameObject.SetActive(isPresent);
        pastTilemap.gameObject.SetActive(!isPresent);
        pastTopPieces.gameObject.SetActive(!isPresent);
    }

    // Check if present timeline is active 
    public bool IsPresentActive()
    {
        return isPresentActive;
    }

    // Attempt to pick up the camera 
    void TryPickUpCamera()
    {
        if (cameraPickup != null)
        {
            Vector2 playerPosition = transform.position;
            cameraPickup.TryPickUpCamera(playerPosition);
        }
    }

    // Disable the input actions when the script is disabled 
    void OnDisable()
    {
        switchAction.Disable();
        interactAction.Disable();
    }
}
