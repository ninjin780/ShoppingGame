using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UseButton : MonoBehaviour, IPointerDownHandler
{
    public static event Action UseItem;
    public void OnPointerDown(PointerEventData eventData)
    {
        UseItem?.Invoke();
    }
}
