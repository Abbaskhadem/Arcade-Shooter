using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SpaceShip : SpaceShip
{
    [HideInInspector] public Vector2 FinalDestination;
    [SerializeField] private Transform[] Routes;
    private int RoutesToGo;
    private List<GameObject> Bullets;
    private float Timer;
    [SerializeField]int MaximumAmmo;
    void Start()
    {
        Bullets = GameManager.ObjectPooler(bullet[0], MaximumAmmo);
        Body = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Shoot();
    }

    void Death()
    {
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        for (int i = 0; i < Bullets.Count; i++)
        {
            if (Bullets[i] != null)
            {
                if (!Bullets[i].activeInHierarchy)
                {
                    Timer += Time.deltaTime;
                    if (Timer >= AttackSpeed)
                    {
                        Timer = 0;
                        for (int j = 0; j <GunPoints.Length; j++)
                        {
                            Bullets[i].transform.position = GunPoints[j].transform.position;
                            Bullets[i].transform.rotation = GunPoints[j].transform.rotation;
                            Bullets[i].SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
