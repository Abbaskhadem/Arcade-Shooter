using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject Objects;
        public float TimeBetweenEnemies;
        public List<GameObject> EnemiesList;
        public int MaxEnemies;
    }

    public Wave[] _Waves;
    private int WaveNumber;
    void Start()
    {
        for (int i = 0; i < _Waves.Length; i++)
        {
            _Waves[i].EnemiesList = GameManager.ObjectPooler(_Waves[i].Objects, _Waves[i].MaxEnemies);
        }
    }

    void Update()
    {
        
    }
}
