using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public string itemName;

    public string objectiveID;

    private GameManager gameManager;

    private PhotoTarget photoTarget;

    void Start()
    {
        photoTarget =
            GetComponent<PhotoTarget>();

        gameManager =
            FindObjectOfType<GameManager>();
    }

    public void Interact()
    {
        if (photoTarget != null &&
            !photoTarget.hasBeenPhotographed)
        {
            Debug.Log("Take a photo first!");
            return;
        }

        gameManager
            .CompleteObjective(objectiveID);

        Debug.Log(
            "Picked up: " +
            itemName
        );

        gameManager.PlaySFX(gameManager.pickupSFX);

        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        if (photoTarget != null &&
            !photoTarget.hasBeenPhotographed)
        {
            return
                "Photo First [Right Click]";
        }

        return
            "[E] Pick Up " + itemName;
    }
}