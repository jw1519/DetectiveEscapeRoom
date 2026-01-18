using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item itemSO;

    private void Awake()
    {
        itemSO = Instantiate(itemSO);
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
    }
}
