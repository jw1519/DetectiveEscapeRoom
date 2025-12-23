using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    public Item itemNeeded;
    public GameObject manager;

    private void OnMouseDown()
    {
        Item item = Inventory.Instance.selectedItem;
        if (item != null)
        {
            UseItemOnPoint(item);
        }
    }
    public virtual void UseItemOnPoint(Item item)
    {
        if (item.itemID == itemNeeded.itemID)
        {
            item.isInCorrectPosition = true;
            Inventory.Instance.RemoveItem(item);
            Debug.Log("Item used");
        }
        else
            Debug.Log("cant use that here");
    }
}
