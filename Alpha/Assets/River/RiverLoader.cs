using UnityEngine;
using UnityEngine.SceneManagement;

public class RiverLoader : MonoBehaviour
{
    public Vector3 riverSpawnPosition = new Vector3(0, 0, 0); // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadRiver();
        }
    }

    private void LoadRiver()
    {

        // Set up the scene transition
        GameManager.SetupSceneTransition(riverSpawnPosition);

        // Load the Forest scene
        SceneManager.LoadScene("River");
    }
}
