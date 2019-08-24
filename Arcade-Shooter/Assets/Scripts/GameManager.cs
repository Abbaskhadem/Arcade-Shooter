using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;
    public bool GameEnded = false;
    private static string tag;

    private void Awake()
    {
        _Instance = this;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}


