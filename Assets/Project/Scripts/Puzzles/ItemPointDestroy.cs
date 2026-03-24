using UnityEngine;

public class ItemPointDestroy : ItemPoint
{
    public override void OnMouseDown()
    {
        Item item = ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().selectedItem;
        if (item != null)
        {
            UseItemOnPoint(item);
        }
        else
        {
            ManagerUI.Instance.SetHintText("The Object looks flimsy prehaps a tool could help");
        }
    }
    public override void UseItemOnPoint(Item item)
    {
        if (itemNeeded != null && CheckItemNeeded(item))
        {
            RemoveItems();
            Destroy(gameObject);
        }
        else
            ManagerUI.Instance.SetHintText("This item doesnt help. Maybe something sharp could do the trick");
    }
    public void RemoveItems()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }
}
