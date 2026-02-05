using UnityEngine;

public class Room : ItemPoint
{
    public override void UseItemOnPoint(Item item)
    {
        base.UseItemOnPoint(item);
        if (isComplete)
        {
            manager.GetComponent<Map>().Check();
        }
    }
}
