using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    private int b = 100;
    public UnityEvent Abbas;
    // Start is called before the first frame update
    void Start()
    {
        Ontest += GetDebugged;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Abbas.Invoke();
        }
    }

    public delegate void testhandler(int j);

    public event testhandler Ontest;

    public void GetDebugged(int a)
    {
        Debug.Log(a);
    }

}
