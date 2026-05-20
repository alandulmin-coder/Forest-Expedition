using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactionDistance = 3f;
    public LayerMask interactLayer;

    [Header("References")]
    public Camera playerCamera;
    public TextMeshProUGUI interactionText;

    private IInteractable currentInteractable;
    private ObjectHighlighter currentHighlighter;

    void Update()
    {
        CheckInteraction();

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable?.Interact();
        }
    }

    void CheckInteraction()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactLayer))
        {
            IInteractable interactable =
                hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;

                interactionText.text =
                    interactable.GetInteractionText();

                interactionText.gameObject.SetActive(true);

                // Highlight
                ObjectHighlighter highlighter =
                    hit.collider.GetComponent<ObjectHighlighter>();

                if (highlighter != currentHighlighter)
                {
                    ClearHighlight();

                    currentHighlighter = highlighter;

                    currentHighlighter?.ShowHighlight();
                }

                return;
            }
        }

        currentInteractable = null;
        interactionText.gameObject.SetActive(false);

        ClearHighlight();
    }

    void ClearHighlight()
    {
        if (currentHighlighter != null)
        {
            currentHighlighter.HideHighlight();
            currentHighlighter = null;
        }
    }
}