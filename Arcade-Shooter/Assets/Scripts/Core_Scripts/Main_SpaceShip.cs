using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_SpaceShip : SpaceShip
{
    #region UIElements

    [SerializeField] private Slider PowerBar;

    #endregion

    #region Exclusive Variables

    public float scatterShotTurretReloadTime = 2.0f;
    float min = -0.0022f;
    float max = 0.0022f;
    private float Starttime;
    private float SuperPowerTimer;
    private List<GameObject> BulletList;
    private bool moveAllowed;
    private float deltaX;
    private float deltaY;
    public float Timer;
    public float Timer1;
    [SerializeField] private int MaximumBullets;
    private int MaxBullets;
    private Vector2 ScreenBounds;

    public int upgradeState = 0;
    public GameObject startWeapon;
    public List<GameObject> tripleShotTurrets; //
    public List<GameObject> scatterShotTurrets;
    public List<GameObject> wideShotTurrets;
    public List<GameObject> activePlayerTurrets;
    

    #endregion

    #region List Preparing

    void Start()
    {
        upgradeState = 0;
        activePlayerTurrets = new List<GameObject>();
        activePlayerTurrets.Add(startWeapon);
        bullet[PlayerPrefs.GetInt("GunIndex")].GetComponentInChildren<Bullet>().Damage = Damage;
        BulletList = GameManager.ObjectPooler(bullet[PlayerPrefs.GetInt("GunIndex")], MaximumBullets);
        Body = this.GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Actions

    void Update()
    {
        if (!GameManager._Instance.GameEnded)
        {
            if (health <= 100)
            {
                float temp;
                temp = Mathf.Round(health * 100f) / 100f;
                PowerBar.GetComponentInChildren<Text>().text = temp + "%";
                PowerBar.value = health;
            }
            else
            {
                PowerBar.GetComponentInChildren<Text>().text = 100 + "%";
            }

            if (health >= 100)
            {
                Debug.Log("TIMER STARTED!");
                SuperPowerTimer += Time.deltaTime;
                if (SuperPowerTimer >= Random.Range(3f, 5f))
                {
                    SuperPowerTimer = 0;
                    health = 50;
                    Debug.Log("ACTIVE SUPERPOWER!");
                }
            }

            Movement();
            Timer += Time.deltaTime;
            if (Timer >= AttackSpeed)
            {
                Timer = 0;
                Shoot();
            }

            Timer1 += Time.deltaTime;

            if (Timer1 >= 5)
            {
                Timer1 = -100000;
                UpgradeWeapons();
            }
        }

        if (!moveAllowed)
        {
            IdleMovement();
        }
    }

    #endregion

    #region Player Ability

    void IdleMovement()
    {
        transform.position += new Vector3(Mathf.Lerp(min, max, Starttime), 0, 0);
        Starttime += 1.2f * Time.deltaTime;
        if (Starttime > 1)
        {
            float temp = max;
            max = min;
            min = temp;
            Starttime = 0;
        }
    }

    void Movement()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount >= 2)
            {
                moveAllowed = false;
            }

            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    Body.freezeRotation = true;
                    Body.velocity = new Vector2(0, 0);
                    Body.gravityScale = 0;
                    moveAllowed = true;
                    break;
                case TouchPhase.Moved:
                    if (moveAllowed)
                        Body.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    break;
                case TouchPhase.Ended:
                    moveAllowed = false;
                    Body.freezeRotation = false;
                    Body.velocity = Vector2.zero;
                    break;
            }
        }
    }

    void Shoot()
    {
        foreach (GameObject turret in activePlayerTurrets)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet");

            if (bullet != null)
            {
                bullet.transform.position = turret.transform.position;
                bullet.transform.rotation = turret.transform.rotation;
                bullet.GetComponent<TrailRenderer>().Clear();
                bullet.SetActive(true);
            }
        }

        // BulletList = GameManager.ObjectPooler(bullet[PlayerPrefs.GetInt("GunIndex")], MaximumBullets);
        // for (int i = 0; i < BulletList.Count; i++)
        // {
        //     //BulletList = GameManager.ObjectPooler(bullet[PlayerPrefs.GetInt("GunIndex")], MaximumBullets);
        //     if (BulletList[i] != null)
        //     {
        //         if (!BulletList[i].activeInHierarchy)
        //         {
        //             Timer += Time.deltaTime;
        //             if (Timer >= AttackSpeed)
        //             {
        //                 Timer = 0;
        //                 //for (int j = 0; j < GunPoints.Length; j++)
        //                // 
        //                 foreach (GameObject turret in activePlayerTurrets)
        //                 {
        //                     BulletList[i].transform.position = turret.transform.position;
        //                     BulletList[i].transform.rotation = turret.transform.rotation;
        //                     BulletList[i].SetActive(true);
        //                    // BulletList[i + 1].transform.position = tripleShotTurrets[1].transform.position;
        //                    // BulletList[i + 1].transform.rotation = tripleShotTurrets[1].transform.rotation;
        //                    // BulletList[i+1].SetActive(true);
        //                 }
        //               
        //             }
        //         }
        //     }
        // }
    }

    public void UpgradeWeapons()
    {
        if (upgradeState == 0)
        {
            foreach (GameObject turret in tripleShotTurrets)
            {
                activePlayerTurrets.Add(turret);
            }
        }
        else if (upgradeState == 1)
        {
            foreach (GameObject turret in wideShotTurrets)
            {
                activePlayerTurrets.Add(turret);
            }
        }
        else if (upgradeState == 2)
        {
            StartCoroutine("ActivateScatterShotTurret");
        }
        else
        {
            return;
        }

        upgradeState++;
    }

    IEnumerator ActivateScatterShotTurret()
    {
        // The ScatterShot turret is shot independantly of the spacebar
        // This Coroutine shoots the scatteshot at a reload interval

        while (true)
        {
            foreach (GameObject turret in scatterShotTurrets)
            {
                for (int i = 0; i < BulletList.Count; i++)
                {
                    if (BulletList[i] != null)
                    {
                        BulletList[i].transform.position = turret.transform.position;
                        BulletList[i].transform.rotation = turret.transform.rotation;
                        BulletList[i].SetActive(true);
                    }
                }
            }

            yield return new WaitForSeconds(scatterShotTurretReloadTime);
        }
    }

    public void TakeDamage()
    {
        health -= 50;
        if (health < 0)
        {
            GameManager.GameLost = true;
            gameObject.SetActive(false);
        }
    }

    #endregion
}