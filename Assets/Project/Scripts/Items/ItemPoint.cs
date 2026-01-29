using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    public List<Item> itemNeeded; //the item/items made for this spot
    public List<Item> useableItems; //list of items that can be used here that aren't the correct item
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
        Debug.Log(item.itemID);
        if (itemNeeded != null && CheckItemNeeded(item))
        {
            item.isInCorrectPosition = true;
            Inventory.Instance.RemoveItem(item);

            if (item.canBePlaced == false)
            {
                GetComponentInParent<ILock>()?.unlock();
                Destroy(gameObject);
                return;
            }
            item.PlaceItem(transform);
            Debug.Log("Item used");
        }
        else if (CheckItem(item))
        {
            Debug.Log("Item used but not the correct one");
            Inventory.Instance.RemoveItem(item);
            item.PlaceItem(transform);
        }
        else
            Debug.Log("cant use that here");
    }
    // chekc if item is in list
    public bool CheckItem(Item checkItem)
    {
        foreach (Item item in useableItems)
        {
            if (checkItem.itemID == item.itemID)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckItemNeeded(Item checkItem)
    {
        foreach (Item item in itemNeeded)
        {
            if (checkItem.itemID == item.itemID)
            {
                return true;
            }
        }
        return false;
    }
}
