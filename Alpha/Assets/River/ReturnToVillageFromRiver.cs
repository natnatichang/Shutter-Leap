using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToVillageFromRiver : MonoBehaviour
{
    public Vector3 villageSpawnPosition = new Vector3(0, 0, 0); // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadVillage();
        }
    }

    private void LoadVillage()
    {
        // Set up the scene transition
        GameManager.SetupSceneTransition(villageSpawnPosition);

        // Load the Village scene
        SceneManager.LoadScene("Village");
    }
}
