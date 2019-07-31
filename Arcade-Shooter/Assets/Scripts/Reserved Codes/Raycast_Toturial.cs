using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_Toturial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var Dir = Camera.main.ScreenToWorldPoint((Input.mousePosition));
        Dir.z = 0;
        var hit = Physics2D.Raycast(Vector2.zero,Dir.normalized);
        if (hit)
        {
            Debug.Log("Point+" + hit.point);

        }
    }
}
