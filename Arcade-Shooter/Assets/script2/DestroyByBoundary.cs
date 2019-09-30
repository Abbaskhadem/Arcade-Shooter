using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            if (gameObject.tag == "Player Bullet" || gameObject.tag=="Enemy Bullet" || gameObject.tag=="sinderella Bullet")
            {
                if(!gameObject.GetComponent<Bullet>().Child)
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
