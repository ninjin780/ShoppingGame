using UnityEngine;
using UnityEngine.Audio;

public class ShopButtons : MonoBehaviour
{
    [Header("Inventarios (ScriptableObjects)")]
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Inventory shopInventory;

    [Header("UI Roots (Padres donde están los slots)")]
    [SerializeField] private Transform playerUIRoot;
    [SerializeField] private Transform shopUIRoot;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buySound;
    [SerializeField] private AudioClip sellSound;


    public void OnEnable()
    {
        ItemSlotUI.BuyItem += Buy;
        ItemSlotUI.SellItem += Sell;
    }
    public void OnDisable()
    {
        ItemSlotUI.BuyItem -= Buy;
        ItemSlotUI.SellItem -= Sell;
    }

    private int GetPrice(ItemBase item) => item.Cost;

    public void Buy()
    {
        var slotUI = ItemSelection.SlotSelected;
        if (slotUI == null)
        {
            Debug.Log("No hay item seleccionado");
            return;
        }

        if (shopUIRoot == null || (slotUI as ItemSlotUI).GetInventory().tag != "Shop")
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
            slotUI.ResetPosition();
            return;
        }

        shopInventory.RemoveItem(item);
        playerInventory.AddItem(item);

        audioSource.PlayOneShot(buySound);

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

        if (playerUIRoot == null || (slotUI as ItemSlotUI).GetInventory().tag != "Player")
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

        audioSource.PlayOneShot(sellSound);


        Debug.Log($"Vendido: {item.Name} (+{gain})");
    }
}