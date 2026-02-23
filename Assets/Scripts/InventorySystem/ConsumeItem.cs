using System;
using UnityEngine;

public class ConsumeItem : MonoBehaviour, IConsume
{
    public event Action<ConsumableItem> OnItemConsumed;
    public void Use(ConsumableItem item)
    {
        OnItemConsumed?.Invoke(item);
    }
}
