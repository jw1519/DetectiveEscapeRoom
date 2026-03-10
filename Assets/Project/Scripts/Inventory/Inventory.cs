
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    InventoryPanel inventPanel;
    public List<Item> items;
    [HideInInspector] public Item selectedItem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        inventPanel = GetComponent<InventoryPanel>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        inventPanel.AddItem(item);
        Debug.Log("Item Added to Inventory");
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        inventPanel.RemoveItem(item);
        Debug.Log("Item Removed from Inventory");
    }
    public void SelectItem(Item item)
    {
        if (item.itemName == "FlashLight")
        {
            item.UseItem();
        }
        // mae sure item isnt already selected
        if (selectedItem == item)
            return;

        // make sure to deselect anything already selected
        if (selectedItem != null)
        {
            DeselectItem();
        }
        //select item
        inventPanel.SelectItem(item);
        selectedItem = item;
        item.UseItem();
        Debug.Log("Item Selected: " + item.itemName);
    }
    public void DeselectItem()
    {
        inventPanel.DeselectItem(selectedItem);
        selectedItem = null;
    }
}
