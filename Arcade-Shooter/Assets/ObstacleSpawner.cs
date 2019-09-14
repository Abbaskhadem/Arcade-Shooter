using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private Transform[] SpawnPositions;
    private float SpawnTime;
    [SerializeField]private float SpawnRate;
    private int b;

    private void Start()
    {
        for (int i = 0; i < Random.Range(1,4); i++)
        {
            int a = Random.Range(0, SpawnPositions.Length - 1);
            if(a==b)
                break;
            else
            {
                Instantiate(Obstacles[Random.Range(0, Obstacles.Length-1)],
                    SpawnPositions[a].transform.position, Quaternion.identity);
            }

            b = a;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager._Instance.GamePause)
        {
            SpawnTime += Time.deltaTime;
            if (SpawnTime >= SpawnRate)
            {
                SpawnTime = 0;
                for (int i = 0; i < Random.Range(1,4); i++)
                {
                    int a = Random.Range(0, SpawnPositions.Length - 1);
                    if(a!=b)
                    {
                        Instantiate(Obstacles[Random.Range(0, Obstacles.Length-1)],
                            SpawnPositions[a].transform.position, Quaternion.identity);
                    }
                    b = a;
                }
            }
        }
    }
}
