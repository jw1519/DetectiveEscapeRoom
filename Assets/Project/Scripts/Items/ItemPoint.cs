using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    public List<Item> itemNeeded; //the item/items made for this spot
    public List<Item> useableItems; //list of items that can be used here that aren't the correct item
    public GameObject manager;
    public bool isComplete;

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
        // check if Item is needed for the point
        if (itemNeeded != null && CheckItemNeeded(item)) 
        {
            if (item.canBePlaced == false)
            {
                GetComponentInParent<ILock>()?.unlock();
                Inventory.Instance.RemoveItem(item);
                Destroy(gameObject);
                return;
            }
            if (itemNeeded.Count == 1 && transform.childCount == 1)
            {
                Debug.Log("can't place another item");
                return;
            }
            else
            {
                item.isInCorrectPosition = true;
                Inventory.Instance.RemoveItem(item);
                item.PlaceItem(transform);
                HasAllNeededItems();
            }
        }
        //check if Item can be used on this point even if its not needed
        else if (CheckItem(item))
        {
            if (itemNeeded.Count == 1 && transform.childCount == 1)
            {
                Debug.Log("can't place another item");
                return;
            }
            Debug.Log("Item used but not the correct one");
            Inventory.Instance.RemoveItem(item);
            item.PlaceItem(transform);
        }
        else
            Debug.Log("cant use that here");
    }
    // check if item is usable on this point
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
        if (itemNeeded == null) return false;
        foreach (Item item in itemNeeded)
        {
            if (checkItem.itemID == item.itemID)
            {
                return true;
            }
        }
        return false;
    }
    public void HasAllNeededItems()
    {
        if (transform.childCount != itemNeeded.Count || transform.childCount == 0)
        { 
            isComplete = false;
            return;
        }
        Debug.Log(transform.childCount);

        foreach (GameObject child in transform)
        {
            if (!child.GetComponent<WorldItem>()) continue;

            if (!CheckItem(child.GetComponent<WorldItem>().itemSO))
            {
                isComplete = false;
                return;
            }
            
        }
        isComplete = true;
    }
}
