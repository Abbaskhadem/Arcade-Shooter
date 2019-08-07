using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy_SpaceShip : SpaceShip
{
    public Transform FinalDestination;
    [HideInInspector]public Transform[] Routes;
    private Vector2 EnemyPosition;
    private Vector3 MainTarget;
    private Transform Target;
    private int RoutesToGo;
    private int i;
    private List<GameObject> BulletList;
    private float Timer;
    private float tparam;
    private float SpeedModifier;
    private bool MoveAllowed;
    private bool coroutineAllowed;
    private bool check;
    private bool ShootAllowed = false;
    private bool ShootBool;
    [SerializeField]int MaximumAmmo;
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
            Shoot();
        }
  
    }
    private IEnumerator GoByTheRoute(int RouteNumber)
    {
        coroutineAllowed = false;
        Vector2 p0 = Routes[RouteNumber].GetChild(0).position;
        Vector2 p1 = Routes[RouteNumber].GetChild(1).position;
        Vector2 p2 = Routes[RouteNumber].GetChild(2).position;
        Vector2 p3 = Routes[RouteNumber].GetChild(3).position;
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
            RoutesToGo = 0;
            MoveAllowed = false;        
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
    void Death()
    {
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        AttackSpeed = Random.Range(10, 15);
        for (int i = 0; i < BulletList.Count; i++)
        {
            if (BulletList[i] != null)
            {
                if (!BulletList[i].activeInHierarchy)
                {
                    Timer += Time.deltaTime;
                    if (Timer>=AttackSpeed)
                    {
                        Timer = 0;
                        for (int j = 0; j <GunPoints.Length; j++)
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
    }

    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health<=0)
        {
            Death();
        }
    }
}
