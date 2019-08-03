using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Director : MonoBehaviour
{
    private bool SpawnAllowed = true;
    private int WaveNumber;
    private int frame;
    bool firsttime = false;
    [System.Serializable]
    public class _Wave
    {
        public int MaxEnemies;
        public GameObject[] EnemyTypes;
        public int[] Quantity;
        public float ActiveDly;
        public List<GameObject> EnemyList;
    }

    public _Wave[] Waves;

    void Start()
    {
        for (int i = 0; i < Waves.Length; i++)
        {
            for (int j = 0; j < Waves[i].EnemyTypes.Length; j++)
            {
                for (int k = 0; k <= Waves[i].Quantity[j]; k++)
                {
                    GameObject temp = (GameObject) Instantiate(Waves[i].EnemyTypes[j]);
                    temp.SetActive(false);
                    Waves[i].EnemyList.Add(temp);
                }
            }
        }
    }
    void FixedUpdate()
    {
        frame = 0;

        if (frame <= 1)
        {
            if (!CheckAlive())
            {
                frame++;
            }

        }
        else
            return;
    }
    void Update()
    {
        if (SpawnAllowed)
        {
            Debug.Log("Spawning!");
            StartCoroutine(SpawnEnemyWaves());
        }
    }
    IEnumerator SpawnEnemyWaves()
    {
        SpawnAllowed = false;
        for (int i = 0; i < Waves[WaveNumber].MaxEnemies; i++)
            {
                Waves[WaveNumber].EnemyList[i].transform.position = transform.position;
                Waves[WaveNumber].EnemyList[i].transform.rotation = transform.rotation;
                Waves[WaveNumber].EnemyList[i].SetActive(true);
  
                yield return new WaitForSeconds(Waves[WaveNumber].ActiveDly);
            }

        yield return null;

    }
    bool CheckAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !SpawnAllowed)
        {
            if (WaveNumber<Waves.Length && firsttime)
            {
                Debug.Log("What Up!?");
                firsttime = false;
                WaveNumber++;
                SpawnAllowed = true;
            }
            return false;   
        }
        else
        {
            firsttime = true;
            return true;  
        }
  
    }
    
}
