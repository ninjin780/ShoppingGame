using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class ItemSelection : MonoBehaviour
{
    [SerializeField]
    public static ItemSlotUI SlotSelected;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    private ItemBase item;

    private void Awake()
    {
        descriptionText.text = "No hay ning�n item seleccionado";
    }

    private void OnEnable()
    {
        ItemSlotUI.ItemClicked += UseSelectedItem;
        Localizer.OnLanguageChange += ChangeLanguage;
    }

    private void OnDisable()
    {
        ItemSlotUI.ItemClicked -= UseSelectedItem;
        Localizer.OnLanguageChange -= ChangeLanguage;
    }

    public void UseSelectedItem(ItemSlotUI slot)
    {
        if (slot != null)
        {
            SlotSelected = slot;
            item = slot.GetItemBase();
            nameText.text = Localizer.GetText(item.Name);
            descriptionText.text = Localizer.GetText(item.Description);
        }
    }

    private void ChangeLanguage()
    {
        nameText.text = Localizer.GetText(item.Name);
        descriptionText.text = Localizer.GetText(item.Description);
    }
}