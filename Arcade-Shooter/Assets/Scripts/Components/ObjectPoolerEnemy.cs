using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class ObjectPoolItemEnemy
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}
public class ObjectPoolerEnemy : MonoBehaviour
{

    public static ObjectPoolerEnemy SharedInstance;
    public List<ObjectPoolItemEnemy> itemsToPool;
    public List<GameObject> pooledObjects;

    void Start()
    {
        //at StartPoint loading it Spawns & Deactivates All the Needed GameObjects
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItemEnemy item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }


    public GameObject GetPooledObjectEnemy(string tag)
    {
        //Check The Quantity Of Objects
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag.Equals(tag))
            {
                return pooledObjects[i];
            }
        }
        //Check if The List Needs To Grow
        foreach (ObjectPoolItemEnemy item in itemsToPool)
        {
            if (item.objectToPool.CompareTag(tag))
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
