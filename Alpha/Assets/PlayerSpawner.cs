using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && GameManager.ShouldUseCustomSpawn)
        {
            player.transform.position = GameManager.NextSpawnPosition;
            GameManager.ResetSceneTransition();
        }
    }
}
