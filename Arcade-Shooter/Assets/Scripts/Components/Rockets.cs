using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockets : MonoBehaviour
{
    private Transform Target;
    private float Speed = 6;
    void Start()
    {
        foreach (var VARIABLE in FindObjectsOfType<Enemy_SpaceShip>())
        {
            if (!VARIABLE.Targeted)
            {
                VARIABLE.Targeted = true;
                Target = VARIABLE.transform;
            }
        }
    }
    void Update()
    {
        if (Target.gameObject.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.up * Speed;
        }
    }
}
