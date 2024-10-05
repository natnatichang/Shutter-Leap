using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Animation strings to call 
    public struct PlayerAnimationStrings
    {
        public string moveX => "moveX";
        public string moveY => "moveY";
        public string isMoving => "IsMoving";
        public string walk => "Present_Player_Walk";
        public string idle => "Present_Player_Idle";
    }

    // Movement and collision variables 
    public float moveSpeed = 5f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    // References to the input and components for it
    public Vector2 movementInput { get; private set; }
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerAnimationStrings animationStrings;

    // Detect the collisions
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // UI Panel for proximity
    private GameObject proximityPanel;
    private GameObject interactionPanel;

    private bool playerInRange = false;
    private IInteractable currentInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animationStrings = new PlayerAnimationStrings();

        // Find the proximity panel in the scene
        proximityPanel = GameObject.Find("ProximityPanel");

        if (proximityPanel != null)
        {
            proximityPanel.SetActive(false); // Ensure the panel is hidden at start
        }
        else
        {
            Debug.LogError("Proximity panel not found. Make sure it is named 'ProximityPanel' in the scene.");
        }

        // Find the interaction panel in the scene
        interactionPanel = GameObject.Find("InteractionPanel");

        // If not found at root level, search in all canvases
        if (interactionPanel == null)
        {
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                interactionPanel = canvas.transform.Find("InteractionPanel")?.gameObject;
                if (interactionPanel != null)
                    break;
            }
        }

        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false); // Ensure the panel is hidden at start
        }
        else
        {
            Debug.LogError("Interaction panel not found. Make sure it is named 'InteractionPanel' in the scene or as a child of a Canvas.");
        }
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            UpdateAnimator(success);
        }
        else
        {
            UpdateAnimator(false);
        }

        UpdateAnimation();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void UpdateAnimator(bool isMoving)
    {
        animator.SetBool(animationStrings.isMoving, isMoving);
        animator.SetFloat(animationStrings.moveX, movementInput.x);
        animator.SetFloat(animationStrings.moveY, movementInput.y);
    }

    void UpdateAnimation()
    {
        if (movementInput != Vector2.zero)
        {
            animator.Play(animationStrings.walk);
        }
        else
        {
            animator.Play(animationStrings.idle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (IsInVillageScene())
            {
                HandleVillageInteraction(collision);
            }
            else if (IsInRiverScene())
            {
                HandleRiverInteraction(collision);
            }
            else if (IsInForestScene())
            {
                HandleForestInteraction(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (IsInVillageScene())
            {
                HandleVillageExitInteraction(collision);
            }
            else if (IsInRiverScene())
            {
                HandleRiverExitInteraction(collision);
            }
            else if (IsInForestScene())
            {
                HandleForestExitInteraction(collision);
            }
        }
    }


    private void HandleVillageInteraction(Collider2D collision)
    {
        Debug.Log("Player entered collision with: " + collision.name + " in Village scene");
        playerInRange = true;
        currentInteractable = collision.GetComponent<IInteractable>();

        if (proximityPanel != null)
        {
            Debug.Log("Activating proximity panel.");
            proximityPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Proximity panel reference is null.");
        }
    }

    private void HandleRiverInteraction(Collider2D collision)
    {
        Debug.Log("Player entered collision with: " + collision.name + " in River scene");
        playerInRange = true;
        currentInteractable = collision.GetComponent<IInteractable>();

        if (proximityPanel != null)
        {
            Debug.Log("Activating proximity panel.");
            proximityPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Proximity panel reference is null.");
        }
    }

    private void HandleForestInteraction(Collider2D collision)
    {
        Debug.Log("Player entered collision with: " + collision.name + " in Forest scene");
        playerInRange = true;
        currentInteractable = collision.GetComponent<IInteractable>();

        if (proximityPanel != null)
        {
            Debug.Log("Activating proximity panel.");
            proximityPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Proximity panel reference is null.");
        }
    }

    private void HandleVillageExitInteraction(Collider2D collision)
    {
        Debug.Log("Player exited collision with: " + collision.name + " in Village scene");
        HandleExitInteraction();
    }

    private void HandleRiverExitInteraction(Collider2D collision)
    {
        Debug.Log("Player exited collision with: " + collision.name + " in River scene");
        HandleExitInteraction();
    }

    private void HandleForestExitInteraction(Collider2D collision)
    {
        Debug.Log("Player exited collision with: " + collision.name + " in Forest scene");
        HandleExitInteraction();
    }

    private void HandleExitInteraction()
    {
        playerInRange = false;
        currentInteractable = null;
        if (proximityPanel != null)
        {
            proximityPanel.SetActive(false);
        }
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }
    }

    private bool IsInVillageScene()
    {
        return SceneManager.GetActiveScene().name == "Village";
    }

    private bool IsInRiverScene()
    {
        return SceneManager.GetActiveScene().name == "River";
    }

    private bool IsInForestScene()
    {
        return SceneManager.GetActiveScene().name == "Forest";
    }
}