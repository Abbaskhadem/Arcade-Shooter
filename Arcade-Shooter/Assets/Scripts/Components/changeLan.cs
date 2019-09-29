using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLan : MonoBehaviour
{
    public void toPersian()
    {
        ChangeLanguage(sysLang.Persian);
        ChangeLanguage(sysLang.Persian);
    }
    public void toEnglish()
    {
        ChangeLanguage(sysLang.English);
        ChangeLanguage(sysLang.English);
    }
    /*sysLang binder(int lanNo)
    {
        switch (lanNo)
        {
            case sysLang.English:
                return 0;
            case sysLang.Persian:
                return 1;
            case sysLang.French:
                return 2;
            case sysLang.German:
                return 3;
            case sysLang.Arabic:
                return 4;
        }
    }*/
    void ChangeLanguage(sysLang v)
    {
        Jun_MultiLanguage.SetSystemLanguage(v);
        startLanguage.language = v;
        PlayerPrefs.SetInt("Language", (int)v);
    }
}
