using System.Reflection;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    private const string MONEY_KEY = "MONEY_VALUE";

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

        LoadMoney();
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
        SaveMoney();
        RefreshUI();
        return true;
    }

    public void Add(int amount)
    {
        money += amount;
        SaveMoney();
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (moneyText == null) return;

        moneyText.text = Localizer.GetText("MONEY") + ": " + money;
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MONEY_KEY, money);
        PlayerPrefs.Save();
    }

    private void LoadMoney()
    {
        if (PlayerPrefs.HasKey(MONEY_KEY))
            money = PlayerPrefs.GetInt(MONEY_KEY);
    }
}