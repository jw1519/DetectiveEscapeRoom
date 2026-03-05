using UnityEngine;

public class Room : ItemPoint
{
    public float spacing;
    public override void UseItemOnPoint(Item item)
    {
        if (isComplete)
        {
            manager.GetComponent<Map>().Check();
        }
        if (CheckItem(item))
        {
            if (items.Count == maxItems)
            {
                Debug.Log("can't place another item");
                return;
            }
            Inventory.Instance.RemoveItem(item);
            int amount = items.Count;
            item.PlaceItem(transform, new Vector3(spacing * amount, 0, 0));
            items.Add(item);
        }
        else
            base.UseItemOnPoint(item);
    }
}
