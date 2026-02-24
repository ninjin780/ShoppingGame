using System;
using System.Collections.Generic;
using UnityEngine;

public class Localizer : MonoBehaviour
{
    public static Localizer Instance; // Singleton instance of Localizer

    public TextAsset DataSheet; // Unity text asset to be assigned (.csv)

    private Language currentLanguage;
    public Language DefaultLanguage;

    Dictionary<string, LanguageData> Data; // Text data from CSV

    public static Action OnLanguageChange; // Change language event

    private void Awake()
    {
        Instance = this;
        currentLanguage = DefaultLanguage;

        LoadLanguageSheet();
    }

    public static string GetText(string textKey)
    {
        return Instance.Data[textKey].GetText(Instance.currentLanguage);
    }

    public static void SetLanguage(Language language)
    {
        Instance.currentLanguage = language;

        OnLanguageChange?.Invoke();
    }

    void LoadLanguageSheet()
    {
        string[] lines = DataSheet.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (lines.Length > 1) AddLanguageData(lines[i]);
        }
    }

    void AddLanguageData(string str)
    {
        // Lazy initialization
        if (Data == null) Data = new Dictionary<string, LanguageData>();

        string[] entries = str.Split(';');

        var languageData = new LanguageData(entries);

        Data.Add(entries[0], languageData);
    }
}
