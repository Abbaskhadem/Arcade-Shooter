using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game_Director : MonoBehaviour
{
    #region UI Elements

    [SerializeField] private GameObject WaveText;
    [SerializeField] private GameObject EndGameUI;
    

    #endregion
    #region Temp Variables
    private bool StartTimer=true;
    private float WaveTimer;
    private int _index;
    private bool SpawnAllowed = true;
    public int WaveNumber;
    private float AttackTimer=1.5f;
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
        public Transform[] Routes;
        public Transform FinalPositions;
    }
    #endregion
    #region Check When To Active

    void Start()
    {
        int temp = WaveNumber + 1;
        WaveText.GetComponent<Text>().text = "Wave" + " " + temp;
        WaveText.SetActive(true);
    }
    void Update()
    {
        if (WaveNumber<Waves.Length)
        {
            int a = Random.Range(5, 2);
            CheckAlive();
            if (StartTimer)
            {
                WaveTimer += Time.deltaTime;
                if (WaveTimer>=2f)
                {
                    if (SpawnAllowed)
                    {
                        WaveTimer = 0;
                        StartTimer = false;
                        StartCoroutine(SpawnEnemyWaves(WaveNumber));
                    }
                }
            }

            if (!SpawnAllowed)
            {
                AttackTimer -= Time.deltaTime;
                if (AttackTimer <= 0)
                {
                    AttackTimer = Random.Range(1.5f, 2f);
                    int temp= Random.Range(0, Waves[WaveNumber].EnemyList.Count);
                   Debug.Log(temp);
                   if (Waves[WaveNumber].EnemyList[temp] != null)
                   {
                       Waves[WaveNumber].EnemyList[temp].GetComponent<Enemy_SpaceShip>().Shoot();
                   }
                }

            }
        }
        else
        {
            GameManager._Instance.GameEnded = true;
            EndGameUI.SetActive(true);
            Debug.Log("Game Ended!");
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
                temp.GetComponent<Enemy_SpaceShip>().FinalDestination = Waves[a].FinalPositions.GetChild(_index++);
                temp.GetComponent<Enemy_SpaceShip>().Routes = new Transform[1]; 
                temp.GetComponent<Enemy_SpaceShip>().Routes[0] = Waves[a].Routes[j];
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
   
                _index = 0;
                firsttime = false;
                WaveNumber++;
                if (WaveNumber < Waves.Length)
                {
                    int temp = WaveNumber+1;
                    WaveText.GetComponent<Text>().text = "Wave" + " "+ temp;
                    WaveText.SetActive(true); 
                }
                StartTimer = true;
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
