using UnityEngine;
using System.Collections;

public class TreeInteractable : MonoBehaviour, IInteractable
{
    public Animator tree1Animator;
    public GameObject apple1Object;
    public float appleAnimationDuration = 1f;
    public AudioClip choppingSound;
    public float animationDelay = 0.5f;

    private bool hasBeenChopped = false;
    private AudioSource audioSource;
    private BoxCollider2D treeCollider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        treeCollider = GetComponent<BoxCollider2D>();
        if (treeCollider == null)
        {
            Debug.LogWarning("BoxCollider2D not found on the tree object!");
        }

        hasBeenChopped = PlayerPrefs.GetInt("TreeChopped", 0) == 1;
        if (hasBeenChopped)
        {
            gameObject.SetActive(false);
            apple1Object.SetActive(false);
        }
    }

    public string GetInteractionPrompt()
    {
        return hasBeenChopped ? "Chopped Tree" : "Press E to chop tree";
    }

    public void Interact()
    {
        if (!hasBeenChopped && PlayerInventory.Instance.HasItem("Axe"))
        {
            StartCoroutine(ChopTreeSequence());
        }
        else if (hasBeenChopped)
        {
            InteractionManager.Instance.ShowInteractionPanel("This tree has already been chopped.");
        }
        else
        {
            InteractionManager.Instance.ShowInteractionPanel("The tree won't budge.");
        }
    }

    private IEnumerator ChopTreeSequence()
    {
        hasBeenChopped = true;

        PlayerPrefs.SetInt("TreeChopped", 1);

        if (choppingSound != null && audioSource != null)
        {
            audioSource.clip = choppingSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Chopping sound or AudioSource is missing!");
        }

        yield return new WaitForSeconds(animationDelay);

        tree1Animator.SetTrigger("Cut");
        yield return new WaitForSeconds(tree1Animator.GetCurrentAnimatorStateInfo(0).length);

        if (treeCollider != null)
        {
            treeCollider.enabled = false;
            Debug.Log("Tree BoxCollider2D disabled");
        }

        Animator appleAnimator = apple1Object.GetComponent<Animator>();
        if (appleAnimator != null)
        {
            appleAnimator.SetTrigger("StartFalling");
            yield return new WaitForSeconds(appleAnimationDuration);
            EnableAppleInteraction();
        }
        else
        {
            Debug.LogWarning("Apple Animator is not found on apple1Object!");
        }
    }

    private void EnableAppleInteraction()
    {
        AppleInteractable appleInteractable = apple1Object.GetComponent<AppleInteractable>();
        if (appleInteractable != null)
        {
            appleInteractable.EnableInteraction();
        }
        else
        {
            Debug.LogWarning("AppleInteractable component not found on apple1Object!");
        }
    }
}
