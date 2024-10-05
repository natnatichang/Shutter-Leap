using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestLoader : MonoBehaviour
{
    public Vector3 forestSpawnPosition = new Vector3(0, 0, 0); // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadForest();
        }
    }

    private void LoadForest()
    {

        // Set up the scene transition
        GameManager.SetupSceneTransition(forestSpawnPosition);

        // Load the Forest scene
        SceneManager.LoadScene("Forest");
    }
}
