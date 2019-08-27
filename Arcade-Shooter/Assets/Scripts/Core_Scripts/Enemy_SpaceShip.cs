using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Enemy_SpaceShip : SpaceShip
{
    float min = -0.06f;
    float max = 0.06f;
    private float Starttime;
    private float ShakeIntensity = 12f;
    private Vector3 ShakePos;
    private bool firsttime = true;
    private Vector2 p0;
    private Vector2 p1;
    private Vector2 p2;
    private Vector2 p3;
    public Transform FinalDestination;
    [HideInInspector] public Transform[] Routes;
    private Vector2 EnemyPosition;
    private Vector3 MainTarget;
    private Transform Target;
    private int RoutesToGo;
    private int i;
    private List<GameObject> BulletList;
    private float Timer;
    private float tparam;
    private float SpeedModifier;
    [HideInInspector] public bool MoveAllowed;
    [HideInInspector] public bool coroutineAllowed;
    private bool check;
    [HideInInspector] public bool ShootAllowed = false;
    private bool ShootBool;
    [SerializeField] private bool Looping;
    [SerializeField] int MaximumAmmo;
    [SerializeField] private GameObject[] RandomItems;

    void Start()
    {
        RoutesToGo = 0;
        tparam = 0;
        SpeedModifier = 0.3f;
        coroutineAllowed = true;
        AttackSpeed = Random.Range(4, 5);
        bullet[0].GetComponent<Bullet>().Damage = Damage;
        BulletList = GameManager.ObjectPooler(bullet[0], MaximumAmmo);
        Body = this.GetComponent<Rigidbody2D>();
        MoveAllowed = true;
        check = false;
    }

    void Update()
    {
        ManageEnemyMovement();
        if (ShootAllowed)
        {
            IdleMovement();
            //     Shoot();
        }
    }

    private IEnumerator GoByTheRoute(int RouteNumber)
    {
        coroutineAllowed = false;
        if (firsttime)
        {
            p0 = Routes[RouteNumber].GetChild(0).position;
            p1 = Routes[RouteNumber].GetChild(1).position;
            p2 = Routes[RouteNumber].GetChild(2).position;
            p3 = Routes[RouteNumber].GetChild(3).position;
        }

        while (tparam < 1)
        {
            tparam += Time.deltaTime * SpeedModifier;
            EnemyPosition = Mathf.Pow(1 - tparam, 3) * p0 +
                            3 * Mathf.Pow(1 - tparam, 2) * tparam * p1 +
                            3 * (1 - tparam) * Mathf.Pow(tparam, 2) * p2 +
                            Mathf.Pow(tparam, 3) * p3;
            Vector3 dir = MainTarget - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //   transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            transform.position = EnemyPosition;
            yield return new WaitForEndOfFrame();
        }

        tparam = 0f;
        RoutesToGo += 1;
        if (RoutesToGo > Routes.Length - 1)
        {
            if (Looping)
            {
                tparam = 0;
                RoutesToGo = 0;
                p0 = Routes[RouteNumber].GetChild(3).position;
                p1 = Routes[RouteNumber].GetChild(2).position;
                p2 = Routes[RouteNumber].GetChild(1).position;
                p3 = Routes[RouteNumber].GetChild(0).position;
                if (firsttime)
                    firsttime = false;
                else
                {
                    firsttime = true;
                }
            }
            else
            {
                RoutesToGo = 0;
                MoveAllowed = false;
            }
        }

        coroutineAllowed = true;
    }

    void ManageEnemyMovement()
    {
        if (MoveAllowed)
        {
            if (coroutineAllowed)
                StartCoroutine(GoByTheRoute(RoutesToGo));
        }
        else
        {
            MainTarget = FinalDestination.position;
            transform.position = Vector2.MoveTowards(transform.position, MainTarget, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, FinalDestination.position) == 0)
            {
                ShootAllowed = true;
            }
        }
        //rotation();
    }

    void IdleMovement()
    {
        transform.position += new Vector3(Mathf.Lerp(min, max, Starttime), Mathf.Lerp(min, max, Starttime), 0);
        Starttime += 0.8f * Time.deltaTime;
        if (Starttime > 1)
        {
            float temp = max;
            max = min;
            min = temp;
            Starttime = 0;
        }
    }

    void Death()
    {
        FindObjectOfType<Game_Director>().Waves[FindObjectOfType<Game_Director>().WaveNumber].EnemyList
            .Remove(this.gameObject);
        int i = Random.Range(0, 100);
        if (SceneManager.GetActiveScene().name != "Random")
        {
            if (i > FindObjectOfType<Game_Director>().ItemDropChance)
            {
                Instantiate(RandomItems[Random.Range(0, RandomItems.Length)], transform.position,
                    Quaternion.identity);
            }
        }
        else
        {
            if (i > 85)
            {
                Instantiate(RandomItems[Random.Range(0, RandomItems.Length)], transform.position,
                    Quaternion.identity); 
            }
        }

        if (SceneManager.GetActiveScene().name!="Random")
        {
            for (int j = 0; j < BulletList.Count; j++)
            {
                if(!BulletList[j].activeInHierarchy)
                    Destroy(BulletList[j]);
            } 
        }

        ParticleSystem temp = ParticleManager._Instance.GetExplosionParticle();
        temp.transform.position = transform.position;
        temp.Play();
        //    ParticleManager._Instance.tempParticle2.transform.position = transform.position;
     //   ParticleManager._Instance.tempParticle2.Play();

        gameObject.SetActive(false);
        tparam = 0;
        GetComponent<Animator>().ResetTrigger("GotHit");
        //Destroy(gameObject);
    }

    public void Shoot()
    {
        // AttackSpeed = Random.Range(10, 15);
        for (int i = 0; i < BulletList.Count; i++)
        {
            if (BulletList[i] != null)
            {
                if (!BulletList[i].activeInHierarchy)
                {
                    for (int j = 0; j < GunPoints.Length; j++)
                    {
                        BulletList[i].transform.position = GunPoints[0].transform.position;
                        BulletList[i].transform.rotation = GunPoints[0].transform.rotation;
                        BulletList[i].GetComponent<TrailRenderer>().Clear();
                        BulletList[i].SetActive(true);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Main_SpaceShip>().health -= 101;
        }
    }

    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Death();
        }
    }
}