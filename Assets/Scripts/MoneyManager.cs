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

        moneyText.text = $"{GetMoneyLabel()}: {money}";
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

    private string GetMoneyLabel()
    {
        Language lang = GetCurrentLanguage();

        switch (lang)
        {
            case Language.Spanish: return "Dinero";
            case Language.Catalan: return "Diners";
            case Language.English: return "Money";
            default: return "Money";
        }
    }

    private Language GetCurrentLanguage()
    {
        FieldInfo field = typeof(Localizer).GetField(
            "currentLanguage",
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        if (field == null || Localizer.Instance == null)
            return Language.English;

        return (Language)field.GetValue(Localizer.Instance);
    }
}