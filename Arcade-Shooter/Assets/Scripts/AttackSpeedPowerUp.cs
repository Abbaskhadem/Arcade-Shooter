using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedPowerUp : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-8f));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<Main_SpaceShip>().AttackSpeed -= 1f;
            Destroy(gameObject);
        }
    }
}
