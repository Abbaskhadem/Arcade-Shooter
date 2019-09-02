using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class startLanguage : MonoBehaviour
{

    //public Dropdown dropdown;
    //public Toggle toggle;
    public static sysLang language;
    // Use this for initialization
    void Start()
    {
        int languageId = PlayerPrefs.GetInt("Language");
        SelectLanguage((sysLang)languageId);
        language = (sysLang)languageId;
        /*List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
        list.Add(new Dropdown.OptionData("English", null));
        list.Add(new Dropdown.OptionData(Farsi.faConvert("فارسی"), null));
        list.Add(new Dropdown.OptionData("Français", null));
        list.Add(new Dropdown.OptionData("Deutsch", null));
        list.Add(new Dropdown.OptionData(Farsi.faConvert("العربی"), null));
        dropdown.AddOptions(list);
        dropdown.onValueChanged.AddListener(OnValueChange);
        dropdown.value = langageId;*/
        //toggle.onValueChanged.AddListener(OnAutoLanguage);

        //toggle.isOn = true;
    }

    void OnAutoLanguage(bool v)
    {
        Jun_MultiLanguage.AutoLanguage(v);
    }
    void SelectLanguage(sysLang languageId)
    {
        Jun_MultiLanguage.SetSystemLanguage(languageId);
    }
}
