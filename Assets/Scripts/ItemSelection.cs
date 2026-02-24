using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class ItemSelection : MonoBehaviour
{
    [SerializeField]
    public static ItemSlotUI SlotSelected;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    private ItemBase item;

    private void Awake()
    {
        descriptionText.text = "No hay ningún item seleccionado";
    }

    private void OnEnable()
    {
        ItemSlotUI.ItemClicked += UseSelectedItem;   
    }

    private void OnDisable()
    {
        ItemSlotUI.ItemClicked -= UseSelectedItem;
    }

    public void UseSelectedItem(ItemSlotUI slot)
    {
        if (slot != null)
        {
            item = slot.GetItemBase();
            descriptionText.text = item.Name + ": " + item.Description;
        }
    }
}