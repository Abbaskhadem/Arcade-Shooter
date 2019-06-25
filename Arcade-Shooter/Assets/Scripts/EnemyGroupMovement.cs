using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupMovement : MonoBehaviour
{
    public int MovementType;
    public int Speed;
    Transform MainTarget;
    Transform EnemyTarget;
    void Start()
    {
        MainTarget = GameObject.FindGameObjectWithTag("EnemyPosition").GetComponent<Transform>();
        EnemyTarget = GetComponent<Transform>();
    }
    void Update()
    {
        switch (MovementType)
        {
            case 1:
                StraightMovement();
                break;
            default:
                break;
        }
    }
    void StraightMovement()
    {
        EnemyTarget.position = Vector2.MoveTowards(transform.position, MainTarget.position, Speed * Time.deltaTime);
    }
}
