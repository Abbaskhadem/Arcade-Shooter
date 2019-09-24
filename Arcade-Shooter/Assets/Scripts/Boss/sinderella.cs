using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinderella : MonoBehaviour
{
    [HideInInspector] public GameObject[] activeBossTurrets = new GameObject[2];
    [SerializeField] private GameObject Gune1;
    [SerializeField] private GameObject Gune2;
    private float Timer;

    void Start()
    {
        activeBossTurrets[0] = Gune1;
        activeBossTurrets[1] = Gune2;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 3)
        {
            Timer = 0;
            foreach (GameObject turret in activeBossTurrets)
            {
                StartCoroutine(SpawnEnemyWaves(turret));
            }
        }

        //StartCoroutine(SpawnEnemyWaves());
    }


    IEnumerator SpawnEnemyWaves(GameObject position)
    {
        //functions
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("sinderella Bullet");
            if (bullet != null)
            {
                bullet.transform.position = position.transform.position;
                bullet.transform.rotation = position.transform.rotation;
                bullet.SetActive(true);
                yield return new WaitForSeconds(0.02f);
            }
        }

        yield return null;
    }
}