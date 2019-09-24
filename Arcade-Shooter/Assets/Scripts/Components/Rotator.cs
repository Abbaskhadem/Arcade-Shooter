using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [HideInInspector]public float Speed = 2f;
    void Update()
    {
        transform.Rotate(0,0,Speed);
    }
}
