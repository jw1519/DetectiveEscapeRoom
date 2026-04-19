using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    public List<Item> itemNeeded; //the item/items made for this spot
    public List<Item> useableItems; //list of items that can be used here that aren't the correct item
    public GameObject manager;
    public bool isComplete;
    public int maxItems;
    public List<Item> items;
    public string hintText = "Can't use that here";

    public virtual void OnMouseDown()
    {
        //Item item = Inventory.Instance.selectedItem;
        Item item = ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().selectedItem;
        if (item != null)
        {
            UseItemOnPoint(item);
        }
        else
            ManagerUI.Instance.SetHintText(hintText);
    }
    public virtual void UseItemOnPoint(Item item)
    {
        // check if Item is needed for the point
        if (itemNeeded != null && CheckItemNeeded(item)) 
        {
            if (item.canBePlaced == false)
            {
                GetComponentInParent<ILock>()?.unlock();
                ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().RemoveItem(item);
                Destroy(gameObject);
                return;
            }
            if (maxItems == 1 && transform.childCount == 1)
            {
                ManagerUI.Instance.SetHintText("Can't place another item here");
                return;
            }
            else
            {
                item.isInCorrectPosition = true;
                ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().RemoveItem(item);
                item.PlaceItem(transform, Vector3.zero);
                HasAllNeededItems();
            }
        }
        //check if Item can be used on this point even if its not needed
        else if (CheckItem(item))
        {
            if (itemNeeded.Count == 1 && transform.childCount == 1)
            {
                ManagerUI.Instance.SetHintText("Can't place another item here");
                return;
            }
            //Inventory.Instance.RemoveItem(item);
            ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().RemoveItem(item);
            items.Add(item);
            item.PlaceItem(transform, Vector3.zero);
        }
        else
            ManagerUI.Instance.SetHintText(hintText);
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

        foreach (Item item in items)
        {
            if (!CheckItem(item))
            {
                isComplete = false;
                return;
            }
        }
        isComplete = true;
    }
}
