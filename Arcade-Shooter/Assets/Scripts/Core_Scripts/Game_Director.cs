using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game_Director : MonoBehaviour
{
    #region UI Elements
    [Header("UI Stuff")]
    [SerializeField] private GameObject WaveText;
    [SerializeField] private GameObject EndGameUI;
    [SerializeField] private GameObject EndWaveText;
    [SerializeField] private string[] InnovativeMessages;
    #endregion
    #region Temp Variables
    public int ItemDropChance;
    private bool StartTimer=true;
    private float WaveTimer;
    private int _index;
    private bool SpawnAllowed = true;
    [HideInInspector]public int WaveNumber;
    private float AttackTimer=1.5f;
    bool firsttime = false;
    [Header("Enemy Waves")]
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
        if (!GameManager._Instance.GamePause)
        {
            if (WaveNumber<Waves.Length)
            {
                int a = Random.Range(5, 2);
                CheckAlive();
                if (StartTimer)
                {
                    WaveTimer += Time.deltaTime;
                    if (WaveTimer>=3f)
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
            } 
        }
        else
        {
          //  StopAllCoroutines();
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
        for (int i = 0; i < Waves[a].EnemyList.Count; i++)
            {
                if (!GameManager._Instance.GamePause)
                {
                    Waves[a].EnemyList[i].transform.position = transform.position;
                    Waves[a].EnemyList[i].transform.rotation = transform.rotation;
                    Waves[a].EnemyList[i].SetActive(true);
                    if (i == Waves[a].EnemyList.Count - 1)
                    {
                        Debug.Log("WHAT?");
           
                    }
                    yield return new WaitForSeconds(Waves[a].ActiveDly);     
                }
                else
                {
                    Debug.Log("OMG!");
                    yield return i;
                }
            }
        SpawnAllowed = false;
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
                if (ItemDropChance < 95)
                {
                    ItemDropChance += 5;
                }
                WaveNumber++;
                if (WaveNumber < Waves.Length)
                {
                    int temp = WaveNumber+1;
                    WaveText.GetComponent<Text>().text = "Wave" + " "+ temp;
                    EndWaveText.GetComponent<Text>().text =
                        InnovativeMessages[Random.Range(0, InnovativeMessages.Length)];
                    EndWaveText.SetActive(true); 
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
