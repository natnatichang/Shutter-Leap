using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    private void Awake()
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

    public void AddItem(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName]++;
        }
        else
        {
            inventory[itemName] = 1;
        }
        Debug.Log($"Added {itemName} to inventory. Total: {inventory[itemName]}");
    }

    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName) && inventory[itemName] > 0;
    }

    public void RemoveItem(string itemName)
    {
        if (inventory.ContainsKey(itemName) && inventory[itemName] > 0)
        {
            inventory[itemName]--;
            if (inventory[itemName] == 0)
            {
                inventory.Remove(itemName);
            }
            Debug.Log($"Removed {itemName} from inventory.");
        }
    }
}
