using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Director : MonoBehaviour
{
    #region Temp Variables

    private int temp1;
    bool UpgrateWave = false;
    int LastRandom = 0;
    int FirstRandom = 0;
    int RandomWave = 0;
    private bool SpawnAllowed = true;
    private int WaveNumber;
    bool firsttime = false;
    public Transform[] Rout;
    public _Wave[] Waves;
    private int RandomRout;
    IEnumerator b;

    #endregion

    #region MyWave_Class

    [System.Serializable]
    public class _Wave
    {
        public GameObject[] EnemyTypes;
        public int[] Quantity;
        public float ActiveDly;
        public List<GameObject> EnemyList;
        public Transform[] FinalPositions;
    }

    #endregion
    #region Check When To Active

    void Start()
    {
       // RandomRout = 0;
        UpgrateWave = true;
    }

    void Update()
    {
        CheckAlive();
        if (UpgrateWave) GetRandomWaveIndex();
    }

    #endregion

    #region Activating Functions

    IEnumerator SpawnEnemyWaves(int a)
    {
        if (SpawnAllowed)
        {
            if (Waves[a].EnemyList.Count == 0)
            {
                for (int j = 0; j < Waves[a].EnemyTypes.Length; ++j)
                {
                   
                    for (int k = 0; k < Waves[a].Quantity[j]; k++)
                    {
                        
                        int RandomFinalPositions = Random.Range(0, Waves[a].FinalPositions.Length);
                        GameObject temp = (GameObject) Instantiate(Waves[a].EnemyTypes[j]);
                        temp.SetActive(false);
                        Waves[a].EnemyList.Add(temp);
                        temp.GetComponent<Enemy_SpaceShip>().MoveAllowed = true;
                       //temp.GetComponent<Enemy_SpaceShip>().Routes = new Transform[1];
                       //temp.GetComponent<Enemy_SpaceShip>().Routes[0] = Rout[RandomRout];
                        temp.GetComponent<Enemy_SpaceShip>().FinalDestination =
                            Waves[a].FinalPositions[RandomFinalPositions].GetChild(temp1++);
                    }
                }
            }
        }
        SpawnAllowed = false;
        RandomRout = Random.Range(0, Rout.Length);
        for (int i = 0; i < Waves[a].EnemyList.Count; i++)
        {
            //Waves[a].EnemyList[i].GetComponent<Enemy_SpaceShip>().MoveAllowed = true;
            Waves[a].EnemyList[i].transform.position = transform.position;
            Waves[a].EnemyList[i].transform.rotation = transform.rotation;
            Waves[a].EnemyList[i].GetComponent<Enemy_SpaceShip>().MoveAllowed = true;
            Waves[a].EnemyList[i].GetComponent<Enemy_SpaceShip>().Routes = new Transform[1];
            Waves[a].EnemyList[i].GetComponent<Enemy_SpaceShip>().Routes[0] = Rout[RandomRout];
            Debug.Log(Waves[a].EnemyList[i].GetComponent<Enemy_SpaceShip>().MoveAllowed+""+RandomRout);
            Waves[a].EnemyList[i].SetActive(true);
            //Waves[a].EnemyList[i].GetComponent<Enemy_SpaceShip>().MoveAllowed = true;
            yield return new WaitForSeconds(Waves[a].ActiveDly);
        }

        yield return null;
    }

    bool CheckAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !SpawnAllowed)
        {
            //RandomRout = Random.Range(0, Rout.Length);
            if (WaveNumber <= Waves.Length && firsttime)
            {
                firsttime = false;
                WaveNumber++;
                UpgrateWave = true;
                temp1 = 0;
                SpawnAllowed = true;
            }
            else
            {
                WaveNumber = Waves.Length - 4;
            }

            return false;
        }
        else
        {
            firsttime = true;
            return true;
        }
    }

    void GetRandomWaveIndex()
    {
        FirstRandom = WaveNumber - 1;
        LastRandom = WaveNumber + 2;
        if (FirstRandom < 0) FirstRandom = 0;
        if (FirstRandom >= Waves.Length) FirstRandom = Waves.Length - 3;
        if (LastRandom > Waves.Length) LastRandom = Waves.Length;
        UpgrateWave = false;
        if (b != null) StopCoroutine(b);
        b = SpawnEnemyWaves(RandomWave);
        StartCoroutine(b);
        RandomWave = Random.Range(FirstRandom, LastRandom);
    }

    #endregion
}