using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public float health;
    public int Damage;
    public GameObject[] bullet;
   [HideInInspector] public Rigidbody2D Body;
    public float AttackSpeed;
    public Transform[] GunPoints;
    public float Speed;
}
