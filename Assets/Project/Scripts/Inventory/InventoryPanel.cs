using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : BasePanel
{
    public GameObject itemContainer;
    public GameObject inventoryContainer;
    public Button closeButton;
    public Button openButton;

    public List<ItemUI> itemUis;
      public Item selectedItem;
    public override void OpenPanel()
    {
        gameObject.SetActive(true);
        openButton.gameObject.SetActive(false);
    }
    public override void ClosePanel()
    {
        gameObject.SetActive(false);
        openButton.gameObject.SetActive(true);
    }
    public void AddItem(Item item)
    {
        if (item == null)
            return;
        itemContainer.GetComponent<ItemUI>().SetItem(item);
        ItemUI itemUI = Instantiate(itemContainer, inventoryContainer.transform).GetComponent<ItemUI>();
        itemUis.Add(itemUI);
    }
    public void RemoveItem(Item item)
    {
        foreach (RectTransform child in inventoryContainer.transform)
        {
            Debug.Log(item.itemName);
            if (child.name == item.itemID)
            {
                itemUis.Remove(child.GetComponent<ItemUI>());
                Destroy(child.gameObject);
                break;
            }
        }
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

        //select item
        selectedItem = item;
        item.UseItem();
    }
    public void DeselectItem()
    {
        selectedItem = null;
    }
}
