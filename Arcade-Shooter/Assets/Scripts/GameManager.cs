using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;
   [HideInInspector] public bool GameEnded = false;
   [HideInInspector] public bool GamePause = false;
    private static string tag;
    public _ShopItems[] ShopItems;
    [System.Serializable]
    public class _ShopItems
    {
        public string Item;
        public int PriceValue;
        public int PriceCost;
        public int ValuePlus;
    }
    private void Awake()
    {
        _Instance = this;
    }

    void Start()
    {
        PlayerPrefs.SetInt("Coins",10000);
    }

    public static List<GameObject> ObjectPooler(GameObject Obj, int MaxObj)
    {
        List<GameObject> ListOBJ = new List<GameObject>();
        for (int i = 0; i < MaxObj; i++)
        {
            GameObject MainObj = (GameObject) Instantiate(Obj);
            MainObj.SetActive(false);
            ListOBJ.Add(MainObj);
        }
        return ListOBJ;
    }

    public void LoadScence(int Level)
    {
        SceneManager.LoadScene(Level);
    }

    public void Restart()
    {
        GamePause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        GamePause = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        GamePause = false;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        GamePause = false;
        SceneManager.LoadScene(0);
    }

    public void Shop(int ID)
    {
        if (PlayerPrefs.GetInt("Coins") >= ShopItems[ID].PriceValue)
        {
            int temp = PlayerPrefs.GetInt(ShopItems[ID].Item);
            PlayerPrefs.SetInt(ShopItems[ID].Item,temp+ShopItems[ID].PriceCost);
           ShopItems[ID].PriceValue += ShopItems[ID].ValuePlus;
        }
    }
}


