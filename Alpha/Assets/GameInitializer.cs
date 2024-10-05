using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        GameState.ResetState();
    }
}
