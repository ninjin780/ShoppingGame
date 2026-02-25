using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class ItemSelection : MonoBehaviour
{
    [SerializeField]
    public static ItemSlotUI SlotSelected;
    public static ItemSlotUI PreviousSlotSelected;
    private Vector3 originalScale;

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
        originalScale = new Vector3(1.5f, 1.5f, 1.5f);
        nameText.text = "";
        descriptionText.text = ""; 
    }
    private void Update()
    {
        if (SlotSelected != null && PreviousSlotSelected != SlotSelected)
        {
            if (PreviousSlotSelected != null)
            {
                PreviousSlotSelected.GetImage().transform.localScale = originalScale;
                PreviousSlotSelected.GetItemBase().IsSelected = false;
            }

            SlotSelected.GetImage().transform.localScale = new Vector3(2f, 2f, 2f);
            SlotSelected.GetItemBase().IsSelected = true;

            PreviousSlotSelected = SlotSelected;
        }
    }

    private void OnEnable()
    {
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
}