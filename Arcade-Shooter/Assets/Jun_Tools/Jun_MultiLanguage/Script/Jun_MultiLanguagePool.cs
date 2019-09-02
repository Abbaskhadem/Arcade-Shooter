using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jun_MultiLanguagePool : MonoBehaviour 
{
    [System.Serializable]
    public class LanguageData
    {
        [HideInInspector][SerializeField]string m_key;
        [HideInInspector] [SerializeField] Jun_MultiLanguage m_language;

        public string key{ get { return m_key; }}
        public Jun_MultiLanguage language{ get { return m_language; }}
    }

    [HideInInspector] [SerializeField] List<LanguageData> m_languageDatas = new List<LanguageData>();

    public string[] keys
    {
        get
        {
            string[] keyNames = new string[m_languageDatas.Count];
            for (int i = 0; i < m_languageDatas.Count; i++)
            {
                keyNames[i] = m_languageDatas[i].key;
            }
            return keyNames;
        }
    }

    public Jun_MultiLanguage GetLanguage (int index)
    {
        if (index < m_languageDatas.Count)
            return m_languageDatas[index].language;
        return null;
    }

    public Jun_MultiLanguage GetLanguage (string key)
    {
        for (int i = 0; i < m_languageDatas.Count; i++)
        {
            if (key == m_languageDatas[i].key)
                return m_languageDatas[i].language;
        }
        return null;
    }

    public int GetKeyID (string key)
    {
        for (int i = 0; i < m_languageDatas.Count; i++)
        {
            if (key == m_languageDatas[i].key)
                return i;
        }
        return -1;
    }

    public string GetKey (int index)
    {
        if (index < m_languageDatas.Count)
            return m_languageDatas[index].key;
        return "";
    }
}
