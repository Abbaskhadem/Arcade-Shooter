using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemies : MonoBehaviour
{
    public int MovementType;
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
    bool Ranged_Melee;
    [SerializeField]
    float Speed;
    [SerializeField]
    Transform Target;
    Vector3 MainTarget;
    [SerializeField]
    float Distance;
    [SerializeField]
    bool Leader;
    [SerializeField]
    private Transform[] routes;
    [SerializeField]
    Vector2[] LeaderVector2;
    int i = 0;
    public GameObject startWeapon;
    bool check;    
    private int RouteToGo;
    public List<GameObject> activePlayerTurrets;
    private float tparam;
    private Vector2 Enemyposition;
    private float SpeedModifier;
    private bool coroutineAllowed;
    private bool MoveAllowed;
    void Start()
    {

        activePlayerTurrets = new List<GameObject>();
        activePlayerTurrets.Add(startWeapon);
        RouteToGo = 0;
        tparam = 0;
        SpeedModifier = 0.3f;
        coroutineAllowed = true;
        check = false;
        MoveAllowed = true;
    }
    void Update()
    {
        ManageEnemyMovement();
        shoot();
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
        foreach (GameObject turret in activePlayerTurrets)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet");
            if (bullet != null)
            {
                FireRate += Time.deltaTime;
                if (FireRate > 0.2)
                {
                    FireRate = 0;
                    bullet.transform.position = turret.transform.position;
                    bullet.transform.rotation = turret.transform.rotation;
                    bullet.SetActive(true);
                }
            }
        }



        // DelayShoot += Time.deltaTime;
        // if (DelayShoot > FireRate)
        // {
        //     DelayShoot = 0;
        //     Instantiate(Bullet, Gun.position, Quaternion.identity);
        // }
    }
    void ManageEnemyMovement()
    {
       // if (Leader == false && Vector2.Distance(transform.position, Target.position) > Distance)
       // {
       //     //if (coroutineAllowed)
       //     //{
       //     //    StartCoroutine(GoByTheRoute(RouteToGo));
       //     //}
       //     Vector3 dir = Target.position - transform.position;
       //     float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
       //     transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
       //     transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
       // }
       // else
       // {
       //     if (MoveAllowed)
       //     {
       //         if (coroutineAllowed)
       //             StartCoroutine(GoByTheRoute(RouteToGo));
       //     }
       //     else
       //     {
       //         MainTarget = GameManager.GG(LeaderVector2[i].x, LeaderVector2[i].y);
       //         transform.position = Vector2.MoveTowards(transform.position, MainTarget, Speed * Time.deltaTime);
       //         if (i < LeaderVector2.Length)
       //         {
       //             MainTarget = GameManager.GG(LeaderVector2[i].x, LeaderVector2[i].y);
       //             transform.position = Vector2.MoveTowards(transform.position, MainTarget, Speed * Time.deltaTime);
       //             if (Vector2.Distance(transform.position, MainTarget) <= 0.01 && check == false)
       //             {
       //                 i++;
       //                 if (i == LeaderVector2.Length - 1)
       //                 {
       //                     check = true;
       //                 }
       //
       //             }
       //         }
       //
       //     }
       //
       //     //rotation();
       // }
    }
    // movement Enemy with deffult Strakcher
    private IEnumerator GoByTheRoute(int RouteNumber)
    {
        coroutineAllowed = false;
        Vector2 p0 = routes[RouteNumber].GetChild(0).position;
        Vector2 p1 = routes[RouteNumber].GetChild(1).position;
        Vector2 p2 = routes[RouteNumber].GetChild(2).position;
        Vector2 p3 = routes[RouteNumber].GetChild(3).position;
        while (tparam < 1)
        {
            tparam += Time.deltaTime * SpeedModifier;
            Enemyposition = Mathf.Pow(1 - tparam, 3) * p0 +
            3 * Mathf.Pow(1 - tparam, 2) * tparam * p1 +
            3 * (1 - tparam) * Mathf.Pow(tparam, 2) * p2 +
            Mathf.Pow(tparam, 3) * p3;
            Vector3 dir = MainTarget - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            transform.position = Enemyposition;
            yield return new WaitForEndOfFrame();
        }
        tparam = 0f;
        RouteToGo += 1;
        if (RouteToGo > routes.Length - 1)
        {
            RouteToGo = 0;
            MoveAllowed = false;
        }
        coroutineAllowed = true;
    }
    //rotation and fallow Gride Position
    //void rotation()
    //{
    //    MainTarget = GameManager.GG(LeaderVector2[i].x, LeaderVector2[i].y);
    //    Vector3 dir = MainTarget - transform.position;
    //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    //    transform.position = Vector2.MoveTowards(transform.position, MainTarget, Speed * Time.deltaTime);
    //}

    // liitel diffrent main IEnumerator
    //private IEnumerator GoByTheRoute1(int RouteNumber)
    //{

    //        coroutineAllowed = false;
    //        Vector2 p0 = routes[RouteNumber].GetChild(0).position;
    //        Vector2 p1 = routes[RouteNumber].GetChild(1).position;
    //        Vector2 p2 = routes[RouteNumber].GetChild(2).position;
    //        Vector2 p3 = routes[RouteNumber].GetChild(3).position;
    //        while (tparam < 1)
    //        {
    //            tparam += Time.deltaTime * SpeedModifier;
    //            Enemyposition = Mathf.Pow(1 - tparam, 3) * p0 +
    //                3 * Mathf.Pow(1 - tparam, 2) * tparam * p1 +
    //                3 * (1 - tparam) * Mathf.Pow(tparam, 2) * p2 +
    //                Mathf.Pow(tparam, 3) * p3;
    //            Vector3 dir = MainTarget - transform.position;
    //            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    //        if (Vector2.Distance(transform.position, Target.position) > Distance)
    //        {
    //            transform.position = Enemyposition;

    //            yield return new WaitForEndOfFrame();

    //        }
    //        tparam = 0f;
    //        RouteToGo += 1;
    //        if (RouteToGo > routes.Length - 1)
    //            RouteToGo = 0;

    //        coroutineAllowed = true;
    //        // MoveAllowed = false;
    //    }
    //}

    //for update
    //switch (MovementType)
    //{
    //    case 1:
    //        EnemyAnimMovement();
    //        break;
    //    case 2:
    //        EnemyRotateArea();
    //        break;
    //    default:
    //        break;
    //}
}