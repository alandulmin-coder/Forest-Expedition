using UnityEngine;

public class PhotoSystem : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Photo Settings")]
    public float normalFOV = 75f;
    public float photoFOV = 40f;

    private bool isPhotoMode;

    void Update()
    {
        // Toggle Photo Mode
        if (Input.GetMouseButtonDown(1))
        {
            TogglePhotoMode();
        }

        // Take Photo
        if (isPhotoMode && Input.GetMouseButtonDown(0))
        {
            TakePhoto();
        }
    }

    void TogglePhotoMode()
    {
        isPhotoMode = !isPhotoMode;

        if (isPhotoMode)
        {
            playerCamera.fieldOfView = photoFOV;

            Debug.Log("Entered Photo Mode");
        }
        else
        {
            playerCamera.fieldOfView = normalFOV;

            Debug.Log("Exited Photo Mode");
        }
    }

    void TakePhoto()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            PhotoTarget target =
                hit.collider.GetComponent<PhotoTarget>();

            if (target != null)
            {
                Debug.Log(
                    "Photographed: " + target.targetName
                );
            }
            else
            {
                Debug.Log("Photo Taken");
            }
        }
    }
}