using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController SharedInstance;
    // [Space(25f)]
    [Header("startWait")]
    public float[] startWait;     //wait to start
    public float waveInterval = 2.0f;  //wait to start again
    [Header("spawnInterval")]
    public float[] spawnInterval; // wait to spawn next enemy
    public int enemiesPerWave = 5;
    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {

        while (true)
        {
            float waveType = 2;
            for (int i = 0; i < enemiesPerWave; i++)
            {
                Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight + 2, 0));
                Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight + 2, 0));
                Vector3 spawnPosition = new Vector3(Random.Range(topLeft.x, topRight.x), topLeft.y, 0);
                Quaternion spawnRotation = Quaternion.Euler(0, 0, 180);
                if (waveType >= 5.0f)
                {
                    yield return new WaitForSeconds(startWait[0]);
                    GameObject enemy1 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Ship 1");
                    if (enemy1 != null)
                    {
                        enemy1.transform.position = spawnPosition;
                        enemy1.transform.rotation = spawnRotation;
                        enemy1.SetActive(true);
                    }
                    yield return new WaitForSeconds(spawnInterval[0]);

                }
                else
                {
                    GameObject enemy2 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Ship 2");
                    if (enemy2 != null)
                    {
                        yield return new WaitForSeconds(startWait[1]);
                        enemy2.transform.position = spawnPosition;
                        enemy2.transform.rotation = spawnRotation;
                        enemy2.SetActive(true);
                    }
                    yield return new WaitForSeconds(spawnInterval[1]);
                }

            }
            yield return new WaitForSeconds(waveInterval);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

}
