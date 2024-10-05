using UnityEngine;
using System.Collections.Generic;

public class VillageSceneManager : MonoBehaviour
{
    public static VillageSceneManager Instance { get; private set; }

    private Dictionary<string, int> npcDialogueIndices = new Dictionary<string, int>();

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

    public void SaveNPCDialogueIndex(string npcName, int index)
    {
        npcDialogueIndices[npcName] = index;
    }

    public int GetNPCDialogueIndex(string npcName)
    {
        if (npcDialogueIndices.TryGetValue(npcName, out int index))
        {
            return index;
        }
        return 0;
    }
}
