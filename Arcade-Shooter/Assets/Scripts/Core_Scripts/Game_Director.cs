using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Director : MonoBehaviour
{
    #region Temp Variables
    public bool Randomize;
    bool UpgrateWave;
    int LastRandom = 1;
    int RandomWave;
    private bool SpawnAllowed = true;
    private int WaveNumber;
    bool firsttime = false;
    public _Wave[] Waves;
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
        UpgrateWave = true;

    }
        void Update()
    {
        
        CheckAlive();
        

        if(Randomize && UpgrateWave)
        {
            Debug.Log("ok");
                UpgrateWaveSys();
           
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

            for (int j = 0; j < Waves[a].EnemyTypes.Length; j++)
        {
            for (int k = 0; k < Waves[a].Quantity[j]; k++)
            {
                GameObject temp = (GameObject) Instantiate(Waves[a].EnemyTypes[j]);
                temp.SetActive(false);
                Waves[a].EnemyList.Add(temp);
            }
        }
        }
        SpawnAllowed = false;
       if(a<WaveNumber||a< RandomWave)
       
        for (int i = 0; i < Waves[a].EnemyList.Count; i++)
            {
                if (a < WaveNumber || a < RandomWave)
                {
                    Waves[a].EnemyList[i].transform.position = transform.position;
                    Waves[a].EnemyList[i].transform.rotation = transform.rotation;
                    Waves[a].EnemyList[i].SetActive(true);
                }
                   
                yield return new WaitForSeconds(Waves[a].ActiveDly);
            }
        yield return null;
    }
    bool CheckAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !SpawnAllowed)
        {
            if (WaveNumber<Waves.Length && firsttime)
            {
                firsttime = false;
                WaveNumber++;
                UpgrateWave = true;
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
    #endregion
    void UpgrateWaveSys()
    {
         RandomWave = Random.Range(LastRandom-1, LastRandom=2 * LastRandom);
             StartCoroutine(SpawnEnemyWaves(RandomWave));
              UpgrateWave = false;


    }
}
