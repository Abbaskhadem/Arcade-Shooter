using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Temp;
    float Width;
    float Height;
    [SerializeField]
    Camera Cam;
    public static bool GameLost=false;
    int a, b;
    // Start is called before the first frame update
    void Start()
    {
        Height = 2f * Cam.orthographicSize;
        Width = Height * Cam.aspect;
        //Debug.Log(Width);
        //Debug.Log(SetXGrid());
        
        //Temp.GetComponent<Transform>().localScale = new Vector3(Width, Height, 0);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(Temp, GG(i, i), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void LoseGame()
    {
        GameLost = true;
    //    Debug.Log("Game Has Ended!");
    }
    float SetXGrid()
    {
        return Width / 2f;
    }
    float SetYGrid()
    {
        return Height / 2f;
    }
    Vector2 GG(int a, int b)
    {
        float aa = a*SetXGrid() - Width/2;
        float bb = (-1)*(b*SetYGrid() - Height/2);
        //aa = aa + (SetXGrid() / 2f);
        //bb = bb - (SetYGrid() / 2f);
        Vector2 natije = new Vector2(aa, bb);
        return natije;
    }
}
