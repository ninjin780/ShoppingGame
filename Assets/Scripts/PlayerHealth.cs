using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class PlayerHealth : MonoBehaviour, IPointerDownHandler
{
    public Slider lifeBar;
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateLifeBar();
    }

    
    public void OnPointerDown(PointerEventData eventData)
    {
        TakeDamage(10); 
        Debug.Log("¡Ay! He recibido daño.");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateLifeBar();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateLifeBar();
    }

    private void UpdateLifeBar()
    {
        if (lifeBar != null)
        {
            lifeBar.value = currentHealth;
        }
    }
}