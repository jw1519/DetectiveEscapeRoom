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
        Inventory.Instance.AddItem(itemSO);
        itemSO.isInCorrectPosition = false;
        ItemPool.Instance.AddItem(gameObject);

        ItemPoint itemPoint = transform.parent.GetComponent<ItemPoint>();
        if (itemPoint != null)
        {
            itemPoint.HasAllNeededItems();
        }
        transform.SetParent(null);
    }
}
