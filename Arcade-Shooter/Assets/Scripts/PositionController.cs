﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    void Start()
    {
        Debug.Log(WavePositions[1].positions.Length);
    }
    public static PositionController Instance;
    //[SerializeField] private Transform[] positions;
    [System.Serializable]
    public class _WavePositions
    {
    public Transform[] positions;
    }

    public _WavePositions[] WavePositions;
    
    public int _index;

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetPosition(int i)
    {
        if (_index == WavePositions[i].positions.Length)
        {
            Debug.LogError("More SpaceShip Than Final Positions");
            _index = 0;
        }
//        foreach (var position in WavePositions[i].positions)
//        {
//            if (Math.Abs(position.position.z - _index) < 0.5f) return position;
//        }
        return WavePositions[i].positions[_index++];
    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        foreach (var Transform in WavePositions)
//        {
//            Gizmos.DrawSphere(new Vector3(0,0,0), 0.5f);
//        }
//    }
}
