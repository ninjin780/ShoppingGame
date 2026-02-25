using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Generic")]
public class ItemBase : ScriptableObject
{
    public string Name;
    public int Cost;
    public string Description;
    public Sprite ImageUI;
    public bool IsStackable;
    public bool IsSelected;
}