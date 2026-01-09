using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    public Item itemNeeded; //the item made for this spot
    public List<Item> useableItems; //list of items that can be used here that arnt the correct item
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
        else if (useableItems.Contains(item))
        {
            Debug.Log("Item used but not the correct one");
            Inventory.Instance.RemoveItem(item);
        }
        else
            Debug.Log("cant use that here");
    }
}
