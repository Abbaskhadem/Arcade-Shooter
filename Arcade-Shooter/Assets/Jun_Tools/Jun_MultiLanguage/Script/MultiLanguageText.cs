using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MultiLanguageText : MonoBehaviour
{
    public Jun_LanguageSettingType settingType = Jun_LanguageSettingType.Custom;

    public Jun_MultiLanguage multiLanguage = new Jun_MultiLanguage();
    Text uiText;

    [HideInInspector]
    [SerializeField]
    Jun_MultiLanguagePool m_languagePool;
    [HideInInspector]
    [SerializeField]
    string m_languageKey;
    [HideInInspector]
    [SerializeField]
    int m_languageID;

    // Use this for initialization

    void Start()
    {
        startLanguage.language = (sysLang)PlayerPrefs.GetInt("Language");
        ShowLanguage();
    }

    void ShowLanguage()
    {
        startLanguage.language = (sysLang)PlayerPrefs.GetInt("Language");
        uiText = GetComponent<Text>();
        if (uiText != null)
        {
            switch (settingType)
            {
                case Jun_LanguageSettingType.Custom:
                    Jun_Language thisLanguage = multiLanguage.GetLanguage();
                    if (thisLanguage != null)
                    {
                        uiText.SetLanguage(thisLanguage);

                        if (startLanguage.language == sysLang.Persian)
                        {
                            Font Iransans = (Font)Resources.Load("Iransans-Bold") as Font;
                            uiText.font = Iransans;
                            uiText.text = Farsi.faConvert(uiText.text);
                        }
                        else
                        {
                            Font Iransans = (Font)Resources.Load("Code Pro Bold") as Font;
                            uiText.font = Iransans;
                        }
                    }
                    break;

                case Jun_LanguageSettingType.LanguagePool:
                    if (m_languagePool != null)
                    {
                        Jun_MultiLanguage thisML = m_languagePool.GetLanguage(m_languageKey);
                        if (thisML == null)
                            thisML = m_languagePool.GetLanguage(m_languageID);
                        if (thisML != null)
                        {
                            uiText.SetLanguage(thisML.GetLanguage());
                            if (startLanguage.language == sysLang.Persian)
                            {
                                Font Iransans = (Font)Resources.Load("Iransans-Bold") as Font;
                                uiText.font = Iransans;
                                uiText.text = Farsi.faConvert(uiText.text);
                            }
                            else
                            {
                                Font Iransans = (Font)Resources.Load("Code Pro Bold") as Font;
                                uiText.font = Iransans;
                            }
                        }
                    }
                    break;
            }
        }
    }

    private void OnValidate()
    {
        ShowLanguage();
    }

    private void OnEnable()
    {
        Jun_MultiLanguage.onSystemLanguageChange += OnSystemLanguageChange;
    }

    private void OnDisable()
    {
        Jun_MultiLanguage.onSystemLanguageChange -= OnSystemLanguageChange;
    }

    void OnSystemLanguageChange(SystemLanguage systemLanguage)
    {
        ShowLanguage();
    }
}
