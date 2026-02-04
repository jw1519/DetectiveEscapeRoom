using UnityEngine;

public class Room : ItemPoint
{
    public override void UseItemOnPoint(Item item)
    {
        base.UseItemOnPoint(item);
        item.itemPrefab.transform.rotation = Quaternion.Euler(90, 0, 0); // reset rotation when placed
        if (isComplete)
        {
            manager.GetComponent<Map>().Check();
        }
    }
}
