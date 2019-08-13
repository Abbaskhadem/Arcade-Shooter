using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_SpaceShip : SpaceShip
{
    #region Exclusive Variables

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
        Movement();
        Shoot();
    }

//    void LateUpdate()
//         {
//             Vector3 viewPos = transform.position;
//             viewPos.x = Mathf.Clamp(viewPos.x, ScreenBounds.x, ScreenBounds.x * -1);
//             viewPos.y = Mathf.Clamp(viewPos.y, ScreenBounds.y, ScreenBounds.y * -1);
//             transform.position = viewPos;
//         }

    #endregion
    #region Player Ability
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

    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            GameManager.GameLost = true;
            gameObject.SetActive(false);
            Debug.Log("You Lost!");
        }
    }
    #endregion
}
