using UnityEngine;

public class QuestItem : MonoBehaviour, Interactable
{
    public string itemName;

    public void Interact()
    {
        Inventory.Instance.AddItem(itemName);
        Destroy(gameObject);
    }
}