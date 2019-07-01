using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupCods : MonoBehaviour
{
    [SerializeField]
    float smoothTimeX;
    [SerializeField]
    float smoothTimeY;
    Vector2 Velocity;
    Vector3 MainTarget;
    // Start is called before the first frame update
    void Start()
    {
        MainTarget = new Vector2();

    }

    // Update is called once per frame
    void Update()
    {
       
        float PosX = Mathf.SmoothDamp(transform.position.x, MainTarget.x, ref Velocity.x, smoothTimeX);
        float PosY = Mathf.SmoothDamp(transform.position.y, MainTarget.y, ref Velocity.y, smoothTimeY);
        transform.position = new Vector3(PosX, PosY, transform.position.z);
    }
}
