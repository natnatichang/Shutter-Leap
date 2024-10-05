using UnityEngine;

public class PlayerInventoryReference : MonoBehaviour
{
    private void Start()
    {
        // Ensure the InventoryManager exists
        if (PlayerInventory.Instance == null)
        {
            Debug.LogError("InventoryManager not found! Make sure it's in the first loaded scene.");
        }
    }

    // You can add methods here to interact with the inventory if needed
    public void AddItem(string itemName)
    {
        PlayerInventory.Instance.AddItem(itemName);
    }

    public bool HasItem(string itemName)
    {
        return PlayerInventory.Instance.HasItem(itemName);
    }

    // ... other methods as needed
}
