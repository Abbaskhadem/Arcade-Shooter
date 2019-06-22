using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    Vector2 whereTospawn;
    float roundX;
    public float spawnRate = 2f;
    float nextspawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if (Time.time > nextspawn)
        {

            nextspawn = (Time.time + 0.05f) + spawnRate;
            roundX = Random.Range(-1.66f, 2);
            whereTospawn = new Vector2(roundX, transform.position.y);
            Instantiate(enemy, whereTospawn, Quaternion.identity);
        }
    }
}
