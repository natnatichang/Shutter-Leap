using UnityEngine;

public class InteractionManagerSpawner : MonoBehaviour
{
    public GameObject interactionManagerPrefab;

    private void Awake()
    {
        SpawnInteractionManager();
    }

    private void SpawnInteractionManager()
    {
        if (InteractionManager.Instance == null)
        {
            if (interactionManagerPrefab != null)
            {
                Debug.Log("Spawning InteractionManager");
                Instantiate(interactionManagerPrefab);
            }
            else
            {
                Debug.LogError("InteractionManager prefab is not assigned in the Inspector!");
            }
        }
        else
        {
            Debug.Log("InteractionManager already exists");
        }
    }
}
