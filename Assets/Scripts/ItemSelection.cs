using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class ItemSelection : MonoBehaviour
{
    [SerializeField]
    public static ItemSlotUI SlotSelected;
    public Vector3 originalScale = new Vector3(1.5f,1.5f,1.5f); 

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;
    private ItemBase item;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip selectedSFX;

    private void Awake()
    {
        nameText.text = "";
        descriptionText.text = "";
    }

    private void OnEnable()
    {
        ItemSlotUI.ItemClicked += SelectionVFX;
        ItemSlotUI.ItemClicked += SelectionSound;
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
        if (item != null)
        {
            nameText.text = Localizer.GetText(item.Name);
            descriptionText.text = Localizer.GetText(item.Description);
        }
    }

    private void SelectionSound(ItemSlotUI slot)
    {
       audioSource.PlayOneShot(selectedSFX);
    }

    private void SelectionVFX(ItemSlotUI slot)
    {
        slot.GetImage().transform.localScale = Vector3.one * 2.0f;
    }
}