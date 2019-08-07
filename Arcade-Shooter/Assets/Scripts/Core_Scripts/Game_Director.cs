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
       [HideInInspector]public List<GameObject> EnemyList;
        private int frame;
        public Transform[] Routes;
    }
    #endregion
    #region Check When To Active
    void Update()
    {
        if (WaveNumber<Waves.Length-1)
        {
            int a = Random.Range(5, 2);
            CheckAlive();
            if (SpawnAllowed)
            {
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
                temp.GetComponent<Enemy_SpaceShip>().FinalDestination = PositionController.Instance.GetPosition(WaveNumber);
                temp.GetComponent<Enemy_SpaceShip>().Routes=new Transform[Waves[a].Routes.Length];
                for (int i = 0; i < Waves[a].Routes.Length; i++)
                {
                    temp.GetComponent<Enemy_SpaceShip>().Routes[i] = Waves[a].Routes[i];
                }
                Waves[a].EnemyList.Add(temp);
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
            if (WaveNumber<Waves.Length && firsttime)
            {
                firsttime = false;
                PositionController.Instance._index = 0;
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
