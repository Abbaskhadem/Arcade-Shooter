using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public int Damage;
    [SerializeField] float Speed;
    private Rigidbody2D objectRigidbody;

    void Start()
    {
     
    }

    void Update()
    {
        if (!gameObject.activeSelf)
            this.GetComponent<TrailRenderer>().enabled = false;
        objectRigidbody = transform.GetComponent<Rigidbody2D>();
        objectRigidbody.velocity = transform.up * Speed;
    }
//    void OnEnable()
//    {
//        Invoke("hideBullet",4f);
//        objectRigidbody = transform.GetComponent<Rigidbody2D>();
//        objectRigidbody.velocity = transform.up * Speed;
//    }
//    void hideBullet()
//    {
//        gameObject.SetActive(false);
//    }
//    void OnDisable()
//    {
//        CancelInvoke();
//    }

}
