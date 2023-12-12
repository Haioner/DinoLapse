using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine;
using TMPro;

public class LocaleSelector : MonoBehaviour
{
    public TMP_Dropdown dropDown;
    AsyncOperationHandle initializeOperation;

    private void Start()
    {
        dropDown.onValueChanged.AddListener(OnSelectionChanged);

        dropDown.ClearOptions();
        dropDown.options.Add(new TMP_Dropdown.OptionData("Loading..."));
        dropDown.interactable = false;

        initializeOperation = LocalizationSettings.SelectedLocaleAsync;
        if (initializeOperation.IsDone)
            LoadOptions(initializeOperation);
        else
            initializeOperation.Completed += LoadOptions;
    }

    void LoadOptions(AsyncOperationHandle obj)
    {
        //Add options for each locale
        var options = new List<string>();
        int selectedOption = 0;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        for (int i = 0; i < locales.Count; i++)
        {
            var locale = locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selectedOption = i;
            options.Add(locales[i].ToString());
        }

        //On ERROR
        if (options.Count == 0)
        {
            options.Add("No Locales Available");
            dropDown.interactable = false;
        }
        else
        {
            dropDown.interactable = true;
        }

        dropDown.ClearOptions();
        dropDown.AddOptions(options);
        dropDown.SetValueWithoutNotify(selectedOption);

        LocalizationSettings.SelectedLocaleChanged += LocalizationSelectedLocale;
        LoadSelection();
    }

    void OnSelectionChanged(int index)
    {
        // Unsubscribe from SelectedLocaleChanged so we don't get an unnecessary callback from the change we are about to make.
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSelectedLocale;

        var locale = LocalizationSettings.AvailableLocales.Locales[index];
        LocalizationSettings.SelectedLocale = locale;

        // Resubscribe to SelectedLocaleChanged so that we can stay in sync with changes that may be made by other scripts.
        LocalizationSettings.SelectedLocaleChanged += LocalizationSelectedLocale;
    }

    void LocalizationSelectedLocale(Locale locale)
    {
        var selectedIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(locale);
        dropDown.SetValueWithoutNotify(selectedIndex);
    }

    public void SaveSelection()
    {
        PlayerPrefs.SetInt("Locale", dropDown.value);
    }

    void LoadSelection()
    {
        dropDown.value = PlayerPrefs.GetInt("Locale");
    }
}