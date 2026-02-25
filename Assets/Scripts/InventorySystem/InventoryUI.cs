using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory Inventory;
    public ItemSlotUI SlotPrefab;

    List<GameObject> itemStartSlotList;
    List<GameObject> itemSlotList;

    void Start()
    {
        itemSlotList = new List<GameObject>();
        itemStartSlotList = new List<GameObject>();
        FillInventoryUI(Inventory);
    }

    private void OnEnable()
    {
        Inventory.OnInventoryChange += UpdateInventoryUI;
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChange -= UpdateInventoryUI;
    }

    private void UpdateInventoryUI()
    {
        // Regenerate full inventory on changes
        ClearInventoryUI();
        FillInventoryUI(Inventory);
    }

    private void ClearInventoryUI()
    {
        foreach (var item in itemSlotList)
        {
            if (item) Destroy(item);
        }

        itemSlotList.Clear();
    }

    private void FillInventoryUI(Inventory inventory)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            itemSlotList.Add(AddSlot(inventory.GetSlot(i)));
        }
    }

    private GameObject AddSlot(ItemSlot itemSlot)
    {
        // Add a new visual slot UI in inventory UI, using provided prefab
        var element = GameObject.Instantiate(SlotPrefab, Vector3.zero, Quaternion.identity, transform);
        
        element.Initialize(itemSlot, this);

        return element.gameObject;
    }

    public void UseItem(ItemBase item)
    {
        Inventory.RemoveItem(item);
    }
}
