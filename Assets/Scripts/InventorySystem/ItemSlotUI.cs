using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    // NOTE: Inventory UI slots support drag&drop,
    // implementing the Unity provided interfaces by events system

    public Image Image;
    public TextMeshProUGUI AmountText;
    public TextMeshProUGUI Price;

    private Canvas canvas;
    private Transform parent;
    private ItemBase item;
    private InventoryUI inventory;
    private CanvasGroup canvasGroup;

    public static event Action<ItemSlotUI> ItemClicked;
    public static event Action BuyItem;
    public static event Action SellItem;

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        Image.sprite = slot.Item.ImageUI;
        Image.SetNativeSize();

        AmountText.text = slot.Amount.ToString();
        Price.text = slot.Item.Cost.ToString() + " c";
        AmountText.enabled = (slot.Amount > 1);

        item = slot.Item;
        this.inventory = inventory;
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store previous reference position
        parent = transform.parent;

        // Change parent of our item to the canvas
        transform.SetParent(canvas.transform, true);
        
        // And set it as last child to be rendered on top of UI
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Moving object around screen using mouse delta
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        // Find scene objects colliding with mouse point on end dragging
        GameObject hitData = eventData.pointerCurrentRaycast.gameObject;

        canvasGroup.blocksRaycasts = true;




        if (hitData)
        {
            if (hitData.GetComponent<InventoryUI>())
            {
                if (hitData.GetComponent<InventoryUI>().tag == "Shop" && ItemSelection.SlotSelected.inventory.tag == "Player")
                {
                    SellItem?.Invoke();
                }

                else if (hitData.GetComponent<InventoryUI>().tag == "Player" && ItemSelection.SlotSelected.inventory.tag == "Shop")
                {
                    BuyItem?.Invoke();
                }

                else
                {
                    ResetPosition();
                }
            }

            else
            {
                ResetPosition();
            }
        }

        else
        {
            ResetPosition();
        }
    }

    public ItemBase GetItemBase()
    {
        return item;
    }

    public InventoryUI GetInventory()
    {
        return inventory;
    }

    public Image GetImage()
    {
        return Image;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        item.IsSelected = true;
        ItemClicked?.Invoke(this);
    }

    public void ResetPosition()
    {
        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
    }
}