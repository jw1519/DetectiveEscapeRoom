using UnityEngine;

public class ItemPointDestroy : ItemPoint
{
    public override void UseItemOnPoint(Item item)
    {
        if (itemNeeded != null && CheckItemNeeded(item))
        {
            RemoveItems();
            Destroy(gameObject);
        }
        else
            Debug.Log("cant use that here");
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
