using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int[] WaveEnemiesSpawnType;
    public int[] EnemiesWave;
    [SerializeField]
    Transform[] EnemySpawnLocations;
    [SerializeField]
    GameObject [] EnemyType;
    [SerializeField]
    GameObject[] EnemyReserves;
    GameObject enemy;
    Vector2 whereTospawn;
    float roundX;
    public int EnemyCounter;
    public float SpawnRate;
    public int EnemiesWaveTmp;
    public int SpawnType;
    public bool SpawnAllowed=true;
    float Timer;
    public int i;
    void Update()
    {
        if (i < WaveEnemiesSpawnType.Length)
        {
            if (SpawnAllowed)
            {
                switch (SpawnType)
                {

                    case 1:
                        SpawnSoloEnemy();
                        break;
                    case 2:
                        SpawnDouEnemy();
                        break;
                    case 3:
                        SpawnGroupEnemy();
                        break;
                    case 4:
                        SpawnSoloEnemy();
                        SpawnDouEnemy();
                        break;
                    case 5:
                        SpawnGroupEnemy();
                        SpawnSoloEnemy();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void SpawnSoloEnemy()
    {
      
            if (EnemyCounter < EnemiesWaveTmp)
            {
                Timer += Time.deltaTime;
                if (Timer >= SpawnRate)
                {
                    Timer = 0;
                    Instantiate(EnemyType[0], EnemySpawnLocations[0].position, Quaternion.identity);
                    EnemyCounter++;
                }
            }
            else if (i < WaveEnemiesSpawnType.Length-1)
            {
                Debug.Log("Wave Upgrade");
                EnemyCounter = 0;
                SpawnAllowed = false;
                i++;
                SpawnType = WaveEnemiesSpawnType[i];
                EnemiesWaveTmp = EnemiesWave[i];

            }
        

    }
    public void SpawnGroupEnemy()
    {
    
            if (EnemyCounter < EnemiesWaveTmp)
            {
                for (int i = 3; i < 7; i++)
                {
                    Timer += Time.deltaTime;
                    if (Timer >= SpawnRate)
                    {
                        Timer = 0;
                        Instantiate(EnemyType[i], EnemySpawnLocations[i].position, Quaternion.identity);
                        EnemyCounter++;
                    }
                }
            }
            else if (i < WaveEnemiesSpawnType.Length-1)
            {
                Debug.Log("Wave Upgrade");
                EnemyCounter = 0;
                SpawnAllowed = false;
                i++;
                SpawnType = WaveEnemiesSpawnType[i];
                EnemiesWaveTmp = EnemiesWave[i];

            }
        
    }
    public void SpawnDouEnemy()
    {
   
            if (EnemyCounter < EnemiesWaveTmp)
            {
                for (int i = 1; i < 3; i++)
                {

                    Timer += Time.deltaTime;
                    if (Timer >= SpawnRate)
                    {
                        Timer = 0;
                        Instantiate(EnemyType[i], EnemySpawnLocations[i].position, Quaternion.identity);
                        EnemyCounter++;
                    }
                }
            }
            else if (i < WaveEnemiesSpawnType.Length-1)
            {
                Debug.Log("Wave Upgrade");
                EnemyCounter = 0;
                SpawnAllowed = false;
                i++;
                SpawnType = WaveEnemiesSpawnType[i];
                EnemiesWaveTmp = EnemiesWave[i];
            }
        
    }
    public void Ready()
    {
        SpawnAllowed = true;
    }
}
