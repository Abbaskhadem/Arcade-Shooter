using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainJet : MonoBehaviour
{
    Rigidbody2D rd;
    float deltaX, deltaY;
    bool moveAllowed = false;
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
            if(Input.touchCount >= 2)
            {
                moveAllowed = false;
            }
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //  if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    rd.freezeRotation = true;
                    rd.velocity = new Vector2(0, 0);
                    rd.gravityScale = 0;
                  //  GetComponent<BoxCollider2D>().sharedMaterial = null;
                    moveAllowed = true;

                    break;
                case TouchPhase.Moved:
                     //if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                     if(moveAllowed)
                    rd.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    break;
                case TouchPhase.Ended:
                    moveAllowed = false;
                  //  rd.freezeRotation = false;
                    rd.velocity = Vector2.zero;
                    PhysicsMaterial2D mat = new PhysicsMaterial2D();
                  //  mat.friction = 0.4f;
                   // GetComponent<BoxCollider2D>().sharedMaterial = mat;
                    break;
            }
        }
    }
}
