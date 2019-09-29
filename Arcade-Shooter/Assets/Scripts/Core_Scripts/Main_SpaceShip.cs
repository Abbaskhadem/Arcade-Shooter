using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Main_SpaceShip : SpaceShip
{
    #region UIElements

    [SerializeField] private GameObject LoseUI;
    [SerializeField] private Slider PowerBar;
    [SerializeField] private GameObject DamageEffect;

    #endregion

    #region Exclusive Variables
    [SerializeField] private GameObject Canvas;
    [SerializeField] private AnimationCurve MoveOrto;
    [SerializeField] private GameObject  GameDirector;
    [SerializeField] private GameObject  ScreenBoundery;
    [SerializeField] private ParticleSystem[] Jetpack;
    [SerializeField] private ParticleSystem DeathEffect;
    [SerializeField] private ParticleSystem SuperPower;
    [HideInInspector] public float TimeShild;
    [HideInInspector]public bool Shild;
    float min = -0.0038f;
    float max = 0.0038f;
    private int DamageCost = 10;
    private float Starttime;
    private float SuperPowerTimer;
    private List<GameObject> BulletList;
    private bool moveAllowed;
    private float deltaX;
    private float deltaY;
    [HideInInspector] public float Timer;
    [SerializeField] private GameObject Rocket;
    private int MaxBullets;
    private Vector2 ScreenBounds;
    public GameObject startWeapon;
    private int index;
    [SerializeField] List<GameObject> UpgradableTurrets;
    [HideInInspector] public List<GameObject> activePlayerTurrets;
    private bool IntroMode = true;
    private bool CenterPosition = false;
    void Start()
    {
        activePlayerTurrets = new List<GameObject>();
        activePlayerTurrets.Add(startWeapon);
        Body = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Actions
    void Update()
    {
        if (IntroMode)
        {
            Intro();
        }
        if (!GameManager._Instance.GamePause && !IntroMode && !GameManager._Instance.GameEnded)
        {
            transform.position=new Vector3(Mathf.Clamp(transform.position.x,-3f,3f),Mathf.Clamp(transform.position.y,-5f,5f),transform.position.z);
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

            if (Input.GetKey(KeyCode.Space))
            {
                if (health >= 100)
                {
                    foreach (var VARIABLE in FindObjectsOfType<Enemy_SpaceShip>())
                    {
                        VARIABLE.TakeDamage(50);
                    }
                    SuperPowerTimer = 0;
                    health = 50;
                    SuperPower.Play();
                    Camera.main.GetComponent<RipplePost>().enabled = true;
                    Camera.main.GetComponent<RipplePost>().RippleEffect(transform);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position+=new Vector3(-0.08f,0,0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                
                transform.position+=new Vector3(0.08f,0,0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position+=new Vector3(0f,0.08f,0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position+=new Vector3(0f,-0.08f,0);
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

       

                // Timer Starts
                   // if (SuperPowerTimer >= Random.Range(3f, 5f))
                  //  {
                  //    if (SceneManager.GetActiveScene().name != "Random")
                      //  {
                          //  if (SceneManager.GetActiveScene().name != "RandomGenerator")
                          //  {
                                //if (!FindObjectOfType<Game_Director>().SpawnAllowed)
                             //   {
                             
                            //    }
                           // }
                      //  }
                     //   else
                     //   {
                        //    if (!FindObjectOfType<Random_Director>().SpawnAllowed )
                        //    {
                         //       foreach (var VARIABLE in FindObjectsOfType<Enemy_SpaceShip>())
                         //       {
                          //          VARIABLE.TakeDamage(50);
                           //     }
                           //     SuperPowerTimer = 0;
                           //     health = 50;
                          //      SuperPower.Play();
                          //  }
                     //   }
                        // Super Power Activates!
                 //   }
            //    }

                Movement();
                Timer += Time.deltaTime;
                if (Timer >= AttackSpeed)
                {
                    Timer = 0;
                    if(!GameManager._Instance.GameEnded)
                      Shoot();
                }
            }

            if (health >= 100)
            {
                ActiveSuperPower();
            }
            if (!moveAllowed)
            {
                IdleMovement();
            } 
        }

        if (GameManager._Instance.GameEnded)
        {
            OutThrough();
        }
    }

    void Intro()
    {
        for (int i = 0; i < Jetpack.Length; i++)
        {
            Jetpack[i].GetComponent<ParticleSystemRenderer>().sortingOrder = -2;
        }
        GetComponent<SpriteRenderer>().sortingOrder = -2;
        IdleMovement();
        foreach (var VARIABLE in FindObjectsOfType<Enviroment>())
        {
            if(VARIABLE.Speed>-0.09)
                VARIABLE.Speed +=-0.09f;
        }

        if (Camera.main.orthographicSize < 5)
            Camera.main.orthographicSize += MoveOrto.Evaluate(Time.deltaTime);
        if(Camera.main.transform.position.y<0)
          Camera.main.transform.Translate(0,0.03f,0);
        else if(Camera.main.transform.position.y>=0 && Camera.main.orthographicSize>=5)
        {
            Canvas.SetActive(true);
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            Time.timeScale = 1;
            ScreenBoundery.SetActive(true);
            foreach (var VARIABLE in FindObjectsOfType<Enviroment>())
            {
                VARIABLE.Speed -=-0.09f;
            } 
            for (int i = 0; i < Jetpack.Length; i++)
            {
                 Jetpack[i].Stop();
                 Jetpack[i].GetComponent<ParticleSystemRenderer>().sortingOrder = 0; 
            }
            GameDirector.SetActive(true);
            IntroMode = false;

        }
    }

    void OutThrough()
    {
        if (Vector2.Distance(transform.position, new Vector2(0, 0)) != 0 && !CenterPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), Speed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, new Vector2(0, 0))==0 || CenterPosition)
        {
            if (!CenterPosition)
            {
                GetComponentInChildren<TrailRenderer>().enabled = true;
                Camera.main.GetComponent<RipplePost>().enabled = true;
                Camera.main.GetComponent<RipplePost>().RippleEffect(transform);  
                CenterPosition = true;
            }
            Body.AddForce(new Vector2(0,20));
        }
    }

    #endregion

    #region Player Ability

    void IdleMovement()
    {
        transform.position += new Vector3(Mathf.Lerp(min, max, Starttime), 0, 0);
        Starttime += 1.4f * Time.deltaTime;
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
                GetComponent<AudioSource>().Play();
                turret.GetComponentInChildren<ParticleSystem>().Play();
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
            PowerUpTextController.Instance.Creat("Power Up", transform.position);
            activePlayerTurrets.Add(UpgradableTurrets[index++]);
            if (index % 2 == 1)
            {
                activePlayerTurrets.Remove(startWeapon);
               startWeapon.SetActive(false);
                activePlayerTurrets.Add(UpgradableTurrets[index]);
            }
            else
            {
                activePlayerTurrets.Remove(UpgradableTurrets[--index]);
                index++;
                activePlayerTurrets.Add(startWeapon);
                startWeapon.SetActive(true);
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

    void ActiveSuperPower()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.tapCount == 2)
            {
                foreach (var VARIABLE in FindObjectsOfType<Enemy_SpaceShip>())
                {
                    VARIABLE.TakeDamage(50);
                }
                SuperPowerTimer = 0;
                health = 50;
                SuperPower.Play();
                Camera.main.GetComponent<RipplePost>().enabled = true;
                Camera.main.GetComponent<RipplePost>().RippleEffect(transform);
            }
        }
    }
    public void LaunchRocket()
    {
        StartCoroutine(LaunchingRockets());
    }

    private IEnumerator LaunchingRockets()
    {
        for (int i = 0; i < Random.Range(4,6); i++)
        {
            Instantiate(Rocket, new Vector2(transform.position.x-Random.Range(0,0.02f),transform.position.y-Random.Range(-0.02f,0.02f)), Quaternion.identity);
            yield return new  WaitForSeconds(0.2f);
        }
        yield return null;
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
}