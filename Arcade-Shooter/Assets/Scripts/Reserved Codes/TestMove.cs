using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private GameObject What;
    Rigidbody2D Something;
    [SerializeField]
    Vector2 Target;
    float Speed = 3f;
    void Start()
    {
        Something = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.x,Target.y), Time.deltaTime * Speed);
    }
}
