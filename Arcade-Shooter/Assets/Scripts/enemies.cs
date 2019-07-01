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
    int Speed;
    [SerializeField]
    Transform Target;
    Vector3 MainTarget;
    [SerializeField]
    float Distance;
    [SerializeField]
    bool Leader;
    [SerializeField]
    Vector2[] LeaderVector2;
    GameManager GM;
    int i = 0;
    bool check;
    void Start()
    {

        check = false;
    }
    void Update()
    {
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
        EnemyRotateArea();
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
        DelayShoot += Time.deltaTime;
        if (DelayShoot > FireRate)
        {
            DelayShoot = 0;
            Instantiate(Bullet, Gun.position, Quaternion.identity);
        }
    }
    void EnemyRotateArea()
    {
        if (Leader == false && Vector2.Distance(transform.position, Target.position) > Distance)
        {
            Vector3 dir = Target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        else
        {
            if (i < LeaderVector2.Length)
            {
                rotation();

                if (Vector2.Distance(transform.position, MainTarget) <= 0.01 && check == false)
                {
                    rotation();
                    i++;
                    if (i == LeaderVector2.Length)
                    {
                        check = true;
                    }
                }
            }
        }
    }
    void EnemyAnimMovement()
    {

    }
    void rotation()
    {
        MainTarget = GameManager.GG(LeaderVector2[i].x, LeaderVector2[i].y);
        Vector3 dir = MainTarget - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.position = Vector2.MoveTowards(transform.position, MainTarget, Speed * Time.deltaTime);
    }
}