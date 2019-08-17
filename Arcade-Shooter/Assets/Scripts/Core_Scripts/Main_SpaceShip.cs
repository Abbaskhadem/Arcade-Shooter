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

    float min =- 0.0022f;
    float max = 0.0022f;
    private float Starttime;
    private float SuperPowerTimer;
    private List<GameObject> BulletList;
    private bool moveAllowed;
    private float deltaX;
    private float deltaY;
    private float Timer;
    [SerializeField] private int MaximumBullets;
    private int MaxBullets;
    private Vector2 ScreenBounds;

    #endregion

    #region List Preparing

    void Start()
    {
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
            if (health<=100)
            {
                float temp;
                temp = Mathf.Round(health * 100f) / 100f;
                PowerBar.GetComponentInChildren<Text>().text=temp+"%";
                PowerBar.value = health;   
            }
            else
            {
                PowerBar.GetComponentInChildren<Text>().text=100+"%";
            }
            if (health>=100)
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
            Shoot();
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
                            BulletList[i].transform.position = GunPoints[j].transform.position;
                            BulletList[i].transform.rotation = GunPoints[j].transform.rotation;
                            BulletList[i].GetComponentInChildren<TrailRenderer>().Clear();
                            BulletList[i].SetActive(true);
                        }
                    }
                }
            }
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
