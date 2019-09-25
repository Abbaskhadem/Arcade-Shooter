using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinderella : MonoBehaviour
{
    [HideInInspector] public GameObject[] activeBossTurrets = new GameObject[2];
    [SerializeField] private GameObject Gune1;
    [SerializeField] private GameObject Gune2;
    [SerializeField] Vector2[] target;
    private float Timer;
    private int i;
    private bool LastPos = true; 
    [SerializeField] private float speed;
    void Start()
    {
        // target[0] = new Vector2(1.79f, -2.25f);
        // target[1] = new Vector2(0.92f, 0.17f);
        // target[2] = new Vector2(-1.79f, -2.25f);
        // target[3] = new Vector2(-8.7f, -0.7f);
        // target[4] = new Vector2(-5.7f, -9.7f);
        activeBossTurrets[0] = Gune1;
        activeBossTurrets[1] = Gune2;
    }

    void Update()
    {
        Monment();
        Timer += Time.deltaTime;
        if (Timer >= 3)
        {
            Timer = 0;
            foreach (GameObject turret in activeBossTurrets)
            {
                StartCoroutine(SpawnEnemyWaves(turret));
            }
        }
    }

    void Monment()
    {
        float dist = Vector3.Distance(target[i], transform.position);
        if (dist != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target[i], speed * Time.deltaTime);
        }
        else
        {
            if (i == target.Length - 1)
                LastPos = false;
            if (LastPos)
            {
                if (i < target.Length - 1)
                    i++;
                transform.position = Vector2.MoveTowards(transform.position, target[i], speed * Time.deltaTime);
                Debug.Log("True");
            }
            else
            {
                if (i >= 0)
                {
                    i--;
                    if (i == 0)
                        LastPos = true;
                }

                transform.position = Vector2.MoveTowards(transform.position, target[i], speed * Time.deltaTime);
                Debug.Log("false");
            }
        }
    }

    IEnumerator SpawnEnemyWaves(GameObject position)
    {
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