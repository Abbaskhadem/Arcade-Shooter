using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    bool a;
    [SerializeField]
    Transform WhereToSpawn;
    int j = 0;
    public float TimeBetweenWave;
    [System.Serializable]
    public class Wave
    {
        public GameObject Objects;
        public float TimeBetweenEnemies;
        public List<GameObject> EnemiesList;
        public int MaxEnemies;
    }
    public int frame;

    public Wave[] _Waves;
    private int WaveNumber;
    void Start()
    {
        Vector3 spawnPosition = WhereToSpawn.transform.position;
        StartCoroutine(SpawnEnemyWaves());

    }

    void FixedUpdate()
    {
        // Debug.Log(ch());        

        frame = 0;
        Debug.Log(ch());
        if (frame <= 1)
        {
            if (!ch())
            {

                Debug.Log("Frame: " + frame);
                frame++;

            }

        }
        else
            return;
    }
    IEnumerator SpawnEnemyWaves()

    {
        float waveType = 0;
        while (waveType < 1)
        {
            waveType++;
            for (j = 0; j < _Waves.Length; j++)
            {

                _Waves[j].EnemiesList = GameManager.ObjectPooler(_Waves[j].Objects, _Waves[j].MaxEnemies);

                for (int i = 0; i < _Waves[j].MaxEnemies; i++)
                {
                    //  _Waves[i].Objects.transform.position = spawnPosition;
                    //  _Waves[i].Objects.transform.rotation = spawnRotation;
                    _Waves[j].EnemiesList[i].SetActive(true);
                    yield return new WaitForSeconds(_Waves[j].TimeBetweenEnemies);

                }

                yield return new WaitUntil(() => frame >= 1);
            }

        }

    }
    public bool ch()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
            return false;
        return true;
    }
}
