using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int HP;
    Rigidbody2D rb;
    public int Damage;
    [SerializeField]
    float Speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * Speed;
    }
    void Update()
    {
        Destroy(gameObject, 0.7f);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (Speed > 0)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
              //  col.GetComponent<enemies>().TakeDamage(Damage);
            }
        }
        else
        {
            if (col.gameObject.tag == "Player")
            {
                Destroy(gameObject);
                col.GetComponent<MainJet>().TakeDamage(Damage);
            }
        }
    }
}
