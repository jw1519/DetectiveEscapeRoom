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
    public Vector3 scale;
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
        ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().SelectItem(this);
    }
    public virtual void UseItem()
    {
    }
    public virtual void PlaceItem(Transform parent, Vector3 postion)
    {
        GameObject prefab = ItemPool.Instance.GetItem(itemID);
        prefab.transform.SetParent(parent);
        prefab.transform.localPosition = postion;
        prefab.transform.localRotation = Quaternion.identity;

        if (scale != Vector3.zero)
            prefab.transform.localScale = scale;

        ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().RemoveItem(this);
        ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().DeselectItem();
    }
}
