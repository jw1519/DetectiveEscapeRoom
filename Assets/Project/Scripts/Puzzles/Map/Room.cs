using UnityEngine;

public class Room : ItemPoint
{
    private void Start()
    {
        maxItems = 5;
    }
    public override void UseItemOnPoint(Item item)
    {
        base.UseItemOnPoint(item);
        if (isComplete)
        {
            manager.GetComponent<Map>().Check();
        }
    }
}
