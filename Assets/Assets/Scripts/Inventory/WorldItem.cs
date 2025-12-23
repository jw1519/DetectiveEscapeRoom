using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item itemSO;

    private void OnMouseDown()
    {
        if (itemSO == null)
            return;

        //pick up item
        Inventory.Instance.AddItem(itemSO);
        itemSO.isInCorrectPosition = false;
        ItemPool.Instance.AddItem(gameObject);
    }
}
