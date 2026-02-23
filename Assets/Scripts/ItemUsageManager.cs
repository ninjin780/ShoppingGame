using NUnit.Framework.Interfaces;
using UnityEngine;

public class ItemUsageManager : MonoBehaviour
{
    public PlayerHealth playerHealth; // Arrastra aquí el objeto que tiene el script PlayerHealth

    // Esta función la llamarás desde el evento OnClick() de tu botón [Use Item]
    public void UseSelectedItem(ItemBase itemToUse)
    {
        if (itemToUse == null) return;

        if (itemToUse.isConsumable)
        {
            playerHealth.Heal(itemToUse.lifeRestore);
            Debug.Log($"Has consumido {itemToUse.itemName} y recuperado {itemToUse.lifeRestore} de vida.");

            // IMPORTANTE: Aquí deberás añadir tu propia lógica para eliminar el item del inventario.
            // Ejemplo: PlayerInventory.RemoveItem(itemToUse);
        }
        else
        {
            Debug.Log("Este item no se puede consumir.");
        }
    }
}