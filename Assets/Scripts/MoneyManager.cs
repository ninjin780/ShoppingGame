using System.Reflection;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    private const string MONEY_KEY = "MONEY";

    [SerializeField] private int money = 100;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        RefreshUI();
    }

    private void OnEnable()
    {
        Localizer.OnLanguageChange += RefreshUI;
    }

    private void OnDisable()
    {
        Localizer.OnLanguageChange -= RefreshUI;
    }

    public bool Spend(int amount)
    {
        if (money < amount) return false;

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
        if (moneyText == null) return;
        Debug.Log("Money: "+money);
        moneyText.text = Localizer.GetText(MONEY_KEY) + ": " + money;
    }
}