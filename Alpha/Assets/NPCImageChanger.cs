using UnityEngine;
using UnityEngine.UI; 

public class NPCImageChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] npcSprites;
    [SerializeField] private Image npcImage; 
    [SerializeField] private int npcID; 

    private void Start()
    {
        // Hide the image at the start
        npcImage.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeImage();
            npcImage.enabled = true;
        }
    }

    private void ChangeImage()
    {
        Debug.Log("NPC ID: " + npcID);
        Debug.Log("Number of Sprites: " + npcSprites.Length);

        if (npcID >= 0 && npcID < npcSprites.Length)
        {
            npcImage.sprite = npcSprites[npcID];
        }
        else
        {
            Debug.LogWarning("NPC ID is out of range or not set properly.");
        }
    }

}
