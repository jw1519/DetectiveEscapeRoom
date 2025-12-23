using UnityEngine;
using System.Collections.Generic;

public class ItemPool : MonoBehaviour
{
    public static ItemPool Instance;
    public List<GameObject> items;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public GameObject GetItem(string itemId)
    {
        foreach (GameObject item in items)
        {
            Item itemSO = item.GetComponent<WorldItem>().itemSO;

            if (itemSO.itemID == itemId)
            {
                Debug.Log("Retrieving item from pool: " + item.name);
                RemoveItem(item);
                return item;
            }
        }
        return null;
    }
    public void AddItem(GameObject item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            item.SetActive(false);
        }
    }
    public void RemoveItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            item.SetActive(true);
        }
    }
}
