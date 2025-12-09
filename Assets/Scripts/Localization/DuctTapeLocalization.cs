using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DuctTapeLocalization : MonoBehaviour
{
    void Awake()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];        
    }

}
