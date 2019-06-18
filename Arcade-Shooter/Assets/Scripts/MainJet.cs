using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainJet : MonoBehaviour
{
    Rigidbody2D rd;
    float deltaX, deltaY;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        PhysicsMaterial2D mat = new PhysicsMaterial2D();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        MovmentJet();
    }

    void MovmentJet()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;
                        rd.gravityScale = 0;
                    }
                    break;
                case TouchPhase.Moved:
                    //  if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    rd.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    break;
                case TouchPhase.Ended:                  
                    rd.freezeRotation = false;
                    PhysicsMaterial2D mat = new PhysicsMaterial2D();                   
                    break;
            }
        }
    }
}
