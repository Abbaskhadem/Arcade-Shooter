using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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
    [SerializeField] private Dictionary<string,string> InnovativeMessages;
    #endregion
    #region Temp Variables
    [Header(("LeveL Info"))]
    public int ItemDropChance;
    private bool StartTimer=true;
    private float WaveTimer;
    private int _index;
   [HideInInspector] public bool SpawnAllowed = true;
    [HideInInspector]public int WaveNumber;
    private float AttackTimer=1.5f;
    private int K;
    private int J;
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
        public Routes[] Movements;
        public Transform FinalPositions;
        public float AttackSpeed;
    }
    [System.Serializable]
    public class Routes
    {
        public Transform[] Parts;
    }
    #endregion
    #region Check When To Active

    private void Awake()
    {
        for (int i = 0; i < Waves.Length; i++)
        {
            _index = 0;
            for (int j = 0; j < Waves[i].EnemyTypes.Length; j++)
            {
                for (int k = 0; k < Waves[i].Quantity[j]; k++)
                {
                    GameObject temp = (GameObject) Instantiate(Waves[i].EnemyTypes[j]);
                    temp.SetActive(false);
                    temp.GetComponent<Enemy_SpaceShip>().FinalDestination = Waves[i].FinalPositions.GetChild(_index++);
                    temp.GetComponent<Enemy_SpaceShip>().Routes = new Transform[Waves[i].Movements[j].Parts.Length];
                    for (int l = 0; l < Waves[i].Movements[j].Parts.Length; l++)
                    {
                        temp.GetComponent<Enemy_SpaceShip>().Routes[l] = Waves[i].Movements[j].Parts[l];    
                    }
                    Waves[i].EnemyList.Add(temp);
                }
            } 
        }

    }

    void Start()
    {
        int temp = WaveNumber + 1;
        WaveText.GetComponent<Text>().text =Farsi.multiLanguageText( "Wave " +temp,"موج"+temp);
        WaveText.SetActive(true);
    }
    void Update()
    {
        if (!GameManager._Instance.GamePause && !GameManager._Instance.GameEnded)
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
                if (!SpawnAllowed )
                {
                    AttackTimer += Time.deltaTime;
                    if (AttackTimer >= Waves[WaveNumber].AttackSpeed)
                    {
                        AttackTimer = 0;
                        for (int i = 0; i < Random.Range(1,3); i++)
                        {
                            if (Waves[WaveNumber].EnemyList[Random.Range(0, Waves[WaveNumber].EnemyList.Count)] != null &&
                                !Waves[WaveNumber].EnemyList[Random.Range(0, Waves[WaveNumber].EnemyList.Count)].GetComponent<Enemy_SpaceShip>().Melee )
                            {
                                Waves[WaveNumber].EnemyList[Random.Range(0, Waves[WaveNumber].EnemyList.Count)].GetComponent<Enemy_SpaceShip>().Shoot();
                            }
                            else if(Waves[WaveNumber].EnemyList[Random.Range(0, Waves[WaveNumber].EnemyList.Count)] != null &&
                                    Waves[WaveNumber].EnemyList[Random.Range(0, Waves[WaveNumber].EnemyList.Count)].GetComponent<Enemy_SpaceShip>().Melee)
                            {
                                if (!Waves[WaveNumber].EnemyList[Random.Range(0, Waves[WaveNumber].EnemyList.Count)]
                                    .GetComponent<Enemy_SpaceShip>().Droping)
                                {
                                    Waves[WaveNumber].EnemyList[Random.Range(0, Waves[WaveNumber].EnemyList.Count)]
                                        .GetComponent<Enemy_SpaceShip>().GoDrop = true;
                                }
                            }
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
        for (int i = 0; i < Waves[a].EnemyList.Count; i++)
            {
                if (!GameManager._Instance.GamePause)
                {
                    Waves[a].EnemyList[i].transform.position = transform.position;
                    Waves[a].EnemyList[i].SetActive(true);
                    yield return new WaitForSeconds(Waves[a].ActiveDly);     
                }
                else
                {
                    yield return null;
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
                    WaveText.GetComponent<Text>().text =Farsi.multiLanguageText( "Wave " +temp,"موج"+temp);
                    EndWaveText.GetComponent<Text>().text = Farsi.multiLanguageText("Great!", "عالی!");
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
