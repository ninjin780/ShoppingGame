using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    [Header("Inventarios (ScriptableObjects)")]
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Inventory shopInventory;

    [Header("UI Roots (Padres donde están los slots)")]
    [SerializeField] private Transform playerUIRoot;
    [SerializeField] private Transform shopUIRoot;

    private int GetPrice(ItemBase item) => item.Cost;

    public void Buy()
    {
        var slotUI = ItemSelection.SlotSelected;
        if (slotUI == null)
        {
            Debug.Log("No hay item seleccionado");
            return;
        }

        if (shopUIRoot == null || !slotUI.transform.IsChildOf(shopUIRoot))
        {
            Debug.Log("Solo puedes comprar items de la tienda");
            return;
        }

        ItemBase item = slotUI.GetItemBase();
        if (item == null)
        {
            Debug.Log("Slot vacío");
            return;
        }

        int price = GetPrice(item);

        if (!MoneyManager.Instance.Spend(price))
        {
            Debug.Log("No tienes suficiente dinero");
            return;
        }

        shopInventory.RemoveItem(item);
        playerInventory.AddItem(item);

        Debug.Log($"Comprado: {item.Name} (-{price})");
    }

    public void Sell()
    {
        var slotUI = ItemSelection.SlotSelected;
        if (slotUI == null)
        {
            Debug.Log("No hay item seleccionado");
            return;
        }

        if (playerUIRoot == null || !slotUI.transform.IsChildOf(playerUIRoot))
        {
            Debug.Log("Solo puedes vender items de tu inventario");
            return;
        }

        ItemBase item = slotUI.GetItemBase();
        if (item == null)
        {
            Debug.Log("Slot vacío");
            return;
        }

        int gain = GetPrice(item);

        playerInventory.RemoveItem(item);
        shopInventory.AddItem(item);

        MoneyManager.Instance.Add(gain);

        Debug.Log($"Vendido: {item.Name} (+{gain})");
    }
}