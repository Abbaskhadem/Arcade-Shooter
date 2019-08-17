using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainJet : MonoBehaviour
{
    public int HP;
    [SerializeField]
    Transform[] Gun;
    [SerializeField]
    GameObject[] Bullet;
    Rigidbody2D rd;
    float deltaX, deltaY;
    float FireRate;
    bool moveAllowed = false;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Shoot();
        MovmentJet();
    }

    void MovmentJet()
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
                    rd.freezeRotation = true;
                    rd.velocity = new Vector2(0, 0);
                    rd.gravityScale = 0;
                    moveAllowed = true;
                    break;
                case TouchPhase.Moved:
                    if (moveAllowed)
                        rd.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    break;
                case TouchPhase.Ended:
                    moveAllowed = false;
                    rd.freezeRotation = false;
                    rd.velocity = Vector2.zero;
                    break;
            }
        }
    }
    public void Shoot()
    {
        FireRate += Time.deltaTime;
        if (FireRate > 0.2)
        {
            FireRate = 0;
            for (int i = 0; i < Gun.Length; i++)
            {
                Instantiate(Bullet[i], Gun[i].position, Quaternion.identity);
            }
        }

    }
    public void TakeDamage(int Damage)
    {
        HP -= Damage;
        if (HP <= 0)
        {
         //   GameManager.LoseGame();
        }
    }
    
}
