using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemID;
    public Sprite itemIcon;
    public bool isInCorrectPosition;
    public bool canBePlaced;
    public bool beingInspected;
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(itemID))
        {
            //create unique id for item
            itemID = itemName + "-" + System.Guid.NewGuid().ToString();
        }
    }
    public virtual void SelectItem()
    {
        Inventory.Instance.SelectItem(this);
    }
    public virtual void UseItem()
    {
    }
    public virtual void PlaceItem(Transform parent)
    {
        GameObject prefab = ItemPool.Instance.GetItem(itemID);
        prefab.transform.SetParent(parent);
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localRotation = Quaternion.identity;
        Inventory.Instance.RemoveItem(this);
        Inventory.Instance.DeselectItem();
    }
}
