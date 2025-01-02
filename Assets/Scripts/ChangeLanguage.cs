using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeLanguage : MonoBehaviour
{
    public void ChangeLocale()
    {
        // Determine the next locale index
        int currentLocaleIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        int nextLocaleIndex = (currentLocaleIndex == 0) ? 1 : 0;

        // Start the locale change
        StartCoroutine(SetLocale(nextLocaleIndex));
    }

    IEnumerator SetLocale(int localeID)
    {
        // Wait for localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        // Ensure the localeID is within valid bounds
        if (localeID >= 0 && localeID < LocalizationSettings.AvailableLocales.Locales.Count)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        }
        else
        {
            Debug.LogError($"Invalid localeID: {localeID}");
        }
    }
}
