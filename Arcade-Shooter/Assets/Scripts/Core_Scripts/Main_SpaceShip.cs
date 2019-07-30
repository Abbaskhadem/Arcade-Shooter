using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_SpaceShip : SpaceShip
{
    private List<GameObject> BulletList;
    private bool  moveAllowed;
    private float deltaX;
    private float deltaY;
    private float Timer;
    [SerializeField]
    private int MaximumBullets;
    private int MaxBullets;
    
    
    void Start()
    {
        BulletList = GameManager.ObjectPooler(bullet[PlayerPrefs.GetInt("GunIndex")], MaximumBullets);
        Body = this.GetComponent<Rigidbody2D>();
    }
   void Update()
    {
     //   Movement();
     Shoot();
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
        for (int i = 0; i <BulletList.Count; i++)
        {
            if (BulletList[i]!=null)
            {
                if (!BulletList[i].activeInHierarchy )
                {
                    Timer += Time.deltaTime;
                    if (Timer>=AttackSpeed)
                    {
                        Timer = 0;
                        BulletList[i].transform.position = GunPoints[0].position;
                        Rigidbody2D Body = BulletList[i].GetComponent<Rigidbody2D>();
                        BulletList[i].SetActive(true);
                        Body.velocity = transform.up * 3;
                        break;
                    }
                }  
            }
        }
    }
}
