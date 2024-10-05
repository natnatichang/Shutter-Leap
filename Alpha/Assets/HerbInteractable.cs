using UnityEngine;
using System.Collections.Generic;

public class HerbInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string herbName = "Herb";
    [SerializeField] private string uniqueHerbId;

    private static HashSet<string> collectedHerbs = new HashSet<string>();

    private void Awake()
    {
        if (string.IsNullOrEmpty(uniqueHerbId))
        {
            uniqueHerbId = System.Guid.NewGuid().ToString();
        }

        if (IsCollected())
        {
            Destroy(gameObject);
        }
    }

    public string GetInteractionPrompt()
    {
        return $"Press E to pick up the {herbName}";
    }

    public void Interact()
    {
        if (!IsCollected())
        {
            CollectHerb();
        }
    }

    private void CollectHerb()
    {
        Debug.Log($"Player picked up the {herbName}");
        PlayerInventory.Instance.AddItem(herbName);
        collectedHerbs.Add(uniqueHerbId); 
        Destroy(gameObject);
    }

    private bool IsCollected()
    {
        return collectedHerbs.Contains(uniqueHerbId);
    }

    public static void ResetAllHerbs()
    {
        collectedHerbs.Clear();
    }
}
