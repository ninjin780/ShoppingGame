using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IPointerDownHandler
{
    public static PlayerHealth Instance { get; private set; }
    public Slider lifeBar;
    public int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        UpdateLifeBar();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        TakeDamage(10); 
        Debug.Log("Ay! He recibido daño.");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene("Ending");
        }
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