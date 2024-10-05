using UnityEngine;

public class GoldenApple : MonoBehaviour
{
    private Animator appleAnimator;
    private bool hasLanded = false;
    private Collider2D appleCollider;

    private void Start()
    {
        appleAnimator = GetComponent<Animator>();
        appleCollider = GetComponent<Collider2D>();

        if (appleAnimator == null)
        {
            Debug.LogError("Animator component not found on Apple!");
        }
        else
        {
            Debug.Log("Apple1 animator found and initialized");
        }

        // Disable the collider initially
        if (appleCollider != null)
        {
            appleCollider.enabled = false;
        }
        else
        {
            Debug.LogError("Collider2D component not found on Apple!");
        }

        // Ensure the apple starts in its default state
        if (appleAnimator != null)
        {
            appleAnimator.Play("Apple_Idle", 0, 0f);
        }
    }

    public void StartFalling()
    {
        if (appleAnimator != null)
        {
            appleAnimator.Play("Apple_Fall");
            Debug.Log("Apple falling animation triggered");
        }
    }

    // This method will be called by an Animation Event at the end of the falling animation
    public void OnFallComplete()
    {
        hasLanded = true;

        // Enable the collider so the player can collect the apple
        if (appleCollider != null)
        {
            appleCollider.enabled = true;
            Debug.Log("Apple has landed and collider is now enabled");
        }
        else
        {
            Debug.LogError("Collider2D is missing on the Apple!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && hasLanded)
        {
            Debug.Log("Golden Apple collected!");
            // Add to inventory or increase score here
            Destroy(gameObject);
        }
    }
}
