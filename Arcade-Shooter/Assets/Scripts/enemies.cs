﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemies : MonoBehaviour
{

    float DelayShoot;
    [SerializeField]
    float FireRate;
    [SerializeField]
    Transform Gun;
    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    int HP;
    [SerializeField]
    int[] RandomItemDrop;
    [SerializeField]
    int Distanse;
    [SerializeField]
    bool Ranged_Melee;
    [SerializeField]
    int Speed;
    [SerializeField]
    int[] MovmentType;
    [SerializeField]
    Transform[] Target;
    [SerializeField]
    Transform MainTarget;



    void Start()
    {
        // Target = GameObject.FindGameObjectWithTag("EnemyPosition").GetComponent<Transform>();
    }


    void Update()
    {
        basicMove();
        shoot();
        // GetComponent<MainJet>().Shoot(Bullet, Gun);
    }

    void basicMove()
    {
        MainTarget.position = Vector2.MoveTowards(transform.position, Target[0].position, Speed * Time.deltaTime);
    }

    public void TakeDamage(int Damage)
    {

        HP -= Damage;
        if (HP <= 0)
        {
            Death();
        }

    }
    void Death()
    {
        Destroy(gameObject);
    }
    void shoot()
    {
        DelayShoot += Time.deltaTime;
        if (DelayShoot > FireRate)
        {
            DelayShoot = 0;
            Instantiate(Bullet, Gun.position, Quaternion.identity);
        }
    }
}
