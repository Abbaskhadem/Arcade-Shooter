using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Director : MonoBehaviour
{
    #region Temp Variables
    public int i;
    int t;
    int che;
    [SerializeField]
    bool Randomize;
    bool UpgrateWave = false;
    public int LastRandom = 0;
    public int FirstRandom = 0;
    public int RandomWave = 0;
    private bool SpawnAllowed = true;
    private int WaveNumber;
    bool firsttime = false;
    public _Wave[] Waves;
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
        private int frame;
    }
    #endregion
    #region Check When To Active
    void Start()
    {
        t = 100;
        UpgrateWave = true;

    }
    void Update()
    {
        CheckAlive();


        if (Randomize && UpgrateWave)
        {

            Debug.Log(UpgrateWave + "ok");
            GetRandomWaveIndex();


        }
        else
        {
            if (SpawnAllowed)
            {
                Debug.Log("notok");
                StartCoroutine(SpawnEnemyWaves(WaveNumber));
            }
        }




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
                        GameObject temp = (GameObject)Instantiate(Waves[a].EnemyTypes[j]);
                        temp.SetActive(false);
                        Waves[a].EnemyList.Add(temp);
                        Debug.Log(che + "----------" + a);
                    }
                }

            }



        }
        SpawnAllowed = false;
        for (int i = 0; i < Waves[a].EnemyList.Count; i++)
        {
            Waves[a].EnemyList[i].transform.position = transform.position;
            Waves[a].EnemyList[i].transform.rotation = transform.rotation;
            Waves[a].EnemyList[i].SetActive(true);
            yield return new WaitForSeconds(Waves[a].ActiveDly);
        }
        yield return null;
    }
    bool CheckAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !SpawnAllowed)
        {
            if (WaveNumber <= Waves.Length && firsttime)
            {
                firsttime = false;
                WaveNumber++;
                UpgrateWave = true;
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


    void UpgrateWaveSys()
    {



        //  if (b == null) StopCoroutine(b);
        //  b = SpawnEnemyWaves(RandomWave);
        //  if (i == 6)
        //      Debug.Log("66666666666");
        //  StartCoroutine(b);
        //  RandomWave = Random.Range(0, Waves.Length);



        //  if (LastRandom < Waves.Length - 1)
        //  {
        //      FirstRandom = LastRandom;
        //      LastRandom += 2;
        //      RandomWave = Random.Range(FirstRandom, LastRandom);
        //      StartCoroutine(SpawnEnemyWaves(RandomWave));
        //      Debug.Log("ToelseNist");
        //      UpgrateWave = false;
        //
        //  }
        //  else
        //  {
        //      RandomWave = Random.Range(FirstRandom, LastRandom);
        //      StartCoroutine(SpawnEnemyWaves(RandomWave));
        //      Debug.Log("Toelse");
        //      UpgrateWave = false;
        //     
        //
        //  }
    }


    void GetRandomWaveIndex()
    {

        FirstRandom = WaveNumber - 1;
        LastRandom = WaveNumber + 2;
        if (FirstRandom < 0)
            FirstRandom = 0;
        if (FirstRandom >= Waves.Length)
            FirstRandom = Waves.Length - 3;
        if (LastRandom > Waves.Length)
            LastRandom = Waves.Length;
        UpgrateWave = false;

        i++;
        if (b != null) StopCoroutine(b);
        b = SpawnEnemyWaves(RandomWave);
        StartCoroutine(b);
        RandomWave = Random.Range(FirstRandom, LastRandom);
    }
    #endregion

}
