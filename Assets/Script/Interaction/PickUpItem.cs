using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public string itemName;

    private PhotoTarget photoTarget;

    private void Awake()
    {
        photoTarget = GetComponent<PhotoTarget>();
    }

    public void Interact()
    {
        if (photoTarget != null &&
            !photoTarget.hasBeenPhotographed)
        {
            Debug.Log("Take a photo first!");

            return;
        }

        Debug.Log("Picked up: " + itemName);

        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        if (photoTarget != null &&
            !photoTarget.hasBeenPhotographed)
        {
            return "[Photo First]";
        }

        return "[E] Pick Up " + itemName;
    }
}