using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Director : MonoBehaviour
{
    #region Temp Variables
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
    #region Creating The List
    void Start()
    {
        var a = Waves[Random.Range(0, Waves.Length)];
        for (int i = 0; i < Waves.Length; i++)
        {
            for (int j = 0; j < Waves[i].EnemyTypes.Length; j++)
            {
                for (int k = 0; k < Waves[i].Quantity[j]; k++)
                {
                    GameObject temp = (GameObject) Instantiate(Waves[i].EnemyTypes[j]);
                    temp.SetActive(false);
                    Waves[i].EnemyList.Add(temp);
                }
            }
        }
    }
    #endregion#region Check When To Active
    #region Check When To Active
    void Update()
    {
        CheckAlive();
        if (SpawnAllowed)
        {
            StartCoroutine(SpawnEnemyWaves());
        }
    }
    #endregion
#region Activating Functions
    IEnumerator SpawnEnemyWaves()
    {
        SpawnAllowed = false;
        for (int i = 0; i < Waves[WaveNumber].EnemyList.Count; i++)
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
    #endregion
}
