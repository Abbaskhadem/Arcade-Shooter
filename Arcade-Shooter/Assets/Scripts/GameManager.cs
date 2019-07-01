using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Temp;
    public static float Width;
    public static float Height;
    [SerializeField]
    Camera Cam;
    public static bool GameLost = false;
    int a, b;
    // Start is called before the first frame update
    void Start()
    {
        Height = 2f * Cam.orthographicSize;
        Width = Height * Cam.aspect;
        //Debug.Log(Width);
        //Debug.Log(SetXGrid());

        //Temp.GetComponent<Transform>().localScale = new Vector3(Width, Height, 0);


        // Instantiate(Temp, GG(1, 5), Quaternion.identity);
        //Instantiate(Temp, GG(4, 0), Quaternion.identity);
        //Instantiate(Temp, GG(6, 0), Quaternion.identity);
        //Instantiate(Temp, GG(8, 0), Quaternion.identity);
        //Instantiate(Temp, GG(10, 0), Quaternion.identity);
        //Instantiate(Temp, GG(12, 0), Quaternion.identity);



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
}


