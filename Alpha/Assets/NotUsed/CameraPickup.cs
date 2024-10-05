using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraPickup : MonoBehaviour
{   
    // Set the distance for which the player can pick up the camera 
    public float interactionDistance = 0.16f;

    // Reference to the camera sprite in Unity
    private GameObject cameraSprite;

    // Track if the camera has been picked up or not
    private bool hasCameraItem = false;

    public Tilemap switchOff;

    // Awake called when script instance is loaded
    void Awake()
    {
        // Grab the Game Object with the "Camera_Change" string
        cameraSprite = GameObject.Find("Camera_Change");
        if (cameraSprite == null)
        {
            Debug.LogError("Camera sprite 'camera_change' not found in the scene!");
        }
    }

    // Check if the player is able to pick up the camera 
    public void TryPickUpCamera(Vector2 playerPosition)
    {

        //Exist method if camera already picked up or doesn't exist
        if (hasCameraItem || cameraSprite == null) return;

        // Calculate distance between player and camera sprite 
        float distanceToCamera = Vector2.Distance(playerPosition, cameraSprite.transform.position);
        Debug.Log("Distance to camera: " + distanceToCamera);
        // Check if player is within interaction distance 
        if (distanceToCamera <= interactionDistance)
        {   
           
            // Close enough, call method to pick up the camera 
            PickUpCamera();
        }
    }

    // Changes the state of the camera sprite 
    private void PickUpCamera()
    {
        // Change the flags after camera has been picked up
        hasCameraItem = true;
        cameraSprite.SetActive(false);
        Debug.Log("Camera picked up! You can now switch between timelines.");
        switchOff.gameObject.SetActive(false);
    }

    // Returns whether the player has camera or not 
    public bool HasCameraItem()
    {
        return hasCameraItem;
    }
}

