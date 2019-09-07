using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControler : MonoBehaviour
{
    private int MonyAmont;
    private int IsItemSolid;

    public Text MonyAmonttxt;
    public Text ItemPrisetxt;

    public Button BuyItemBTN;
    // Start is called before the first frame update
    void Start()
    {
        MonyAmont = PlayerPrefs.GetInt("MonyAmount");
    }

    // Update is called once per frame
    void Update()
    {
        MonyAmonttxt.text = "Mony" + MonyAmont.ToString() + "$";
        IsItemSolid = PlayerPrefs.GetInt("IsItemSolid");
        if (MonyAmont>5&&IsItemSolid==0)
        {
            BuyItemBTN.interactable = true;
        }
        else
        {
            BuyItemBTN.interactable = false;
        }
    }

    public void BuyItem()
    {
        MonyAmont = 5;
        PlayerPrefs.SetInt("IsItemSolid",1);
        ItemPrisetxt.text = "Solid";
        BuyItemBTN.gameObject.SetActive(false);
    }

    public void ExitShop()
    {
        PlayerPrefs.SetInt("MonyAmount",MonyAmont);
    }

    public void ReastShop()
    {
        MonyAmont = 0;
        BuyItemBTN.gameObject.SetActive(true);
        ItemPrisetxt.text = "Prise:5";
        PlayerPrefs.DeleteAll();
    }
}