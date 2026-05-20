using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public string itemName;

    public void Interact()
    {
        Debug.Log("Picked up: " + itemName);
        
        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        return "[E] Pick Up " + itemName;
    }
}