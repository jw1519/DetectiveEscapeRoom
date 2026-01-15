using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item item;
    public Button inspectButton;

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
        Button itemButton = GetComponent<Button>();
        itemButton.onClick.AddListener(() => Inventory.Instance.SelectItem(item));

        inspectButton.onClick.AddListener(() => Inspect.instance.EnableInspect(item));
    }
}
