using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public int health;
    public int Damage;
    public GameObject[] bullet;
    public Rigidbody2D Body;
    public float AttackSpeed;
    public Transform[] GunPoints;
    public float Speed;
}
