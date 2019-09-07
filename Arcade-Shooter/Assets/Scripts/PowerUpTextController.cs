using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTextController : MonoBehaviour
{
    public static PowerUpTextController instance;
    private List<GameObject> textList = new List<GameObject>();
   [SerializeField] private GameObject PowerText;

    private void Awake()
    {
        instance = this;
    }

    public static PowerUpTextController Instance
    {
        get
        {
            if (!instance) instance = GameObject.Find("Power Up Text").GetComponent<PowerUpTextController>();
//            print(instance == null);
            return instance;
        }
    }

    public void Creat(string text, Vector2 position)
    {
        GetText(position).text = text;
    }

    private Text GetText(Vector2 targetPosition)
    {
        foreach (var text in textList)
        {
            if (text.activeSelf) continue;
            text.transform.position = targetPosition;
            text.SetActive(true);
            return text.GetComponent<Text>();
        }
        
        var newText = Instantiate(PowerText, transform);
        textList.Add(newText);
        var textComponent = newText.GetComponent<Text>();
        newText.transform.position = targetPosition;
        return textComponent;
    }
}
