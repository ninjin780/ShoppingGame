using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private int money = 100;
    [SerializeField] private TextMeshProUGUI moneyText;

    public int Money => money;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        RefreshUI();
    }

    public bool CanAfford(int amount) => money >= amount;

    public bool Spend(int amount)
    {
        if (!CanAfford(amount)) return false;
        money -= amount;
        RefreshUI();
        return true;
    }

    public void Add(int amount)
    {
        money += amount;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (moneyText != null)
            moneyText.text = "Dinero: " + money;
    }
}
