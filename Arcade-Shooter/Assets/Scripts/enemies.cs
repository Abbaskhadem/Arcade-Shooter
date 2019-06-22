using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemies : MonoBehaviour
{
    [SerializeField]
    int FireRate;
    [SerializeField]
    int Damage;
    public int HP;
    [SerializeField]
    int [] RandomItemDrop ;
    [SerializeField]
    int Distanse;
    [SerializeField]
    bool Ranged_Melee;
    [SerializeField]
    int Speed;
    [SerializeField]
    int[] MovmentType;
    [SerializeField]
    Transform [] Target;
    [SerializeField]
    Transform MainTarget;
   
    void Start()
    {
       // Target = GameObject.FindGameObjectWithTag("EnemyPosition").GetComponent<Transform>();
    }


    void Update()
    {
        basicMove();


    }

    void basicMove()
    {
        MainTarget.position = Vector2.MoveTowards(transform.position, Target[0].position, Speed * Time.deltaTime);
    }
}
