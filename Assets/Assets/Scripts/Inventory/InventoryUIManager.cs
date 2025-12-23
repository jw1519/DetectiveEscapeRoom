using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject itemContainer;
    public GameObject inventoryContainer;
    public void OpenInventory()
    {
        gameObject.SetActive(true);
    }
    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }
    public void AddItem(Item item)
    {
        if (item == null)
            return;
        itemContainer.GetComponent<ItemUI>().SetItem(item);
        Instantiate(this.itemContainer, inventoryContainer.transform);
    }
    public void RemoveItem(Item item)
    {
        foreach (RectTransform child in inventoryContainer.transform)
        {
            Debug.Log(item.itemName);
            if (child.name == item.itemID)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
    public void SelectItem(Item item)
    {
        Debug.Log("Item Selected in UI: " + item.itemName);
        //change background color of selected item
    }
    public void DeselectItem(Item item)
    {
        Debug.Log("Item Deselected in UI: " + item.itemName);
    }
}
