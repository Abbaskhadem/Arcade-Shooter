using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class AttackDamage_Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject PowerText;
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-18));
        Destroy(gameObject,5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(PowerText, other.transform.position, UnityEngine.Quaternion.identity);
            other.GetComponent<Main_SpaceShip>().UpgradeWeapons();   
            Destroy(gameObject);
        }

    }
}
