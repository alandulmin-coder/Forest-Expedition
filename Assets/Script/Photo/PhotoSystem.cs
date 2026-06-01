using System.Collections;
using UnityEngine;

public class PhotoSystem : MonoBehaviour
{
    [Header("Camera")]
    public Camera playerCamera;

    [Header("Photo Settings")]
    public float normalFOV = 75f;
    public float photoFOV = 40f;
    public float photoDistance = 10f;

    [Header("Effects")]
    public PhotoFade photoFade;
    public CameraFlash cameraFlash;

    private bool isPhotoMode = false;
    private bool isTransitioning = false;
    public GameObject photoModeUI;

    void Update()
    {
        // Toggle Photo Mode
        if (Input.GetMouseButtonDown(1) && !isTransitioning)
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
        if (!isPhotoMode)
        {
            StartCoroutine(EnterPhotoMode());
        }
        else
        {
            StartCoroutine(ExitPhotoMode());
        }
    }

    IEnumerator EnterPhotoMode()
    {
        isTransitioning = true;

        yield return StartCoroutine(photoFade.FadeIn());

        playerCamera.fieldOfView = photoFOV;
        
        photoModeUI.SetActive(true);
        
        isPhotoMode = true;

        yield return StartCoroutine(photoFade.FadeOut());

        isTransitioning = false;
    }

    IEnumerator ExitPhotoMode()
    {
        isTransitioning = true;

        yield return StartCoroutine(photoFade.FadeIn());

        playerCamera.fieldOfView = normalFOV;
        photoModeUI.SetActive(false);
        isPhotoMode = false;

        yield return StartCoroutine(photoFade.FadeOut());

        isTransitioning = false;
    }

    void TakePhoto()
    {
        // Flash effect
        StartCoroutine(cameraFlash.Flash());

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, photoDistance))
        {
            PhotoTarget target =
                hit.collider.GetComponent<PhotoTarget>();

            if (target != null)
            {
                if (!target.hasBeenPhotographed)
                {
                    target.hasBeenPhotographed = true;
                }
                Debug.Log("Photographed: " + target.targetName);

                // Nanti objective bisa ditambahkan di sini
            }
            else
            {
                Debug.Log("Photo Taken");
            }
        }
        else
        {
            Debug.Log("Photo Taken");
        }
    }
}