using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        
       // Debug.Log(transform.position.y);
        if(!GameManager._Instance.GamePause)
             transform.Translate(0,Speed,0);
        if (gameObject.GetComponent<Transform>().position.y <= -17)
        {
            transform.position=new Vector3(0,17f,26);
        }
    }
}
