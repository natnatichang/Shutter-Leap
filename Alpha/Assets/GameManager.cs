using UnityEngine;

// Sets the state of the user
public static class GameManager
{
    public static bool HasAxe
    {
        get { return PlayerPrefs.GetInt("HasAxe", 0) == 1; }
        set { PlayerPrefs.SetInt("HasAxe", value ? 1 : 0); }
    }

    public static Vector3 NextSpawnPosition { get; set; }
    public static bool ShouldUseCustomSpawn { get; set; }

    public static void SaveGameState()
    {
        PlayerPrefs.Save();
    }

    public static void SetupSceneTransition(Vector3 spawnPosition)
    {
        NextSpawnPosition = spawnPosition;
        ShouldUseCustomSpawn = true;
    }

    public static void ResetSceneTransition()
    {
        ShouldUseCustomSpawn = false;
    }
}
