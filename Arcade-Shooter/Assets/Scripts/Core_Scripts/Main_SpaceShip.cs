using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_SpaceShip : SpaceShip
{
    #region UIElements

    [SerializeField] private GameObject LoseUI;
    [SerializeField] private Slider PowerBar;
    [SerializeField] private GameObject DamageEffect;

    #endregion

    #region Exclusive Variables

    [SerializeField] private ParticleSystem[] Jetpack;
    [SerializeField] private ParticleSystem DeathEffect;
    [SerializeField] private ParticleSystem SuperPower;
    [HideInInspector] public float TimeShild;
    [HideInInspector]public bool Shild;
    float min = -0.0022f;
    float max = 0.0022f;
    private int DamageCost = 10;
    private float Starttime;
    private float SuperPowerTimer;
    private List<GameObject> BulletList;
    private bool moveAllowed;
    private float deltaX;
    private float deltaY;
    [HideInInspector] public float Timer;
    private int MaxBullets;
    private Vector2 ScreenBounds;
    public GameObject startWeapon;
    private int index;
    [SerializeField] private GameObject PowerText;
    [SerializeField] List<GameObject> UpgradableTurrets;
    [HideInInspector] public List<GameObject> activePlayerTurrets;
    private bool damagedone = false;

    #endregion

    #region List Preparing

    void Awake()
    {
      //  GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet");
    }

    void Start()
    {
      //  PowerUpTextController.instance.Creat("Power Up", transform.position);
        activePlayerTurrets = new List<GameObject>();
        activePlayerTurrets.Add(startWeapon);
//        bullet[PlayerPrefs.GetInt("GunIndex")].GetComponentInChildren<Bullet>().Damage = Damage;
        Body = this.GetComponent<Rigidbody2D>();
       // Shoot();
    }

    #endregion

    #region Actions

    void Update()
    {
        
        if (!GameManager._Instance.GamePause)
        {
            if (SuperPower.isPlaying)
            {
                SuperPower.transform.parent = GameObject.Find("Main Camera").transform;
            }
            else
            {
                SuperPower.transform.parent = this.transform;
                SuperPower.transform.position = this.transform.position;
            }
            #region PC Buttons
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Translate(-1f,0,0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                
               transform.Translate(1f,0,0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Translate(0,1,0);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
               transform.Translate(0,-1,0);
            }
            #endregion
            if (health < 0)
            {
                LoseUI.SetActive(true);
                PowerBar.GetComponentInChildren<Text>().text = Farsi.multiLanguageText("FAILED!", "نا موفق!");
                GameManager._Instance.GameEnded = true;
                Instantiate(DeathEffect, gameObject.transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
            if (!GameManager._Instance.GameEnded)
            {
                if (health <= 100)
                {
                    float  temp = Mathf.Round(health * 100f) / 100f;
                    PowerBar.GetComponentInChildren<Text>().text = temp.ToString("0") + "%";
                    PowerBar.value = health;
                }
                else if (health >= 100)
                {
                    PowerBar.GetComponentInChildren<Text>().text = 100 + "%";
                }

                if (health >= 100)
                {
                    // Timer Starts
                    SuperPowerTimer += Time.deltaTime;
                    if (SuperPowerTimer >= Random.Range(3f, 5f))
                    {
                        if (SceneManager.GetActiveScene().name != "Random")
                        {
                            if (!FindObjectOfType<Game_Director>().SpawnAllowed)
                            {
                                foreach (var VARIABLE in FindObjectsOfType<Enemy_SpaceShip>())
                                {
                                    VARIABLE.TakeDamage(50);
                                }
                                SuperPowerTimer = 0;
                                health = 50;
                                SuperPower.Play();
                            }
                        }
                        else
                        {
                            if (!FindObjectOfType<Random_Director>().SpawnAllowed )
                            {
                                foreach (var VARIABLE in FindObjectsOfType<Enemy_SpaceShip>())
                                {
                                    VARIABLE.TakeDamage(50);
                                }
                                SuperPowerTimer = 0;
                                health = 50;
                                SuperPower.Play();
                            }
                        }
                        // Super Power Activates!
                    }
                }

                Movement();
                Timer += Time.deltaTime;
                if (Timer >= AttackSpeed)
                {
                    Timer = 0;
                    Shoot();
                }
            }

            if (!moveAllowed)
            {
                IdleMovement();
            } 
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
                    for (int i = 0; i < Jetpack.Length; i++)
                    {
                        Jetpack[i].Play();
                    }

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
                    for (int i = 0; i < Jetpack.Length; i++)
                    {
                        Jetpack[i].Stop();
                    }

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
    }

    public void UpgradeWeapons()
    {
        if (index < UpgradableTurrets.Count)
        {
          //  Instantiate(PowerText, transform.position,Quaternion.identity);
            PowerUpTextController.Instance.Creat("Power Up", transform.position);
            activePlayerTurrets.Add(UpgradableTurrets[index++]);
            if (index % 2 == 1)
            {
                activePlayerTurrets.Remove(startWeapon);
                activePlayerTurrets.Add(UpgradableTurrets[index]);
            }
            else
            {
                activePlayerTurrets.Remove(UpgradableTurrets[--index]);
                index++;
                activePlayerTurrets.Add(startWeapon);
            }

            foreach (var a in ObjectPooler.SharedInstance.pooledObjects)
            {
                a.GetComponent<Bullet>().Damage -= DamageCost;
            }

            DamageCost -= 4;
        }
        else
        {
            AttackSpeed -= 0.02f;
        }
    }

    public void TakeDamage()
    {
        if (!Shild)
        {
            if (health >= 50)
            {
                DamageEffect.SetActive(true);  
            }
            health -= 50;
        }
    }

    #endregion

    #region HistoryCodes

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

//    IEnumerator ActivateScatterShotTurret()
//    {
//        // The ScatterShot turret is shot independantly of the spacebar
//        // This Coroutine shoots the scatteshot at a reload interval
//
//        while (true)
//        {
//            foreach (GameObject turret in scatterShotTurrets)
//            {
//                for (int i = 0; i < BulletList.Count; i++)
//                {
//                    if (BulletList[i] != null)
//                    {
//                        BulletList[i].transform.position = turret.transform.position;
//                        BulletList[i].transform.rotation = turret.transform.rotation;
//                        BulletList[i].SetActive(true);
//                    }
//                }
//            }
//            yield return new WaitForSeconds(scatterShotTurretReloadTime);
//        }
//    }

    #endregion
}