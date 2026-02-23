using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory System/Items/Weapon")]
public class ItemWeapon : ConsumableItem
{
    public int Damage;

    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}
