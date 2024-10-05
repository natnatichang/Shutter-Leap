using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
