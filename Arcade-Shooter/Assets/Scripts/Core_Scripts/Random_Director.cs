﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Director : MonoBehaviour
{
    #region Temp Variables
    
    private float AttackTimer;
    private int temp1;
    bool UpgrateWave = false;
    int LastRandom = 0;
    int FirstRandom = 0;
    int RandomWave = 0;
    int i = 0;
    private bool dd = false;
    private int temp;
    private List<GameObject> activEnemy;
    [HideInInspector] public bool SpawnAllowed = true;
    private int WaveNumber;
    bool firsttime = false;
    public Transform[] Rout;
    public _Wave[] Waves;
    private int RandomRout;
    private int RandomFinalPositions;
    private int TmpLast;
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
        public bool spetiallRout;
        public Transform[] Routes;
        public Transform[] FinalPositions;
    }

    #endregion

    #region Check When To Active

    void Start()
    {
        TmpLast = 1;
        LastRandom = (Waves.Length / 6) / 2;
        FirstRandom = 0;
        // RandomRout = 0;
        //  Pause = false;
        UpgrateWave = true;
    }

    void Update()
    {
        CheckAlive();
        if (UpgrateWave) GetRandomWaveIndex();
        if (!SpawnAllowed)
        {
            AttackTimer -= Time.deltaTime;
            if (AttackTimer <= 0)
            {
                AttackTimer = Random.Range(1.5f, 2f);
                temp = Random.Range(0, Waves[RandomWave].EnemyList.Count);
                if (Waves[RandomWave].EnemyList[temp] != null)
                {
                    if (Waves[RandomWave].EnemyList[temp].activeInHierarchy)
                        Waves[RandomWave].EnemyList[temp].GetComponent<Enemy_SpaceShip>().Shoot();
                }
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
                        RandomFinalPositions = Random.Range(0, Waves[a].FinalPositions.Length);
                        GameObject temp = (GameObject) Instantiate(Waves[a].EnemyTypes[j]);
                        temp.SetActive(false);
                        Waves[a].EnemyList.Add(temp);
                        temp.GetComponent<Enemy_SpaceShip>().MoveAllowed = true;
                        temp.GetComponent<Enemy_SpaceShip>().FinalDestination =
                            Waves[a].FinalPositions[RandomFinalPositions].GetChild(temp1++);
                        if (Waves[a].spetiallRout)
                        {
                            temp.GetComponent<Enemy_SpaceShip>().Routes = new Transform[1];
                            temp.GetComponent<Enemy_SpaceShip>().Routes[0] = Waves[a].Routes[j];
                        }
                    }
                }
            }

            for (int j = 0; j < Waves[a].EnemyList.Count; j++)
            {
                if (Waves[a].EnemyList[j].activeInHierarchy)
                {
                    activEnemy.Add(Waves[a].EnemyList[j]);
                }
            }

            RandomRout = Random.Range(0, Rout.Length);
            for (i = 0; i < Waves[a].EnemyList.Count; i++)
            {
                var Local = Waves[a].EnemyList[i];
                var localSecend = Local.GetComponent<Enemy_SpaceShip>();
                localSecend.coroutineAllowed = true;
                localSecend.MoveAllowed = true;
                //localSecend.tparam = 0;
                Local.GetComponent<Animator>().ResetTrigger("GotHit");
                Local.transform.position = transform.position;
                // Local.transform.rotation = transform.rotation;
                localSecend.MoveAllowed = true;
                // if (Waves[a].spetiallRout)
                // {
                //    // for (int j = 0; j < Waves[a].EnemyTypes.Length; ++j)
                //    // {
                //         localSecend.Routes = new Transform[1];
                //         localSecend.Routes[0] = Waves[a].Routes[i];
                //        // Waves[a].EnemyList[j].SetActive(true);
                //    // }
                // }
                // for (int j = 0; j < Waves[a].EnemyTypes.Length; ++j)
                // {
                //     for (int k = 0; k < Waves[a].Quantity[j]; k++)
                //     {
                //         if (Waves[a].spetiallRout)
                //         {
                //             localSecend.Routes = new Transform[1];
                //             localSecend.Routes[0] = Waves[a].Routes[j];
                //         }
                //     }
                // }
                RandomRout = Random.Range(0, Rout.Length);
                if (!Waves[a].spetiallRout)
                {
                    localSecend.Routes = new Transform[1];
                    localSecend.Routes[0] = Rout[RandomRout];
                }

                // localSecend.Routes = new Transform[1];
                // localSecend.Routes[0] = Rout[RandomRout];
                Waves[a].EnemyList[i].SetActive(true);
//                Debug.Log("InToshNis:)");
                dd = false;
                if (i == Waves[a].EnemyList.Count - 1)
                {
                    SpawnAllowed = false;
                }


                yield return new WaitForSeconds(Waves[a].ActiveDly);
            }

//            Debug.Log("InToshNisoooooooooo:)");
            yield return null;
        }
    }

    bool CheckAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !SpawnAllowed)
        {
            Debug.Log("tosh");
            //RandomRout = Random.Range(0, Rout.Length);
            // if (WaveNumber <= Waves.Length && firsttime)
            // {
            firsttime = false;
            WaveNumber++;
            UpgrateWave = true;
            temp1 = 0;
            SpawnAllowed = true;
            RandomRout = Random.Range(0, Rout.Length);
            // }
            // else
            // {
            //     WaveNumber = Waves.Length - 1;
            // }

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
        if (WaveNumber > 0)
        {
            if (LastRandom != Waves.Length)
            {
                Debug.Log("toRandom");
                if (WaveNumber % 2 == 1)
                {
                    if (LastRandom <= Waves.Length)
                        LastRandom = LastRandom + TmpLast;
                }
                else
                {
                    FirstRandom++;
                }

                RandomWave = Random.Range(FirstRandom, LastRandom);
                UpgrateWave = false;
                if (b != null) StopCoroutine(b);
                b = SpawnEnemyWaves(RandomWave);
                StartCoroutine(b);
            }
            else
            {
                RandomWave = Random.Range(FirstRandom, LastRandom-1);
                UpgrateWave = false;
                if (b != null) StopCoroutine(b);
                b = SpawnEnemyWaves(RandomWave);
                StartCoroutine(b);
            }
        }
        else
        {
            RandomWave = Random.Range(0, 3);
            UpgrateWave = false;
            if (b != null) StopCoroutine(b);
            b = SpawnEnemyWaves(RandomWave);
            StartCoroutine(b);
        }


        // FirstRandom = WaveNumber - 1;
        //  LastRandom = WaveNumber + 2;
        //  if (FirstRandom < 0) FirstRandom = 0;
        //  if (FirstRandom >= Waves.Length) FirstRandom = Waves.Length - 3;
        //  if (LastRandom > Waves.Length) LastRandom = Waves.Length;
        //  RandomWave = Random.Range(FirstRandom, LastRandom);
        //  UpgrateWave = false;
        //  if (b != null) StopCoroutine(b);
        //  b = SpawnEnemyWaves(RandomWave);
        //  StartCoroutine(b);
    }

    #endregion
}