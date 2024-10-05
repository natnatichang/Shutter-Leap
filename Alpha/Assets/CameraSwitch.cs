using UnityEngine;

public class SimpleCameraSwitch : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the Main Camera
    public Camera fullCamera;  // Reference to the Full Camera
    public CameraPickup cameraPickup; // Reference to the CameraPickup script

    void Start()
    {
        // Start with the main camera active
        mainCamera.enabled = true;
        fullCamera.enabled = false;
    }

    void Update()
    {
        // Switch cameras when the right mouse button is clicked, only if the camera is picked up
        if (cameraPickup != null && cameraPickup.HasCameraItem() && Input.GetMouseButtonDown(1))
        {
            ToggleCameras();
        }
    }

    void ToggleCameras()
    {
        // Toggle between the main camera and full camera
        mainCamera.enabled = !mainCamera.enabled;
        fullCamera.enabled = !fullCamera.enabled;
    }
}
