using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;
    public bool GameEnded = false;
    [SerializeField]
    GameObject Temp;
    public static float Width;
    public static float Height;
    private static string tag;
    [SerializeField]
   static Vector2[] Positions;
    Camera Cam;
    public static bool GameLost = false;
    int a, b;

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
            //return ListOBJ;
        }

        return ListOBJ;
    }
    public static void LoseGame()
    {
        GameLost = true;
        //    Debug.Log("Game Has Ended!");
    }
    public static float SetXGrid()
    {
        return Width / 12f;
    }
    public static float SetYGrid()
    {
        return Height / 8f;
    }
    public static Vector2 GG(float a, float b)
    {
        float aa = a * SetXGrid() - Width / 2;
        float bb = (-1) * (b * SetYGrid() - Height / 2);
        // aa = aa + (SetXGrid() / 2f);
        // bb = bb - (SetYGrid() / 2f);
        Vector2 natije = new Vector2(aa, bb);
        return natije;
    }

    public static Vector2 []EnemyPositions(int Columns, int Rows,int startcol,int startRow)
    {
        int k = 0;

        for (int i = startRow; i >= Rows; i--)
        {
            for (int j = startcol; j <= Columns; j++)
            {
                Positions[k]=new Vector2(startcol,startRow);
                k++;
                
            }
        }

        return Positions;
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


