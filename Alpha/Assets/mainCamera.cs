using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject cameraUI;
    public Image capturedImageDisplay;
    public float cameraCooldown = 0.5f;
    public Light spotLight;
    public LayerMask puzzlePieceLayer;

    [Header("Camera Settings")]
    public Camera mainCamera;
    public float normalFOV = 60f;
    public float wideFOV = 90f;
    public float transitionSpeed = 2f;

    private bool inCameraMode = false;
    private float lastCaptureTime;
    private float targetFOV;

    void Start()
    {
        spotLight.enabled = false;
        mainCamera.fieldOfView = normalFOV;
        targetFOV = normalFOV;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCameraMode();
        }

        if (inCameraMode && Input.GetMouseButtonDown(1) && Time.time - lastCaptureTime > cameraCooldown)
        {
            CapturePhoto();
        }

        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
    }

    void ToggleCameraMode()
    {
        inCameraMode = !inCameraMode;
        cameraUI.SetActive(inCameraMode);
        spotLight.enabled = inCameraMode;

        targetFOV = inCameraMode ? wideFOV : normalFOV;

        TogglePuzzlePiecesVisibility(inCameraMode);
    }

    void TogglePuzzlePiecesVisibility(bool visible)
    {
        GameObject[] puzzlePieces = GameObject.FindGameObjectsWithTag("PuzzlePiece");
        foreach (GameObject piece in puzzlePieces)
        {
            Renderer renderer = piece.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = visible;
            }
        }
    }

    void CapturePhoto()
    {
        lastCaptureTime = Time.time;
       // StartCoroutine(CaptureScreenshot());
    }
}