using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item item;
    public Button inspectButton;

    Button itemButton;

    public void SetItem(Item i)
    {
        item = i;
    }
    private void OnEnable()
    {
        //set item icon and name
        Image itemImage = GetComponent<Image>();
        itemImage.sprite = item.itemIcon;
        gameObject.name = item.itemID;

        //add button listener
        itemButton = GetComponent<Button>();
        //itemButton.onClick.AddListener(() => Inventory.Instance.SelectItem(item));
        itemButton.onClick.AddListener(() => ManagerUI.Instance.panels.Find(panel => panel.name == "InventoryPanel").gameObject.GetComponent<InventoryPanel>().SelectItem(item));

        inspectButton.onClick.AddListener(() => Inspect.instance.EnableInspect(item));
    }
    public void Select()
    {
        //highlight item in inventory
        GetComponent<Image>().color = Color.yellow;
    }
    public void Deselect()
    {
        //remove highlight from item in inventory
        GetComponent<Image>().color = Color.white;
    }
}
