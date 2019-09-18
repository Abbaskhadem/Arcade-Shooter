using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_WaveGenerator : MonoBehaviour
{
    #region Temp Variables

    public int ItemDropChance;
    private float AttackSpeed=3.5f;
    public GameObject[] EnemyTypesMain;
    public Transform[] EnemyFinalPositionsMain;
    public Transform[] EnemyRoutsMain;
    public float[] ActiveDlyMain;
    private int FirstEnemy;
    private int LastEnemy = 1;
    public _Wave Wave;
    private float AttackTimer;
    private int temp1;
    [HideInInspector] public bool SpawnAllowed = true;
    private bool firsttime;
    private int Index;
    private int RandomType;
    private int c;

    #endregion

    #region MyWave_Class

    [System.Serializable]
    public class _Wave
    {
        public GameObject[] EnemyTypes;
        public int[] Quantity;
        public float ActiveDly;
        public List<GameObject> EnemyList;
        public Transform[] Routes;
        public Transform FinalPositions;
    }

    #endregion

    #region Check When To Active
    void Update()
    {
        CheckAlive();
        if (SpawnAllowed)
        {
            RandomType = Random.Range(0, 100);
            StartCoroutine(SpawnEnemyWaves(GenerateWave()));
        }
        if (!SpawnAllowed)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >=AttackSpeed )
            {
                AttackTimer = 0;
                for (int i = 0; i < Random.Range(1,3); i++)
                {
                    if (Wave.EnemyList[Random.Range(0, Wave.EnemyList.Count)] != null &&
                        !Wave.EnemyList[Random.Range(0, Wave.EnemyList.Count)].GetComponent<Enemy_SpaceShip>().Melee )
                    {
                        Wave.EnemyList[Random.Range(0, Wave.EnemyList.Count)].GetComponent<Enemy_SpaceShip>().Shoot();
                    }
                    else if(Wave.EnemyList[Random.Range(0, Wave.EnemyList.Count)] != null &&
                            Wave.EnemyList[Random.Range(0, Wave.EnemyList.Count)].GetComponent<Enemy_SpaceShip>().Melee)
                    {
                        if (!Wave.EnemyList[Random.Range(0, Wave.EnemyList.Count)]
                            .GetComponent<Enemy_SpaceShip>().Droping)
                        {
                            Wave.EnemyList[Random.Range(0, Wave.EnemyList.Count)]
                                .GetComponent<Enemy_SpaceShip>().GoDrop = true;
                        }
                    }
                }

            }

        }
    }

    #endregion

    #region Activating Functions

    _Wave GenerateWave()
    {
        Wave.EnemyTypes=new GameObject[Random.Range(1,EnemyTypesMain.Length)];
        for (int i = 0; i < Wave.EnemyTypes.Length; i++)
        {
            Wave.EnemyTypes[i] = EnemyTypesMain[Random.Range(0,EnemyTypesMain.Length)];
        }
        Wave.FinalPositions = EnemyFinalPositionsMain[Random.Range(0,EnemyFinalPositionsMain.Length)];
        Wave.Routes=new Transform[EnemyTypesMain.Length];
        for (int i = 0; i <Wave.Routes.Length; i++)
        {
            Wave.Routes[i] = EnemyRoutsMain[Random.Range(0, EnemyRoutsMain.Length)];
        }
        Wave.ActiveDly = ActiveDlyMain[Random.Range(0, ActiveDlyMain.Length)];
        return Wave;
    }

    IEnumerator SpawnEnemyWaves(_Wave a)
    {
        if (SpawnAllowed)
        {
            SpawnAllowed = false; 
                for (int j = 0; j < a.EnemyTypes.Length; ++j)
                {
                    for (int k = 0; k < a.FinalPositions.childCount / a.EnemyTypes.Length; k++)
                    {
                        if (RandomType < 25 ||a.EnemyTypes.Length==1)
                        {
                            GameObject temp = Instantiate(a.EnemyTypes[j]);
                            temp.SetActive(false);
                            a.EnemyList.Add(temp);
                            var Local=temp.GetComponent<Enemy_SpaceShip>();
                            Local.MoveAllowed = true;
                            Local.Routes = new Transform[1]; 
                            Local.Routes[0] = a.Routes[j];
                            Local.FinalDestination = a.FinalPositions.GetChild(Index++);
                        }
                        else if(a.EnemyTypes.Length!=1)
                        {
                            Debug.Log("YEKI DAR MION!");
                            GameObject temp = Instantiate(a.EnemyTypes[OnePerUnit()]);
                            temp.SetActive(false);
                            a.EnemyList.Add(temp);
                            var Local=temp.GetComponent<Enemy_SpaceShip>();
                            Local.MoveAllowed = true;
                            Local.Routes = new Transform[1]; 
                            Local.Routes[0] = a.Routes[j];
                            Local.FinalDestination = a.FinalPositions.GetChild(Index++);
                        }
           
                    }
                }
                for (int i = 0; i < a.EnemyList.Count; i++)
            {
                a.EnemyList[i].SetActive(true);
                yield return new WaitForSeconds(a.ActiveDly);
            }

            yield return null;
        }
    }

    bool CheckAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !SpawnAllowed)
        {
            foreach (var VARIABLE in Wave.EnemyList)
            {
                Destroy(VARIABLE);
            }
            Index = 0;
            Wave.EnemyList.Clear();
            firsttime = false;
            temp1 = 0;
            SpawnAllowed = true;
            return false;
        }
        else
        {
            firsttime = true;
            return true;
        }
    }

    int OnePerUnit()
    {
        if (c % 2 == 0)
        {
            c++;
            return c-1;
        }
        else
        {
            c--;
            return c + 1;
        }
    }
    #endregion
}