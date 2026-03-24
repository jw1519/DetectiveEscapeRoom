using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item itemSO;
    string id;

    private void Awake()
    {
        id = itemSO.itemID;
        itemSO = Instantiate(itemSO);
        itemSO.itemID = id;
    }
    private void OnMouseDown()
    {
        if (itemSO == null)
            return;
        if (itemSO.beingInspected)
            return;

        //pick up item
        ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().AddItem(itemSO);
        itemSO.isInCorrectPosition = false;
        ItemPool.Instance.AddItem(gameObject);

        
        if (transform.parent != null && transform.parent.TryGetComponent<ItemPoint>(out var itemPoint))
        {
            transform.SetParent(null);
            itemPoint.items.Remove(itemSO);
            itemPoint.HasAllNeededItems();
        }
        transform.SetParent(null);
    }
}
