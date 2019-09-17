using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_WaveGenerator : MonoBehaviour
{
    #region Temp Variables

    public GameObject[] EnemyTypesMain;
    public Transform[] EnemyFinalPositionsMain;
    public Transform[] EnemyRoutsMain;
    public float[] ActiveDlyMain;
    private int FirstEnemy;
    private int LastEnemy = 1;

    public _Wave Wave;
    private float AttackTimer;
    private int temp1;
//    bool UpgrateWave = false;
//    int LastRandom = 0;
//    int FirstRandom = 0;
//    int RandomWave = 0;
//    int i = 0;
//    private bool dd = false;
//    private int temp;
//    private List<GameObject> activEnemy;
   [HideInInspector] public bool SpawnAllowed = true;
//    private int WaveNumber;
    bool firsttime = false;
//    public Transform[] Rout;
//    public _Wave[] Waves;
//    private int RandomRout;
//    private int RandomFinalPositions;
//    private int TmpLast;
//    IEnumerator b;

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
        public Transform Routes;
        public Transform FinalPositions;
    }

    #endregion

    #region Check When To Active
    void Update()
    {
        CheckAlive();
        if (SpawnAllowed)
        {
            Debug.Log(SpawnAllowed);
          //  Debug.Log("Generate WAVE!");
            StartCoroutine(SpawnEnemyWaves(GenerateWave()));
        }
  
    }

    #endregion

    #region Activating Functions

    _Wave GenerateWave()
    {
        Wave.EnemyTypes=new GameObject[1];
        for (int i = 0; i < Wave.EnemyTypes.Length; i++)
        {
            Wave.EnemyTypes[i] = EnemyTypesMain[0];
        }
        Wave.FinalPositions = EnemyFinalPositionsMain[Random.Range(0,EnemyFinalPositionsMain.Length)];
        Wave.Routes = EnemyRoutsMain[Random.Range(0, EnemyRoutsMain.Length)];
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
                    for (int k = 0; k < a.FinalPositions.childCount; k++)
                    {
                        GameObject temp = Instantiate(a.EnemyTypes[j]);
                        temp.SetActive(false);
                        a.EnemyList.Add(temp);
                        temp.GetComponent<Enemy_SpaceShip>().MoveAllowed = true;
                        temp.GetComponent<Enemy_SpaceShip>().Routes = new Transform[1]; 
                        temp.GetComponent<Enemy_SpaceShip>().Routes[0] = a.Routes;
                        temp.GetComponent<Enemy_SpaceShip>().FinalDestination = a.FinalPositions.GetChild(k);
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
            Wave.EnemyList.Clear();
            Debug.Log("NO ENEMY FOUND!");
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
    
    #endregion
}